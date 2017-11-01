using System.Threading.Tasks;

using BtcBot.Contracts;
using BtcBot.Models;

namespace BtcBot.Adapters.BaseAdapter {
    public abstract class BaseApiAdapter: IApiAdapter {
        public string AdapterName { get; }

        public string Host { get; protected set; }

        public string Username { get; protected set; }

        public string Token { get; protected set; }

        protected BaseApiAdapter(string adapterName) {
            AdapterName = adapterName;
        }

        public abstract Task<PriceDto> GetPriceAsync(Stock stock);

        public abstract Task<bool> IsStockSupportedAsync(Stock stock);

        public virtual void Initialize(string host = null, string token = null, string username = null) {
            Username = username;
            Token = token;
            Host = host;
        }
    }
}