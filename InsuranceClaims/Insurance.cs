using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using InsuranceClaims.Models;
using InsuranceClaims.Utilities.Interfaces;

namespace InsuranceClaims
{
    public class Insurance: IInsurance
    {
        private readonly IFileUtilities _fileUtilities;
        public Insurance(IFileUtilities fileUtilities)
        {
            _fileUtilities = fileUtilities;
        }
        public async Task<string> GenerateResultFromInputFile(string file)
        {
            Guard.Against.NullOrEmpty(file, nameof(file));
            List<PaymentModel> paymentModels = await _fileUtilities.ReadTextFileAndTranslateToPaymentModelAsync(file);

            var paymentsMatrix = GetPaymentsMatrix(paymentModels, out int minOriginYear, out int range);

            string output = GenerateOutput(ref paymentsMatrix, minOriginYear, range);

            await _fileUtilities.WriteTextToFileAsync("result.txt", output);

            return output;
        }
        private List<PaymentMatrixModel> GetPaymentsMatrix(List<PaymentModel> paymentModels, out int minOriginYear
            ,out int range)
        {
            minOriginYear = paymentModels.Min(w => w.OriginYear);
            int maxOriginYear = paymentModels.Max(w => w.OriginYear);

            range = maxOriginYear - minOriginYear + 1;

            var groupByData = paymentModels.OrderBy(w => w.Product).ThenBy(w => w.OriginYear).ThenBy(w => w.DevelopmentYear)
                .GroupBy(w => w.Product).Select(w => new
                {
                    w.Key,
                    OriginYear = w.GroupBy(e => e.OriginYear).Select(q => new
                    {
                        q.Key,
                        Data = q.Select(r => new
                        {
                            r.DevelopmentYear,
                            r.IncrementalValue
                        }).ToList()
                    }).ToList()
                }).ToList();

            List<PaymentMatrixModel> paymentMatrixModels = new List<PaymentMatrixModel>();
            foreach (var product in groupByData)
            {
                PaymentMatrixModel paymentMatrixModel = new PaymentMatrixModel(product.Key);
                double[,] matrix = new double[range, range];
                for (int i = minOriginYear, j = 0; i <= maxOriginYear; i++, j++)
                {
                    foreach (var originYear in product.OriginYear)
                    {
                        if (originYear.Key == i)
                        {
                            foreach (var data in originYear.Data)
                            {
                                if (data.DevelopmentYear >= originYear.Key)
                                {
                                    matrix[j, data.DevelopmentYear - originYear.Key] = data.IncrementalValue;
                                }
                            }
                        }
                    }
                }
                paymentMatrixModel.Matrix = matrix;
                paymentMatrixModels.Add(paymentMatrixModel);
            }

            return paymentMatrixModels;
        }
        private string GenerateOutput(ref List<PaymentMatrixModel> paymentsMatrix, int minOriginYear, int range)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{minOriginYear}, {range}");
            
            foreach (var paymentMatrixModel in paymentsMatrix)
            {
                stringBuilder.Append($"{paymentMatrixModel.Product}, ");
                for (var i = 0; i < range; i++)
                {
                    double temp = 0;
                    for (var j = 0; j < range - i; j++)
                    {
                        temp += paymentMatrixModel.Matrix[i, j]; 
                        paymentMatrixModel.Matrix[i, j] = temp;
                        stringBuilder.Append($"{temp}, ");
                    }
                }
                stringBuilder.Remove(stringBuilder.Length - 2, 2);
                stringBuilder.AppendLine();
            }
            
            return stringBuilder.ToString();
        }

        

        
    }
}