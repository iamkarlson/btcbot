using System.Threading.Tasks;

using BtcBot.Models;

namespace BtcBot.Contracts {
    public interface IApiAdapter {
        Task<PriceDto> GetPriceAsync(CurrencyCodes sellingCode, CurrencyCodes buyingCode);
    }
}