using Tools.Maths;
using Tools.Generators;
namespace Tests;

[TestClass]
public class Tests
{
    [TestMethod]
    public static bool CorrelationTest()
    {
        IList<double> input1 = [1,2,3,4,5,6,7,8,9];
        IList<double> input2 = [2,4,8,6,3,1,5,9,7];

        double correlation = Maths.Correlation(input1, input2);
        return correlation >= -1 && correlation <= 1;
    }
    
    [TestMethod]
    public static bool GaussianGeneratorTest()
    {
        GaussGenerator generator = new GaussGenerator(0, 1);
        IList<double> input = [];
        for (int i = 0; i<100; i++)
        {
            double x = generator.Next();
            double y = generator.Next();
            input.Add(x);
            input.Add(y);
            if (y > 1 / Math.Sqrt(2*Math.PI) * Math.Exp(-0.5 * x*x)) return false;
        } 

        double m = Maths.Average(input);
        double s = Maths.StdDev(input);
        return    -0.01 < m 
               && m < 0.01 
               && 0.95 < s 
               && s < 1.05 ;
    }

}