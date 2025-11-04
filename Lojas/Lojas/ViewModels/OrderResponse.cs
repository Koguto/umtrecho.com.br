using Newtonsoft.Json;

namespace ViewModels
{
    public class OrderResponseViewModel
    {
        [JsonProperty("in_store_order_id")]
        public string InStoreOrderId { get; set; }


        [JsonProperty("qr_data")]
        public string QrData { get; set; }
    }
}
