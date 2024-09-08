using Models.Enums;
using Models;
using Models.Extensions;
using Tools.Maths.Functions;

namespace Ney.BlackSholes;

public static class BlackSholes
{
    public static double Price(Option option, Spot spot, double volatility, double rate, DateTime calculationDate, string metric="d"){
        if (calculationDate > option.Maturity) 
            return 0;

        double maturity = calculationDate.ToMaturity(option.Maturity).Days / 365;

        double d1 = (Math.Log(spot.Value / option.Strike) + (rate - 0.5*volatility*volatility)*maturity) / (volatility * Math.Sqrt(maturity));
        double d2 = d1 - volatility * Math.Sqrt(maturity);
        double discount = Math.Exp(-rate * maturity);

        switch(option.Type){
            case OptionType.Call:
                return discount * ( spot.Value * Phi(d1) - option.Strike * Phi(d2));

            case OptionType.Put:
                return discount * ( option.Strike * Phi(-d2) - spot.Value * Phi(-d1));

            default:
                throw new ArgumentException($"Unknown type {option.Type}. Must be an OptionType");
        }
    }

    #region Greeks
    public static double Delta(Option option, Spot spot, double volatility, double rate, DateTime calculationDate, string metric="d"){
        if (calculationDate > option.Maturity) 
            return 0;

        double maturity = calculationDate.ToMaturity(option.Maturity).Days / 365;

        double d1 = (Math.Log(spot.Value / option.Strike) + (rate - 0.5*volatility*volatility)*maturity) / (volatility * Math.Sqrt(maturity));

        switch(option.Type){
            case OptionType.Call:
                return Math.Exp(-rate*maturity) * Phi(d1);
            
            case OptionType.Put:
                return -Math.Exp(-rate*maturity) * Phi(-d1);

            default:
                throw new ArgumentException($"Unknown type {option.Type}. Must be an OptionType");
        }
    }
    
    public static double Theta(Option option, Spot spot, double volatility, double rate, DateTime calculationDate, string metric="d"){
        if (calculationDate > option.Maturity) 
            return 0;

        double maturity = calculationDate.ToMaturity(option.Maturity).Days / 365;

        double d1 = (Math.Log(spot.Value / option.Strike) + (rate - 0.5*volatility*volatility)*maturity) / (volatility * Math.Sqrt(maturity));
        double d2 = d1 - volatility * Math.Sqrt(maturity);

        double firstTerm, secondTerm, thirdTerm;
        switch(option.Type){
            case OptionType.Call:
                firstTerm = - Math.Exp(-rate*maturity) * Phi(d1) * spot.Value * volatility / (2*Math.Sqrt(maturity));
                secondTerm = - Math.Exp(-rate*maturity) * rate * option.Strike * Phi(d2);
                thirdTerm = Math.Exp(-rate*maturity) * rate * spot.Value * Phi(d1);
                return firstTerm + secondTerm + thirdTerm;
            
            case OptionType.Put:
                firstTerm = - Math.Exp(-rate*maturity) * Phi(d1) * spot.Value * volatility / (2*Math.Sqrt(maturity));
                secondTerm = Math.Exp(-rate*maturity) * rate * option.Strike * Phi(-d2);
                thirdTerm = - Math.Exp(-rate*maturity) * rate * spot.Value * Phi(-d1);
                return firstTerm + secondTerm + thirdTerm;

            default:
                throw new ArgumentException($"Unknown type {option.Type}. Must be an OptionType");
        }
    }

    public static double Rho(Option option, Spot spot, double volatility, double rate, DateTime calculationDate, string metric="d"){
        if (calculationDate > option.Maturity) 
            return 0;

        double maturity = calculationDate.ToMaturity(option.Maturity).Days / 365;

        double d1 = (Math.Log(spot.Value / option.Strike) + (rate - 0.5*volatility*volatility)*maturity) / (volatility * Math.Sqrt(maturity));
        double d2 = d1 - volatility * Math.Sqrt(maturity);

        switch(option.Type){
            case OptionType.Call:
                return option.Strike * maturity * Math.Exp(-rate*maturity) * Phi(d2);
                
            case OptionType.Put:
                return - option.Strike * maturity * Math.Exp(-rate*maturity) * Phi(-d2);

            default:
                throw new ArgumentException($"Unknown type {option.Type}. Must be an OptionType");
        }
    }

    public static double Gamma(Option option, Spot spot, double volatility, double rate, DateTime calculationDate, string metric="d"){
        if (calculationDate > option.Maturity) 
            return 0;

        double maturity = calculationDate.ToMaturity(option.Maturity).Days / 365;

        double d1 = (Math.Log(spot.Value / option.Strike) + (rate - 0.5*volatility*volatility)*maturity) / (volatility * Math.Sqrt(maturity));

        return Math.Exp(-rate*maturity) * Phi(d1) / (spot.Value * volatility * Math.Sqrt(maturity));
    }

    public static double Vega(Option option, Spot spot, double volatility, double rate, DateTime calculationDate, string metric="d"){
        if (calculationDate > option.Maturity) 
            return 0;

        double maturity = calculationDate.ToMaturity(option.Maturity).Days / 365;

        double d1 = (Math.Log(spot.Value / option.Strike) + (rate - 0.5*volatility*volatility)*maturity) / (volatility * Math.Sqrt(maturity));

        return Math.Exp(-rate*maturity) * Phi(d1) * spot.Value * Math.Sqrt(maturity);
    }
    #endregion Greeks

    #region Private Methds
    private static double Phi(double x, double inf = -10, double nSteps = 100){
        double phi = 0;
        double dx = (x -inf) / nSteps;
        double dx2 = 0.5*dx;
        double cursor = inf;

        double normalizationFactor = 1 / Math.Sqrt(2*Math.PI);

        for(int i = 0; i < nSteps; i++){
            phi += dx * (
                Functions.Normal(cursor, 0, 1) + 4*Functions.Normal(cursor + dx2, 0, 1) + Functions.Normal(cursor + dx, 0, 1)
            ) / 6;
            cursor += dx;
        }
        return phi;
    }
    #endregion Private Methds
}
