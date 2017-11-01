using System;

using BtcBot.Contracts;
using BtcBot.Models;

namespace BtcBot.Services {
    /// <summary>
    /// Combine multiple API into one dataflow.
    /// </summary>
    public interface IPollerService {
        /// <summary>
        /// Show state of the service
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Push new API to poll. When pushed, better if you call restart to update. Otherwise observer isn't changed.
        /// </summary>
        /// <param name="api"></param>
        void PushApi(IApiAdapter api);

        /// <summary>
        /// Remove all the APIs from the list
        /// </summary>
        void ClearApiList();

        /// <summary>
        /// Subscribe observer to prices change
        /// </summary>
        /// <param name="observer">Object who wants to receive updates for particular stock</param>
        /// <param name="stock">Buying stocks</param>
        /// <param name="updateInterval">Set how often to update prices</param>
        void StartPoll(IObserver<PriceDto> observer, Stock stock, int updateInterval = 60);

        /// <summary>
        /// Break all the operations and stop
        /// </summary>
        void Stop();
    }
}