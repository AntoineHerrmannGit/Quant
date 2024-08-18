namespace Tools.Statistics;

public class Statistics
{
    public (IList<double>, IList<double>) Distribution(IList<double> array, double granularity = 0.1)
    {
        double max = array.Max();
        int nbPoints = array.Count;
        
        IList<double> steps = [];
        IList<double> distribution = [];
        
        double step = array.Min();
        while (step < max + granularity)
        {
            double n = 0;
            foreach (double d in array) if (d >= step && d < step+granularity) n++;
            steps.Add(step);
            distribution.Add(n / nbPoints);
            step += granularity;
        }
        return (steps, distribution);
    }
}
