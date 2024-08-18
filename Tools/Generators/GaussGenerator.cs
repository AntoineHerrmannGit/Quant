namespace Tools.Generators;

public class GaussGenerator
{
    private Random _generator;
    private double _mu = 0;
    private double _sigma = 1; 

    public GaussGenerator(double mu = 0, double sigma = 1) 
    {
        _generator = new Random();
        _mu = mu;
        _sigma = sigma;
    }

    public double Next()
    {
        double x, y, r;
        do
        {
            x = _generator.NextDouble();
            y = _generator.NextDouble();
            r = x*x + y*y;
        } while (r <= 1);
        return x * Math.Sqrt(- 2 * Math.Log(r) / r) * _sigma + _mu;
    }
}
