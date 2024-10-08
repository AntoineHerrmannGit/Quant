using YahooFinanceApi;

namespace Berthier.Interface;

public interface IBerthierService
{
    Task<IDictionary<string, IEnumerable<Candle>>> GetSpots(List<string> tickers, DateTime startDate, DateTime endDate, string interval);
}
