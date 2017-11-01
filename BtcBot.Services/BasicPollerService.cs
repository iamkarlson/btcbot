using System;
using System.Collections.Generic;
using System.Threading;

using BtcBot.Contracts;
using BtcBot.Models;

namespace BtcBot.Services {
    /// <inheritdoc />
    public class BasicPollerService: IPollerService {
        private readonly List<IApiAdapter> apis = new List<IApiAdapter>();
        private readonly List<IObserver<PriceDto>> observers = new List<IObserver<PriceDto>>();
        private readonly List<Timer> timers = new List<Timer>();

        public bool IsRunning { get; private set; }

        /// <inheritdoc />
        public void PushApi(IApiAdapter api) {
            apis.Add(api);
        }

        /// <inheritdoc />
        public void ClearApiList() {
            apis.Clear();
        }

        /// <inheritdoc />
        public async void StartPoll(IObserver<PriceDto> observer, Stock stock, int updateInterval = 5) {
            IsRunning = true;
            observers.Add(observer);
            foreach(IApiAdapter api in apis) {
                if(!(await api.IsStockSupportedAsync(stock))) {
                    continue;
                }
                void requestPrice(object state) {
                    var obs = observer;
                    api.GetPriceAsync(stock).ContinueWith(task => {
                        if(task.IsFaulted) { obs.OnError(task.Exception ?? new Exception("Unknown exception during update")); }
                        if(task.IsCanceled) { return; }
                        if(task.IsCompleted) {
                            obs.OnNext(task.Result);
                        }
                    });
                }

                var timer = new Timer(requestPrice, null, TimeSpan.Zero, TimeSpan.FromSeconds(updateInterval));
                timers.Add(timer);
            }
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