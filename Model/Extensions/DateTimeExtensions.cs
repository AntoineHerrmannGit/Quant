using System;

namespace Models.Extensions;

public static class DateTimeExtensions
{
    public static double ToMaturity(this DateTime date, DateTime maturity, string metric="d"){
        switch (metric){
            case "d":
                return (maturity - date).Days;
                
            case "m":
                return (maturity - date).Minutes;
                
            case "h":
                return (maturity - date).Hours;
                
            case "s":
                return (maturity - date).Seconds;
                
            case "ms":
                return (maturity - date).Milliseconds;
                
            case "mms":
                return (maturity - date).Microseconds;

            default:
                throw new ArgumentException($"Unknown metric {metric} for ToMaturity conversion. metric is not supported.");
        }
    }
}
