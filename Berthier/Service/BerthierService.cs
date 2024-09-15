using Berthier.Interface;
using YahooFinanceApi;
using Models.Models.Logger.Interface;

namespace Berthier.Service;

public class BerthierService : IBerthierService
{
    private readonly ILogger _logger;
    private readonly IDataDownloader _dataDownloader;
    public BerthierService(ILogger logger, IDataDownloader dataDownloader)
    {
        _logger = logger;
        _dataDownloader = dataDownloader;
    }

    public async Task<IDictionary<string, IEnumerable<Candle>>> GetSpots(List<string> tickers, DateTime startDate, DateTime endDate, string interval)
    {
        return await _dataDownloader.GetSpots(tickers, startDate, endDate, interval);
    }
}
