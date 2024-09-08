using System.Collections;

namespace Models.Models.TimeSerie
{
    public class TimeSerie : IEnumerable<double>
    {
        public Dictionary<DateTime, double> Serie{ get => Serie; private set => Serie = value; }
        public int Length{ get => Serie.Count; }
        public double this[int index] => Serie.OrderBy(x => x.Key).Select(x => x.Value).ToList()[index];

        public TimeSerie()
        {
            Serie = new Dictionary<DateTime,double>();
        }
        
        public TimeSerie(IList<DateTime> dates, IList<double> values){
            if (dates.Count != values.Count){
                throw new ArgumentException($"Dates and Values must have the same Length : got {dates.Count} and {values.Count}");
            } 

            Serie = new Dictionary<DateTime,double>();
            foreach(var (date, value) in dates.Zip(values))
            {
                Serie[date] = value;
            }
        }

        public TimeSerie(Dictionary<DateTime, double> serie){
            Serie = serie;
        }

        IEnumerator<double> IEnumerable<double>.GetEnumerator()
        {
            return Serie.OrderBy(x => x.Key).Select(x => x.Value).GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Serie.OrderBy(x => x.Key).Select(x => x.Value).GetEnumerator();
        }
    }
}