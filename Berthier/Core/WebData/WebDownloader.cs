using System.Net.Http;
using System.Net.Http.Json;

namespace Berthier.WebData;

public class WebDownloader
{
    private async Task<HttpContent> ExecuteRequest(string url){
        var client = new HttpClient();
        var response = await client.GetAsync(url);
        var content = response.Content;
        return content;
    }

    private string MakeYahooRequest(List<string> stocks, DateTime startDate, DateTime endDate, string interval="1d"){
        int period1 = (int)((DateTimeOffset)startDate).ToUnixTimeSeconds();
        int period2 = (int)((DateTimeOffset)endDate).ToUnixTimeSeconds();
        string tickers = string.Join(",", stocks);

        string url = $"https://query1.finance.yahoo.com/v7/finance/download/{tickers}?period1={period1}&period2={period2}&interval={interval}&events=history";
        return url;
    }
}
