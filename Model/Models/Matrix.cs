namespace Models.Models.Matrices
{
    public class Matrix
    {
        public double[,] Values;
        public int[] Dimensions;

        public double this[int i, int j] {get => Values[i,j]; set => Values[i,j] = value;}

        #region Constructors
        public Matrix()
        {
            Values = new double[0,0];
            Dimensions = [0, 0];
        }

        public Matrix(int[] dimensions)
        {
            Values = new double[dimensions[0], dimensions[1]];
            Dimensions = dimensions;
        }

        public Matrix(int n)
        {
            Values = new double[n, n];
            Dimensions = [n, n];
        }

        public Matrix(double[,] values)
        {
            Values = values;
            Dimensions = [values.GetLength(0), values.GetLength(1)];
        }
        #endregion Constructors

        #region Operators
        public static Matrix operator +(Matrix matrix1, Matrix matrix2) 
        {
            if (matrix1.Dimensions != matrix2.Dimensions)
            {
                throw new ArgumentException("Both matrix does not have the same dimensions");
            }

            double[,] sumMatrix = new double[matrix1.Dimensions[0], matrix1.Dimensions[1]];
            for (int i = 0; i < matrix1.Dimensions[0]; i++)
            {
                for (int j = 0; j < matrix1.Dimensions[1]; j++)
                {
                    sumMatrix[i,j] = matrix1.Values[i,j] + matrix2.Values[i,j];
                }
            }
            
            return new Matrix(sumMatrix);
        }
        
        public static Matrix operator -(Matrix matrix1, Matrix matrix2) 
        {
            if (matrix1.Dimensions != matrix2.Dimensions)
            {
                throw new ArgumentException("Both matrix does not have the same dimensions");
            }

            double[,] sumMatrix = new double[matrix1.Dimensions[0], matrix1.Dimensions[1]];
            for (int i = 0; i < matrix1.Dimensions[0]; i++)
            {
                for (int j = 0; j < matrix1.Dimensions[1]; j++)
                {
                    sumMatrix[i,j] = matrix1.Values[i,j] - matrix2.Values[i,j];
                }
            }
            
            return new Matrix(sumMatrix);
        }

        public static Matrix operator *(Matrix matrix, double value)
        {
            for(int i = 0; i < matrix.Dimensions[0]; i++)
            {
                for(int j = 0; j < matrix.Dimensions[0]; j++)
                {
                    matrix.Values[i,j] *= value;
                }
            }
            return matrix;
        }
        
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            if(matrix1.Dimensions[1] != matrix2.Dimensions[0])
            {
                throw new ArgumentException($"Matrix dimensions do not match : {matrix1.Dimensions.ToString()} and {matrix2.Dimensions.ToString()}");
            }

            Matrix product = new Matrix([matrix1.Dimensions[0], matrix2.Dimensions[1]]);
            for(int i = 0; i<matrix1.Dimensions[0]; i++)
            {
                for(int j = 0; j<matrix1.Dimensions[0]; j++)
                {
                    product.Values[i,j] = 0;
                    for(int k = 0; k<matrix1.Dimensions[1]; k++)
                    {
                        product.Values[i,j] += matrix1.Values[i,k]*matrix2.Values[k,j];
                    }
                }
            }
            
            return product;
        }
        #endregion Operators
    }
}