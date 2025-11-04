namespace Modelos
{
    public class PayloadUsuario
    {
        public int ID { get; set; }
        public long CPF { get; set; }
        public string? Email { get; set; }
        public string? Nome { get; set; }
        public string? Token { get; set; }

    }
}
