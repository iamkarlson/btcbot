using System.Threading.Tasks;

using BtcBot.Models;

namespace BtcBot.Contracts {
    public interface IApiAdapter {
        /// <summary>
        /// Request API to get last price
        /// </summary>
        /// <param name="stock">Trading stock</param>
        /// <returns>Data-transfer object from service</returns>
        Task<PriceDto> GetPriceAsync(Stock stock);

        /// <summary>
        /// Used for defining supported stocks
        /// </summary>
        /// <param name="stock">Trading stock</param>
        /// <returns>Is stock supported - will be returned positive</returns>
        Task<bool> IsStockSupportedAsync(Stock stock);
    }
}