using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    //Explicação dos Campos
    //  Cartão de Crédito: Campos como CardNumber, ExpirationMonth, ExpirationYear, SecurityCode, CardholderName e DocumentNumber são típicos em uma transação de pagamento.Eles correspondem às informações que o Mercado Pago normalmente solicita para autorizar um pagamento com cartão de crédito.
    //  Token: O Mercado Pago utiliza tokens para garantir a segurança.Em uma integração com o Mercado Pago, você deve gerar um token para o cartão do usuário usando a API do Mercado Pago. Este token é então enviado ao servidor para completar a transação.
    //  Informações de Pagamento: O campo Amount é o valor do pagamento, e PaymentMethodId é o identificador do método de pagamento (por exemplo, "visa" ou "master"). O Mercado Pago oferece diferentes métodos de pagamento, e o campo PaymentMethodId indica qual método foi escolhido pelo cliente.
    //  Endereço de Envio: Caso o pagamento envolva a entrega de produtos físicos, você pode precisar incluir um endereço de entrega. Isso pode ser útil se você estiver processando pagamentos com entrega.
    //  Informações de Contato: Você também pode coletar o email e telefone do comprador caso seja necessário para completar o processo de pagamento ou para questões de suporte.
    public class PaymentFormData
    {
        // Informações do cartão de crédito
        public string CardNumber { get; set; } // Número do cartão
        public string ExpirationMonth { get; set; } // Mês de validade (MM)
        public string ExpirationYear { get; set; } // Ano de validade (AAAA)
        public string SecurityCode { get; set; } // Código de segurança (CVV)
        public string CardholderName { get; set; } // Nome do titular do cartão
        public string DocumentNumber { get; set; } // Número do documento (RG ou CPF)
        public string Issuer { get; set; } // Emissor do cartão (opcional, depende do tipo de cartão)

        // Informações de pagamento
        public decimal Amount { get; set; } // Valor total do pagamento
        public string PaymentMethodId { get; set; } // Método de pagamento, ex: "visa", "master"
        public string Token { get; set; } // Token gerado pelo Mercado Pago para segurança

        // Informações de endereço (caso o pagamento envolva a entrega de produtos)
        public string ShippingAddress { get; set; } // Endereço de entrega
        public string ShippingPostalCode { get; set; } // CEP de entrega

        // Dados de contato (caso necessário)
        public string Email { get; set; } // Email do comprador
        public string Phone { get; set; } // Telefone do comprador
        public PedidoModel Pedido { get; set; }
    }

}
