using System;

namespace BtcBot.Models {
    /// <summary>
    /// DTO for price object
    /// </summary>
    public class PriceDto {
        public string SourceName { get; set; }

        public decimal Price { get; set; }

        public CurrencyCodes BuyingCode { get; set; }

        public CurrencyCodes SellingCode { get; set; }

        public DateTime CreationDate { get; set; }
    }
}