using System;

using BtcBot.Models;

using Collections;

using NLog;

namespace BtcBot.Core {
    public class PriceObserver: IObserver<PriceDto> {
        public bool IsCompleted { get; private set; }

        private readonly ObservableConcurrentBag<PriceDto> bag;

        private readonly Logger log = LogManager.GetCurrentClassLogger();

        public PriceObserver(ObservableConcurrentBag<PriceDto> bag) {
            log.Trace("ctor");
            this.bag = bag;
        }

        public void OnNext(PriceDto value) {
            log.Trace(nameof(OnNext));
            if(IsCompleted) { return; }
            bag.Add(value);
        }

        public void OnError(Exception error) {
            log.Trace(nameof(OnError));
            log.Error(error, "Error occured during observe");
            IsCompleted = true;
        }

        public void OnCompleted() {
            log.Trace(nameof(OnCompleted));
            IsCompleted = true;
        }
    }
}