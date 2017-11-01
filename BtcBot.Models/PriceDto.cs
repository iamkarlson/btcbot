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

        public PriceDto() {
        }

        public PriceDto(Stock stock, decimal dataLprice, string source) {
            SellingCode = stock.SellingCode;
            BuyingCode = stock.BuyingCode;
            Price = dataLprice;
            SourceName = source;
            CreationDate = DateTime.Now;
        }
    }
}