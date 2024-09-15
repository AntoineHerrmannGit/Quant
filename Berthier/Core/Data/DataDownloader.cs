using System.Collections.Concurrent;
using Berthier.Interface;
using YahooFinanceApi;

namespace Berthier.Core.Data;

public class DataDownloader : IDataDownloader
{
    // Public method to retrieve spot data from Yahoo Finance
    public async Task<IDictionary<string, IEnumerable<Candle>>> GetSpots(List<string> tickers, DateTime startDate, DateTime endDate, string interval)
    {
        var spotData = new ConcurrentDictionary<string, IEnumerable<Candle>>();

        await Parallel.ForEachAsync(tickers, async (ticker, cancellationToken) => 
            {
                var history = await FetchHistoricalData(ticker, startDate, endDate, interval);
                spotData[ticker] = history;
            }
        );

        return spotData.ToDictionary();
    }

    // Private method to map the interval string to Yahoo Finance Period
    private Period GetPeriod(string interval)
    {
        return interval.ToLower() switch
        {
            "d" => Period.Daily,
            "w" => Period.Weekly,
            "m" => Period.Monthly,
            "days" => Period.Daily,
            "weeks" => Period.Weekly,
            "months" => Period.Monthly,
            "daily" => Period.Daily,
            "weekly" => Period.Weekly,
            "monthly" => Period.Monthly,
            _ => throw new ArgumentException("Invalid interval. Use 'daily', 'weekly', or 'monthly'.")
        };
    }

    // Private method to fetch historical data for a given ticker
    private async Task<IEnumerable<Candle>> FetchHistoricalData(string ticker, DateTime startDate, DateTime endDate, string interval)
    {
        try
        {
            var period = GetPeriod(interval);
            var history = await Yahoo.GetHistoricalAsync(ticker, startDate, endDate, period);
            return history;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching data for {ticker}: {ex.Message}");
            return new List<Candle>(); // Return an empty list in case of an error
        }
    }
}
