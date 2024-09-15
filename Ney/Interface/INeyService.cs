using Models;
using Models.Enums;

namespace Ney.Interface;

public interface INeyService
{
    Task<double> PriceOption(Option option, Spot spot, double volatility, double rate, DateTime calculationDate);
    Task<Dictionary<Greek, double>> Greeks(Option option, Spot spot, double volatility, double rate, DateTime calculationDate);
}
