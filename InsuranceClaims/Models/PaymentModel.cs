using CsvHelper.Configuration.Attributes;

namespace InsuranceClaims.Models
{
    public class PaymentModel
    {
        [Name("Product")]
        public string Product { get; set; }
        [Name("Origin Year")]
        public int OriginYear { get; set; }
        [Name("Development Year")]
        public int DevelopmentYear { get; set; }
        [Name("Incremental Value")]
        public double IncrementalValue { get; set; }
    }
}