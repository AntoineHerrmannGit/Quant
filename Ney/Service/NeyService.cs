using Models;
using Models.Enums;
using Ney.Interface;

namespace Ney.Service;

public class NeyService : INeyService
{
    public NeyService()
    {}
    
    public async Task<double> PriceOption(Option option, Spot spot, double volatility, double rate, DateTime calculationDate)
    {
        return BlackSholes.BlackSholes.Price(option, spot, volatility, rate, calculationDate);
    }
    
    public async Task<Dictionary<Greek, double>> Greeks(Option option, Spot spot, double volatility, double rate, DateTime calculationDate)
    {
        return new Dictionary<Greek, double>{
            {Greek.Delta, BlackSholes.BlackSholes.Delta(option, spot, volatility, rate, calculationDate)},
            {Greek.Gamma, BlackSholes.BlackSholes.Gamma(option, spot, volatility, rate, calculationDate)},
            {Greek.Vega, BlackSholes.BlackSholes.Vega(option, spot, volatility, rate, calculationDate)},
            {Greek.Theta, BlackSholes.BlackSholes.Theta(option, spot, volatility, rate, calculationDate)},
            {Greek.Rho, BlackSholes.BlackSholes.Rho(option, spot, volatility, rate, calculationDate)},
        };
    }
}
