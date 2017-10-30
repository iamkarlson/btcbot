using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BtcBot.Models;
using BtcBot.Services;

namespace BtcBot.ViewModels {
    internal class GraphPageViewModel: BaseNotifyPropertyChanged {
        ConcurrentBag<PriceDto> Prices = new ConcurrentBag<PriceDto>();

        public GraphPageViewModel(IPollerService pollerService) {
            
        }
    }
}
