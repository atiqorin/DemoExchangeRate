namespace DemoExchangeRate.Models
{
    public class PangeaExchangeRate
    {
        public string? CurrencyCode { get; set; }
        public string? CountryCode { get; set; }
        public double PangeaRate { get; set; } = 0.0;
        public string? PaymentMethod { get; set; }
        public string? DeliveryMethod { get; set; }
    }
}
