using CsvHelper.Configuration;

namespace InsuranceClaims.Models
{
    public sealed class PaymentModelMap : ClassMap<PaymentModel>
    {
        public PaymentModelMap()
        {
            Map(w => w.Product).Name("Product").Default("BadProductName").TypeConverterOption.NullValues(string.Empty);
            Map(w => w.OriginYear).Name("Origin Year")
                .Default(0).TypeConverterOption.NullValues(string.Empty);
            Map(w => w.DevelopmentYear).Name("Development Year")
                .Default(0).TypeConverterOption.NullValues(string.Empty);
            Map(w => w.IncrementalValue).Name("Incremental Value")
                .Default(0).TypeConverterOption.NullValues(string.Empty);
        }
    }
}