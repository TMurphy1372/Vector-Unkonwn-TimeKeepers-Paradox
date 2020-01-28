using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Matrix = MathNet.Numerics.LinearAlgebra.Matrix<double>;
using Vector = MathNet.Numerics.LinearAlgebra.Vector<double>;


/* Test Script used to test Math.Net Numerics from 
     * https://answers.unity.com/questions/462042/unity-and-mathnet.html
     * Used for testing, not needed for functionality
     */
public class MathTest : MonoBehaviour
{

    void Start()
    {

        Matrix A = Matrix.Build.DenseOfArray(new double[,] {
             {1,0,0},
             {0,2,0},
             {0,0,3}});

        Vector b = Vector.Build.Dense(new double[] { 1, 1, 1 });

        Vector x = A.Solve(b);


        Debug.Log("x = A^-1b: " + x);
    }
}

