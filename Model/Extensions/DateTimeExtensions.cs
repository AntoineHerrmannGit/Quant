using System;

namespace Models.Extensions;

public static class DateTimeExtensions
{
    public static TimeSpan ToMaturity(this DateTime date, DateTime maturity)
    {
        return maturity - date;
    }

    public static string ToString(this DateTime date)
    {
        return date.ToString("YYYY-MM-DD HH:mm:ss.fff");
    }
}
