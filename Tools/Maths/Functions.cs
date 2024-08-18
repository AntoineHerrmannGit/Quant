namespace Tools.Maths.Functions;

public static class Functions
{
    public static double Normal(double x, double mu = 0, double sigma = 1){
        return 1 / Math.Sqrt(2*Math.PI) * Math.Exp(0.5 * Math.Pow((x-mu)/sigma, 2));
    }
}
