using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class CheckoutViewModel
    {
        public ProdutoViewModel Produto { get; set; }
        public string PreferenceId { get; set; }
        public string QrCodeUrl { get; set; }
    }

}
