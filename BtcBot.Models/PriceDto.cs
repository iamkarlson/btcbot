using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BtcBot.Models {
    /// <summary>
    /// DTO for price object
    /// </summary>
    public class PriceDto {
        public string SourceName { get; set; }

        public decimal Price { get; set; }

        public string BuyingCode { get; set; }

        public string SellingCode { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
