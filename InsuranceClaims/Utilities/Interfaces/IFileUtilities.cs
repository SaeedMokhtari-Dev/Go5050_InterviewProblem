using System.Collections.Generic;
using System.Threading.Tasks;
using InsuranceClaims.Models;

namespace InsuranceClaims.Utilities.Interfaces
{
    public interface IFileUtilities
    {
        Task WriteTextToFileAsync(string file, string text);
        Task<List<PaymentModel>> ReadTextFileAndTranslateToPaymentModelAsync(string file);
        Task<string[]> ReadTextFileAsync(string file);
    }
}