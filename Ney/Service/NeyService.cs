using Models;
using Ney.BlackSholes;

namespace Ney.Service;

public class NeyService
{
    public async Task<double> PriceOption(Option option, Spot spot, double volatility, double rate, DateTime calculationDate)
    {
        return BlackSholes.BlackSholes.Price(option, spot, volatility, rate, calculationDate);
    }
}
