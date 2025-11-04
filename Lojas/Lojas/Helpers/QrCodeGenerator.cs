using QRCoder;
using SkiaSharp;
using System;
using System.IO;

namespace Helpers
{
    public class QrCodeGenerator
    {
        public string GenerateQrCodeBase64(string qrData)
        {
            // Criando o gerador de QR Code
            using (var qrGenerator = new QRCodeGenerator())
            {
                // Gerando os dados do QR Code com o nível de correção ECCLevel.Q
                var qrCodeData = qrGenerator.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.Q);

                // Gerando o QRCode com a codificação do QRCodeData
                var qrCode = new QRCode(qrCodeData);

                // Usando SkiaSharp para criar uma imagem
                using (var ms = new MemoryStream())
                {
                    int pixelSize = 10; // Tamanho de cada módulo do QR Code

                    // Criando um bitmap do QR Code
                    int width = qrCodeData.ModuleMatrix.Count * pixelSize;
                    int height = qrCodeData.ModuleMatrix.Count * pixelSize;

                    using (var bitmap = new SKBitmap(width, height))
                    {
                        using (var canvas = new SKCanvas(bitmap))
                        {
                            canvas.Clear(SKColors.White);

                            // Desenhando os módulos do QR Code
                            for (int i = 0; i < qrCodeData.ModuleMatrix.Count; i++)
                            {
                                for (int j = 0; j < qrCodeData.ModuleMatrix.Count; j++)
                                {
                                    var color = qrCodeData.ModuleMatrix[i][j] ? SKColors.Black : SKColors.White;
                                    var rect = new SKRect(j * pixelSize, i * pixelSize, (j + 1) * pixelSize, (i + 1) * pixelSize);
                                    canvas.DrawRect(rect, new SKPaint { Color = color });
                                }
                            }
                        }

                        // Convertendo para PNG e armazenando no MemoryStream
                        using (var image = SKImage.FromBitmap(bitmap))
                        using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                        {
                            data.SaveTo(ms);
                        }
                    }

                    // Convertendo o MemoryStream para Base64
                    var base64String = Convert.ToBase64String(ms.ToArray());
                    return base64String;
                }
            }
        }


    }
}
