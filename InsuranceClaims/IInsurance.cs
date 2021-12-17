using System.Threading.Tasks;

namespace InsuranceClaims
{
    public interface IInsurance
    {
        Task<string> GenerateResultFromInputFile(string file);
    }
}