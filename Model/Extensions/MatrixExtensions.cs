using Models.Models.Matrices;

namespace Models.Extensions
{
    public static class MatrixExtensions
    {
        public static bool IsEmpty(this Matrix matrix) => matrix.Dimensions[0] == 0;
        
        public static bool IsSquarred(this Matrix matrix) => matrix.Dimensions[0] == matrix.Dimensions[1];

        public static IList<int[]> FindZeros(this Matrix matrix)
        {
            IList<int[]> zeros = new List<int[]>();
            for (int i = 0; i<matrix.Dimensions[0]; i++)
            {
                for (int j = 0; j<matrix.Dimensions[0]; j++)
                {
                    if (matrix.Values[i, j] == 0)
                    {
                        zeros.Add([i,j]);
                    }
                }
            }
            return zeros;
        }

        public static Matrix Transpose(this Matrix matrix)
        {
            Matrix transposed = new Matrix([matrix.Dimensions[1], matrix.Dimensions[0]]);
            for (int i = 0; i<matrix.Dimensions[0]; i++)
            {
                for (int j = 0; j<matrix.Dimensions[1]; j++)
                {
                    transposed.Values[j,i] = matrix.Values[i,j];
                }
            }
            return transposed;
        }

        public static double Det(this Matrix matrix)
        {
            if (matrix.Dimensions[0] != matrix.Dimensions[1])
            {
                throw new ArgumentException("Matrix is not squarred");
            }

            if (matrix.Dimensions[0] == 0) throw new ArgumentException($"Matrix is empty");

            if (matrix.Dimensions[0] == 1) return matrix.Values[0,0];

            if (matrix.Dimensions[0] == 2) return matrix.Values[0,0] * matrix.Values[1,1] - matrix.Values[1,0] * matrix.Values[1,0];

            if (matrix.Dimensions[0] == 3) return (
                matrix.Values[0,0] * matrix.Values[1,1] * matrix.Values[2,2]
                + matrix.Values[1,0] * matrix.Values[2,1] * matrix.Values[0,2]
                + matrix.Values[0,1] * matrix.Values[1,2] * matrix.Values[2,0]
                - matrix.Values[0,0] * matrix.Values[1,1] * matrix.Values[2,2]
                - matrix.Values[0,2] * matrix.Values[1,1] * matrix.Values[2,0]
                - matrix.Values[0,0] * matrix.Values[1,2] * matrix.Values[2,1]
            );

            double det = 0;
            // Developing elong the first column
            for (int i = 0; i<matrix.Dimensions[0]; i++)
            {
                if (matrix.Values[i,0] != 0)
                {
                    Matrix subMatrix = new Matrix(matrix.Dimensions[0]-1);
                    int p = 0;
                    for (int j = 0; j<matrix.Dimensions[0]; j++)
                    {
                        if (p != i){
                            int q = 0;
                            for (int k = 0; k<matrix.Dimensions[0]; k++)
                            {
                                if (q!=j)
                                {
                                    subMatrix.Values[p,q] = matrix.Values[i,j];
                                    q++;
                                }
                            }
                            p++;
                        }
                    }

                    det += Math.Pow(-1, i) * matrix.Values[i,0] * Det(subMatrix);
                }
            }
            return det;
        }
    
        public static Matrix Identity(int dimension)
        {
            Matrix id = new Matrix(dimension);
            for(int i = 0; i<dimension; i++)
            {
                id.Values[i,i] = 1;
            }
            return id;
        }

        public static Matrix Inverse(this Matrix matrix)
        {
            if (matrix.Dimensions[0] != matrix.Dimensions[1])
            {
                throw new ArgumentException($"Matrix is not squares thus not invertible");
            }
            
            if (matrix.Dimensions[0] == 2)
            {
                double det = matrix.Det();
                if (det != 0)
                {
                    double[,] values = new double[2,2];
                    values[0,0] = matrix.Values[1,1] / det;
                    values[0,1] = - matrix.Values[0,1] / det;
                    values[1,0] = - matrix.Values[1,0] / det;
                    values[1,1] = matrix.Values[0,0] / det;
                    return new Matrix(values);
                }
                else
                {
                    throw new InvalidDataException("Matrix has zero determinant and is not invertible.");
                }
            }

            int n = matrix.Dimensions[0];
            Matrix inverse = Identity(n);
            for (int i = 0; i<n; i++)
            {
                if(matrix.Values[i,i] != 0)
                {
                    for (int j = i+1; j<n; j++)
                    {
                        double value = matrix.Values[j,i] / matrix.Values[i,i];
                        matrix.SubstractRows(j, i, value);
                        inverse.SubstractRows(j, i, value);
                    }
                }
                else
                {
                    for (int j = i+1; j<n; j++)
                    {
                        if (matrix.Values[i,j] != 0) 
                        {
                            matrix.SwapColumns(i,j);
                            inverse.SwapColumns(i,j);
                            break;
                        }
                        if (j==n-1)
                        {
                            throw new RankException($"Matrix is not of full rank and is not invertible.");
                        }
                    }
                }
            }

            for (int i=n-1; i>0; i--)
            {
                for (int j=i-1; j>=0; j--)
                {
                    double value = matrix.Values[j,i] / matrix.Values[i,i];
                    matrix.SubstractRows(j, i, value);
                    inverse.SubstractRows(j, i, value);
                }
            }
            
            return inverse;
        }

        #region Private Methods
        private static Matrix SwapRows(this Matrix matrix, int row1, int row2)
        {
            for (int i = 0; i<matrix.Dimensions[1]; i++)
            {
                double tmp = matrix.Values[row1,i];
                matrix.Values[row1,i] = matrix.Values[row2,i];
                matrix.Values[row2,i] = tmp;
            }
            return matrix;
        }

        private static Matrix SwapColumns(this Matrix matrix, int col1, int col2)
        {
            for (int i = 0; i<matrix.Dimensions[0]; i++)
            {
                double tmp = matrix.Values[i,col1];
                matrix.Values[i,col1] = matrix.Values[i,col2];
                matrix.Values[i,col2] = tmp;
            }
            return matrix;
        }

        private static Matrix MultiplyRow(this Matrix matrix, int row, double value)
        {
            for (int i = 0; i<matrix.Dimensions[1]; i++)
            {
                matrix.Values[row,i] *= value;
            }
            return matrix;
        }

        private static Matrix MultiplyColumn(this Matrix matrix, int col, double value)
        {
            for (int i = 0; i<matrix.Dimensions[0]; i++)
            {
                matrix.Values[i,col] *= value;
            }
            return matrix;
        }

        private static Matrix SubstractRows(this Matrix matrix, int row1, int row2, double lambda)
        {
            for (int i=0; i<matrix.Dimensions[1]; i++)
            {
                matrix.Values[row1,i] -= lambda * matrix.Values[row2,i];
            }
            return matrix;
        }

        #endregion Private Methods
    }
}