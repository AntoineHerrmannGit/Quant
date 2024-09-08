using Models;

namespace Ney.Interface;

public interface INeyService
{
    Task<double> PriceOption(Option option, Spot spot, double volatility, double rate, DateTime calculationDate);
}
