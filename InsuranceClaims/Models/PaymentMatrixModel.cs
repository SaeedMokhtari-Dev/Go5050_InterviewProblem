namespace InsuranceClaims.Models
{
    public class PaymentMatrixModel
    {
        public PaymentMatrixModel(string product)
        {
            Product = product;
        }
        public string Product { get; set; }
        public double[,] Matrix { get; set; }
    }
}