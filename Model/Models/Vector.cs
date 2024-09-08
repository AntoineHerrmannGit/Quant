using System.Collections;
using Models.Models.Matrices;

namespace Models.Models.Vectors
{
    public class Vector : Matrix, IEnumerable<double>
    {
        public double this[int i] { get => Values[i,0]; set => Values[i,0] = value; }
        public int Length { get => Dimensions[0];}

        public Vector(int length){
            Values = new double[length, 1];
        }

        public Vector(double[] values) {
            Values = new double[values.Length, 1];
            for (int i = 0; i<values.Length; i++){
                Values[i,0] = values[i];
            }
        }

        public IEnumerator<double> GetEnumerator()
        {
            for(int i = 0; i < Length; i++){
                yield return Values[i,0];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for(int i = 0; i < Length; i++){
                yield return Values[i,0];
            }
        }
    }
}