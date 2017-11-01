using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using BtcBot.Adapters.BaseAdapter;
using BtcBot.Models;

using NLog;

using RestSharp;

namespace BtcBot.Adapters.Cexio {
    public sealed class CexioApiAdapter: BaseApiAdapter {
        private readonly Logger log = LogManager.GetCurrentClassLogger();
        private RestClient client;

        public CexioApiAdapter(): base("cex.io") {
            var host = "https://cex.io/";
            Initialize(host);
        }

        public override void Initialize(string host = null, string token = null, string username = null) {
            base.Initialize(host, token, username);
            client = new RestClient(Host);
        }

        public override async Task<PriceDto> GetPriceAsync(Stock stock) {
            var uri = $"api/last_price/{stock.BuyingCode}/{stock.SellingCode}";
            IRestRequest req = new RestRequest(uri, Method.GET);

            try {
                var cancellationTokenSource = new CancellationTokenSource();

                var resp = await client.ExecuteTaskAsync<LastPriceDto>(req, cancellationTokenSource.Token);
                if(resp.Data != null) { return new PriceDto(stock, resp.Data.lprice, AdapterName); }
                log.Error($"Requested stock is invalid. Please refer to the error message below");
                log.Error(Encoding.UTF8.GetString(resp.RawBytes));
                return null;
            }
            catch(Exception ex) {
                log.Error(ex, "Application exception.");
                return null;
            }
        }

        public override async Task<bool> IsStockSupportedAsync(Stock stock) {
            var price = await GetPriceAsync(stock);
            return price != null;
        }
    }
}