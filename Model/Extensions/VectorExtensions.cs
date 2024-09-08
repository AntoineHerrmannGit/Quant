using Models.Models.Vectors;

namespace Models.Extensions;

public static class VectorExtensions
{
    public static double Dot(this Vector vector, Vector other){
        if (vector.Length != other.Length)
            throw new ArgumentException($"Vectors must have the same size. (got {vector.Length} and {other.Length})");
        
        double dot = 0;
        for (int i = 0; i < vector.Length; i++){
            dot += vector[i] * other[i];
        }
        return dot; 
    }
    
    public static Vector Vect(this Vector vector, Vector other){
        if (vector.Length != other.Length)
            throw new ArgumentException($"Vectors must have the same size. (got {vector.Length} and {other.Length})");
        
        var vect = new Vector(vector.Length);
        int n = vector.Length;
        for (int i = 0; i < vector.Length; i++){
            vect[i] = vector[(i+1)%n] * other[(i+2)%n] - vector[(i+2)%n] * other[(i+1)%n];
        }
        return vect; 
    }

    public static double Norm(this Vector vector){
        double norm = 0;
        foreach (var v in vector){
            norm += v*v;
        }
        return Math.Sqrt(norm);
    }

    public static void Normalize(this Vector vector){
        double norm = vector.Norm();
        for(int i = 0; i < vector.Length; i++){
            vector[i] /= norm;
        } 
    }

}
