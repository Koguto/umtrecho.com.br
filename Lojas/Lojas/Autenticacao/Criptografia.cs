using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Autenticacao
{
    public class Criptografia
    {

        private static string Key = "_sisc_p@sswrd_d@_plataform@_2@2@";
        private static string KeyBase64 = "q3X6tH+W9uFgJdTddqZds9eb/gz2Iex5d1RVuVtMavE=";

        public static string Criptografar(string texto)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(texto);

            using var aes = Aes.Create();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;

            aes.Key = Convert.FromBase64String(KeyBase64); // Use uma chave gerada e armazenada de forma segura
            aes.GenerateIV(); // Gere um IV único para cada operação

            using ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] encrypted = encryptor.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);

            // Combine o IV com o texto criptografado
            byte[] result = new byte[aes.IV.Length + encrypted.Length];
            Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length);
            Buffer.BlockCopy(encrypted, 0, result, aes.IV.Length, encrypted.Length);

            return Convert.ToBase64String(result);
        }

        public static string Descriptografar(string textoEncriptado)
        {
            byte[] encryptedBytes = Convert.FromBase64String(textoEncriptado);

            using var aes = Aes.Create();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;

            aes.Key = Convert.FromBase64String(KeyBase64); // Use a mesma chave

            // Extraia o IV do texto criptografado
            byte[] iv = new byte[16];
            byte[] cipherText = new byte[encryptedBytes.Length - iv.Length];
            Buffer.BlockCopy(encryptedBytes, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(encryptedBytes, iv.Length, cipherText, 0, cipherText.Length);

            aes.IV = iv;

            using ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] decrypted = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);

            return Encoding.UTF8.GetString(decrypted);
        }


        public static string CriptografarSha256(string valor)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(valor.Trim());

            using var sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(bytes);

            // Converte os bytes para uma string hexadecimal
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }



        // Método para verificar a senha
        public static bool VerificarSenha(string senhaOriginal, string senhaCriptografadaComSaltDoBancoDeDados)
        {
            byte[] cipherBytes = Convert.FromBase64String(senhaCriptografadaComSaltDoBancoDeDados);

            byte[] salt = new byte[16]; // O salt está nos primeiros 16 bytes
            Buffer.BlockCopy(cipherBytes, 0, salt, 0, salt.Length);

            byte[] encryptedPassword = new byte[cipherBytes.Length - salt.Length];
            Buffer.BlockCopy(cipherBytes, salt.Length, encryptedPassword, 0, encryptedPassword.Length);

            // Criptografar a senha fornecida com o mesmo salt
            string senhaCriptografada = GerarSenhaEncrypt(senhaOriginal, salt);

            // Verificar se a senha criptografada fornecida é igual à armazenada
            return senhaCriptografada == senhaCriptografadaComSaltDoBancoDeDados;
        }

        // Método para gerar um salt aleatório de 16 bytes
        public static byte[] GenerateSalt(int size = 16)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] salt = new byte[size];
                rng.GetBytes(salt);  // Preenche o array de bytes com valores aleatórios
                return salt;
            }
        }

        public static string GerarSenhaEncrypt(string plainText, byte[] salt)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Chave derivada do salt e da senha
                var key = new Rfc2898DeriveBytes(plainText, salt, 10000);
                aesAlg.Key = key.GetBytes(32); // Tamanho de chave de 256 bits
                aesAlg.IV = new byte[16]; // Inicialize o IV (vetor de inicialização) com zero

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }

                    // Armazenamos o salt junto com a senha criptografada
                    byte[] encrypted = msEncrypt.ToArray();
                    byte[] result = new byte[salt.Length + encrypted.Length];
                    Buffer.BlockCopy(salt, 0, result, 0, salt.Length);
                    Buffer.BlockCopy(encrypted, 0, result, salt.Length, encrypted.Length);
                    return Convert.ToBase64String(result);
                }
            }

        }

        public static string Decrypt(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            // O salt é armazenado nos primeiros 16 bytes
            byte[] salt = new byte[16];
            Buffer.BlockCopy(cipherBytes, 0, salt, 0, salt.Length);

            // A parte criptografada da senha começa após o salt
            byte[] encryptedPassword = new byte[cipherBytes.Length - salt.Length];
            Buffer.BlockCopy(cipherBytes, salt.Length, encryptedPassword, 0, encryptedPassword.Length);

            using (Aes aesAlg = Aes.Create())
            {
                // Deriva a chave com base na senha e no salt
                var key = new Rfc2898DeriveBytes("minhaSenhaSecreta", salt, 10000);
                aesAlg.Key = key.GetBytes(32); // Tamanho de chave de 256 bits
                aesAlg.IV = new byte[16]; // Inicialize o IV (vetor de inicialização) com zero

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(encryptedPassword))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }




    }
}
