using Models.Models.TimeSerie;

namespace Models.Extensions
{
    public static class TimeSerieExtensions
    {
        public static TimeSerie Reverse(this TimeSerie timeSerie)
        {
            return new TimeSerie( timeSerie.Serie.OrderByDescending(x => x.Key).ToDictionary() );
        }

        public static TimeSerie Apply(this TimeSerie timeSerie, 
                                    Func<double, double> func)
        {
            var dates = timeSerie.Serie.Keys.ToList();
            var values = timeSerie.Serie.Values.Select(x => func(x)).ToList();
            return new TimeSerie(dates, values);
        }

        public static TimeSerie Apply(this TimeSerie timeSerie, 
                                    Func<double, double[], double> func, 
                                    params double[] parameters)
        {
            var dates = timeSerie.Serie.Keys.ToList();
            var values = timeSerie.Serie.Values.Select(x => func(x, parameters)).ToList();
            return new TimeSerie(dates, values);
        }

        public static TimeSerie ApplyRolling(this TimeSerie timeSerie, 
                                            int window, 
                                            Func<IList<double>, double> func)
        {
            window = Math.Min(window, timeSerie.Length);
            IList<DateTime> dates = timeSerie.Serie.Keys.Skip(window).ToList();
            IList<double> values = [];
            for (int i=0; i<timeSerie.Length - window; i++)
            {
                values.Add( func( timeSerie.Serie.Values.Skip(i).Take(window).ToList() ) );
            }
            return new TimeSerie(dates, values);
        }

        public static TimeSerie ApplyRolling(this TimeSerie timeSerie, 
                                            int window, 
                                            Func<IList<double>, double[], double> func, 
                                            params double[] parameters)
        {
            window = Math.Min(window, timeSerie.Length);
            IList<DateTime> dates = timeSerie.Serie.Keys.Skip(window).ToList();
            IList<double> values = [];
            for (int i=0; i<timeSerie.Length - window; i++)
            {
                values.Add( func( timeSerie.Serie.Values.Skip(i).Take(window).ToList(), parameters ) );
            }
            return new TimeSerie(dates, values);
        }

        public static TimeSerie Intersect(this TimeSerie timeSerie, TimeSerie other)
        {
            return new TimeSerie( timeSerie.Serie.Where(e => other.Serie.ContainsKey(e.Key)).ToDictionary() );
        }

        public static double Mean(this TimeSerie timeSerie){
            return timeSerie.Serie.Average(e => e.Value);
        }

        public static double Variance(this TimeSerie timeSerie){
            return timeSerie.Moment(2);      
        }

        public static double StdDev(this TimeSerie timeSerie){
            return Math.Sqrt(timeSerie.Variance());
        }

        public static double Covariance(this TimeSerie timeSerie, TimeSerie other){
            if (timeSerie.Length != other.Length)
                throw new ArgumentException($"TimeSeries must have the same size");

            double m1 = timeSerie.Mean();
            double m2 = other.Mean();
            double covariance = 0;
            for(int i = 0; i < timeSerie.Length; i++){
                covariance += (timeSerie[i] - m1) * (other[i] - m2);
            }
            return covariance / timeSerie.Length;
        }

        public static double Correlation(this TimeSerie timeSerie, TimeSerie other){
            if (timeSerie.Length != other.Length)
                throw new ArgumentException($"TimeSeries must have the same size");

            return timeSerie.Covariance(other) / (timeSerie.StdDev() * other.StdDev());
        }
    
        public static double Moment(this TimeSerie timeSerie, int order){
            switch (order){
                case 0:
                    return 1;

                case 1:
                    return timeSerie.Mean();

                default:
                    double mean = timeSerie.Mean();
                    return timeSerie.Serie.Values.Sum(x => Math.Pow(x, order) - mean) / timeSerie.Average();
            }
        }

        public static double NormalizedMoment(this TimeSerie timeSerie, int order){
            switch (order){
                case 0: return 1;
                case 1: return timeSerie.Mean();
                default:
                    return timeSerie.Moment(order) / Math.Pow(timeSerie.StdDev(), order);
            }
        }

        public static double Skew(this TimeSerie timeSerie){
            return timeSerie.NormalizedMoment(3);
        }
        
        public static double Kurtosis(this TimeSerie timeSerie){
            return timeSerie.NormalizedMoment(4);
        }
        
    }
}