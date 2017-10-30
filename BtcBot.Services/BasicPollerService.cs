using System;
using System.Collections.Generic;
using System.Threading;

using BtcBot.Contracts;
using BtcBot.Models;

namespace BtcBot.Services {
    /// <inheritdoc />
    public class BasicPollerService: IPollerService {
        public bool IsRunning { get; set; }

        private readonly List<IApiAdapter> apis = new List<IApiAdapter>();
        private readonly List<IObserver<PriceDto>> observers = new List<IObserver<PriceDto>>();
        private readonly List<Timer> timers = new List<Timer>();

        /// <inheritdoc />
        public void PushApi(IApiAdapter api) {
            apis.Add(api);
        }

        public void ClearApiList() {
            apis.Clear();
        }

        /// <inheritdoc />
        public void StartPoll(IObserver<PriceDto> observer, CurrencyCodes sellingCode, CurrencyCodes buyingCode, int updateInterval = 60) {
            IsRunning = true;
            observers.Add(observer);
            foreach(IApiAdapter api in apis) {
                var timer = new Timer(state => {
                                          var obs = observer;
                                          api.GetPriceAsync(sellingCode, buyingCode).ContinueWith(price => {
                                              if(price.IsCompleted) {
                                                  obs.OnNext(price.Result);
                                                  return;
                                              }
                                              if(price.IsFaulted) {
                                                  obs.OnError(price.Exception ?? new Exception("Unknown exception during update"));
                                              }
                                          });
                                      },
                                      null,
                                      TimeSpan.Zero,
                                      TimeSpan.FromSeconds(updateInterval));
                timers.Add(timer);
            }
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Stop() {
            foreach(Timer timer in timers) { timer.Change(TimeSpan.FromMilliseconds(-1), TimeSpan.FromMilliseconds(-1)); }

            timers.Clear();

            foreach(IObserver<PriceDto> observer in observers) { observer.OnCompleted(); }

            observers.Clear();
            IsRunning = false;
        }
    }
}