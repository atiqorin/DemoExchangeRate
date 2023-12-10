namespace DemoExchangeRate.Models
{
    public class PartnerRate
    {
        public string? Currency { get; set; }
        public string? PaymentMethod { get; set; }
        public string? DeliveryMethod { get; set; }
        public double Rate { get; set; } = 0;
        public DateTime? AcquiredDate { get; set; }
    }
}
