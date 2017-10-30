namespace BtcBot.Models {
    public class Stock {
        public CurrencyCodes BuyingCode { get; set; }

        public CurrencyCodes SellingCode { get; set; }

        public string DisplayName => $"{BuyingCode}/{SellingCode}";
    }
}