using System;
using Ardalis.GuardClauses;
using InsuranceClaims.Utilities.Interfaces;

namespace InsuranceClaims.Utilities.Impls
{
    public class MatrixUtilities : IMatrixUtilities
    {
        public void Print2DArray<T>(T[,] matrix)
        {
            Guard.Against.Null(matrix, nameof(matrix));
            
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }

                Console.WriteLine();
            }
        }
    }
}