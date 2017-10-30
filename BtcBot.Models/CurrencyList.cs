using System.Collections.Generic;

namespace BtcBot.Models {
    public static class CurrencyList {
        private static readonly List<Currency> _currenciesList = new List<Currency>();

        public static List<Currency> List => _currenciesList;

        static CurrencyList() {
            #region filling
            _currenciesList.Add(new Currency() {
                CurrencyCode = CurrencyCodes.BTC,
                Name = "Bitcoin",
                Description = "First and very popular crypto-currency"
            });
            _currenciesList.Add(new Currency() {
                CurrencyCode = CurrencyCodes.ETH,
                Name = "Etherium",
                Description = "Popular currency for creation ICO and used for smart-contracts"
            });
            _currenciesList.Add(new Currency() {
                CurrencyCode = CurrencyCodes.USD,
                IsoNumericCode = (int?) CurrencyCodes.USD,
                Name = "United States Dollar",
                Description = "Most used fiat currency"
            });
            _currenciesList.Add(new Currency() {
                CurrencyCode = CurrencyCodes.EUR,
                IsoNumericCode = (int?) CurrencyCodes.EUR,
                Name = "Euro",
                Description = "European union currency"
            });
            #endregion filling
        }
    }
}