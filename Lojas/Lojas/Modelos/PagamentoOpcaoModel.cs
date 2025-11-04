namespace Modelos
{
    public class PagamentoOpcaoModel
    {
        public string Thumbnail { get; set; } // Imagem ou thumbnail do pagamento
        public string Name { get; set; } // Nome do pagamento
        public string PaymentTypeId { get; set; } // Tipo de pagamento (ID)
        public string Status { get; set; } // Status do pagamento (por exemplo, "Ativo" ou "Inativo")
        public decimal MinAllowedAmount { get; set; } // Valor mínimo permitido
        public decimal MaxAllowedAmount { get; set; } // Valor máximo permitido
    }

}
