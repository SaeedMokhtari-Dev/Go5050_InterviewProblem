using System.Threading.Tasks;

namespace InsuranceClaims.Utilities.Interfaces
{
    public interface IMatrixUtilities
    {
        void Print2DArray<T>(T[,] matrix);
    }
}