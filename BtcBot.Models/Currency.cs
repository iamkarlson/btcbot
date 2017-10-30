namespace BtcBot.Models {
    /// <summary>
    /// Currency information
    /// </summary>
    public class Currency {
        /// <summary>
        /// Alphabetic code. For compability reasons mixed with unofficial cryptocurrencies' codes.
        /// </summary>
        public CurrencyCodes CurrencyCode { get; set; }

        /// <summary>
        /// ISO code
        /// </summary>
        public int? IsoNumericCode { get; set; }

        /// <summary>
        /// Currency short name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Currency description
        /// </summary>
        public string Description { get; set; }
    }
}