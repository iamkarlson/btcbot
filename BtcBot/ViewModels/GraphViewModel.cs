using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Threading;

using BtcBot.Core;
using BtcBot.Models;
using BtcBot.Services;

using Collections;

using NLog;

namespace BtcBot.ViewModels {
    public class GraphViewModel: BaseNotifyPropertyChanged {
        public List<Stock> Stocks {
            get => getProp<List<Stock>>();
            set => setProp(value);
        }

        public RelayCommand Start {
            get => getProp<RelayCommand>();
            set => setProp(value);
        }

        public RelayCommand Stop {
            get => getProp<RelayCommand>();
            set => setProp(value);
        }

        public Stock SelectedStock {
            get => getProp<Stock>();
            set {
                setProp(value);
                RaisePropertyChanged(nameof(Title));
            }
        }

        public ObservableCollection<PriceDto> Prices { get; set; } = new ObservableCollection<PriceDto>();

        public ObservableConcurrentBag<PriceDto> PricesBag {
            get => getProp<ObservableConcurrentBag<PriceDto>>();
            set => setProp(value);
        }

        public string Title => SelectedStock != null ? $"Prices for {SelectedStock.DisplayName}" : "Prices window";

        private readonly Logger log = LogManager.GetCurrentClassLogger();
        private IPollerService poller;

        public GraphViewModel(IPollerService pollerService) {
            log.Trace("ctor");
            poller = pollerService;
            PricesBag = new ObservableConcurrentBag<PriceDto>(Dispatcher.CurrentDispatcher);
            PricesBag.CollectionChanged += PricesOnCollectionChanged;
            //TODO get rid of hardcode
            Stocks = new List<Stock>() {
                new Stock() {
                    BuyingCode = CurrencyCodes.BTC,
                    SellingCode = CurrencyCodes.USD
                },
                new Stock() {
                    BuyingCode = CurrencyCodes.ETH,
                    SellingCode = CurrencyCodes.USD
                },
                new Stock() {
                    BuyingCode = CurrencyCodes.BTC,
                    SellingCode = CurrencyCodes.EUR
                },
                new Stock() {
                    BuyingCode = CurrencyCodes.ETH,
                    SellingCode = CurrencyCodes.EUR
                },
            };
            Start = new RelayCommand(startCommand,
                                     () => {
                                         if(pollerService.IsRunning) { return false; }
                                         if(SelectedStock == null) { return false; }
                                         return true;
                                     });

            Stop = new RelayCommand(() => {
                                        log.Trace("Stopping poll");
                                        poller.Stop();
                                        log.Trace("Stopped poll");
                                    },
                                    () => pollerService.IsRunning);
            log.Trace("graph vm created");
        }

        private void startCommand() {
            log.Trace("Starting poll");
            var observer = new PriceObserver(PricesBag);
            poller.StartPoll(observer, SelectedStock, 5);
            log.Trace("Started poll");
        }

        private void PricesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs) {
            foreach(var item in notifyCollectionChangedEventArgs.NewItems) { Prices.Insert(0, item as PriceDto); }
        }
    }
}