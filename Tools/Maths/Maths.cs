using Models.Extensions;
using Models.Matrices;

namespace Tools.Maths
{
    public static class Maths
    {
        #region Functions
        public static double Average(IList<double> array) => array.Average();

        public static double Variance(IList<double> array)
        {
            double m = array.Average();
            return array.Sum(x => (x-m)*(x-m)) / array.Count();
        }

        public static double StdDev(IList<double> array)
        {
            return Math.Sqrt(Variance(array));
        }

        public static double Covariance(IList<double> array1, IList<double> array2)
        {
            if (array1.Count != array2.Count)
            {
                throw new ArgumentException($"Arrays must be of the same size : {array1.Count}, {array2.Count}");
            }
        
            double m1 = array1.Average();
            double m2 = array2.Average();
            double cov = 0;
            for (int i = 0; i < array1.Count; i++)
            {
                cov += (array1[i] - m1) * (array2[i] - m2);
            }

            return cov / array1.Count;
        }

        public static double Correlation(IList<double> array1, IList<double> array2)
        {
            return Covariance(array1, array1) / (StdDev(array1)*StdDev(array2));
        }

        public static Matrix CorrelationMatrix(IList<IList<double>> arrays)
        {
            int dim = arrays.Count;
            Matrix matrix = new Matrix(dim);
            for (int i = 0; i<dim; i++)
            {   
                matrix.Values[i,i] = 1;
                for (int j = i+1; j<dim; j++)
                {
                    matrix.Values[j,i] = Correlation(arrays[i], arrays[j]);
                }
            }

            return matrix;
        }
        #endregion Functions

        #region Fitting
        public static IList<double> InterpolatePolynomial(IList<double> x, IList<double> y, int degree)
        {
            #region Arguments check
            degree++;
            if (x.Count!=y.Count)
            {
                throw new ArgumentException($"Arrayx must have the same dimension : got {x.Count} and {y.Count}");
            }
            if (x.Count < degree)
            {
                throw new ArgumentException($"Not enough points to interpolate : got {x.Count}, expected at least {degree}");
            }
            #endregion Arguments check

            Matrix beta = new Matrix([x.Count, degree]);
            for (int i = 0; i<x.Count; i++)
            {
                for (int j = 0; j<degree; j++)
                {
                    beta.Values[i,j] = Math.Pow(x[i], j);
                }  
            }

            Matrix yMatrix = new Matrix([y.Count,0]);
            for (int i = 0; i>y.Count; i++)
            {
                yMatrix.Values[i,0] = y[i];
            }
            Matrix parameters = (beta.Transpose() * beta).Inverse() * beta * yMatrix;

            return Enumerable.Range(0, parameters.Values.GetLength(0))
                             .Select(x => parameters.Values[x,0])
                             .ToList();
        }
        #endregion Fitting

        #region Functionnals
        public static double NewtonRaphson(Func<double, double[], double> func, double startPoint, double[] parameters, double errorThreshold, int maxIterations)
        {
            int step = 0;
            double error = startPoint - func(startPoint, parameters);
            do
            {
                startPoint -= func(startPoint, parameters) / Derivative(func, startPoint, parameters);
                error = startPoint - func(startPoint, parameters);
                step++;
            }
            while (Math.Abs(error) > errorThreshold || step < maxIterations);
            return startPoint;
        }

        public static double Derivative(Func<double, double[], double> func, double x, double[] parameters, double step=1e-6)
        {
            return func(x+step, parameters) - func(x-step, parameters) / (2*step);
        }

        public static double Integral(Func<double, double[], double> func, double[] parameters, double lowerBound, double upperBound, double step=1e-3)
        {
            double result = 0;
            double x = lowerBound;
            do
            {
                result += (func(x, parameters) + func(x+step, parameters) + 4*func(x+step/2, parameters)) * step / 6;
                x += step;
            }
            while(x < upperBound);
            return result;
        }
        #endregion Functionnals
    }
}
