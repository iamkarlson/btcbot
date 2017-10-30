using System.Collections.Concurrent;
using System.Collections.Generic;

using BtcBot.Models;
using BtcBot.Services;

using NLog;

namespace BtcBot.ViewModels {
    public class GraphViewModel: BaseNotifyPropertyChanged {
        public List<Stock> Stocks { get; set; }

        private readonly Logger log = LogManager.GetCurrentClassLogger();
        private IPollerService poller;
        ConcurrentBag<PriceDto> Prices = new ConcurrentBag<PriceDto>();

        public GraphViewModel(IPollerService pollerService) {
            log.Trace("ctor");
            poller = pollerService;
            Stocks = new List<Stock>() {
                new Stock() {
                    BuyingCode = CurrencyCodes.BTC,
                    SellingCode = CurrencyCodes.USD
                }
            };
            log.Trace("graph vm created");
        }
    }
}