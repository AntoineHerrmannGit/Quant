namespace Tools.Generators;

public class BrownianGenerator
{
    private GaussGenerator _generator;
    private double _seed;

    public BrownianGenerator(double seed = 100, double mu = 0, double sigma = 1)
    {
        _generator = new GaussGenerator(mu, sigma);
        _seed = seed;
    }

    public double Next()
    {
        _seed = _seed * (1 + _generator.Next());
        return _seed;
    }
}
