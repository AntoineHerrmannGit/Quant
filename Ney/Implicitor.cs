using Models;

namespace Ney.Implicitor;

public static class Implicitor
{
    public static double ImplicitBlackSholesVolatility(double optionPrice, Option option, Spot spot, double rate, DateTime calculationDate, double volatilityGuess=0.2, double errorThreshold=1e-8, int maxIterations=1000)
    {
        double Slope(Option option, Spot spot, double rate, double volatility, DateTime calculationDate, double step=1e-6)
        {
            double x0 = BlackSholes.BlackSholes.Price(option, spot, rate, volatility - step, calculationDate);
            double x1 = BlackSholes.BlackSholes.Price(option, spot, rate, volatility + step, calculationDate);
            return (x1 - x0) / (2*step);
        }

        int step = 0;
        double priceDiff;
        do
        {
            priceDiff = BlackSholes.BlackSholes.Price(option, spot, rate, volatilityGuess, calculationDate) - optionPrice;
            volatilityGuess -= priceDiff / Slope(option, spot, rate, volatilityGuess, calculationDate);
            step++;
        }
        while(Math.Abs(priceDiff) > errorThreshold && step < maxIterations);
        return volatilityGuess;
    }

    
}
