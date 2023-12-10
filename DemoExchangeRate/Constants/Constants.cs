namespace DemoExchangeRate.Constants
{
    public static class Constants
    {
        public static readonly string[] PaymentMethods = new[]
        {
            "cash", "debit", "bankaccount"
        };
        public static readonly string[] DeliveryMethods = new[]
        {
            "debit", "deposit", "cash"
        };

        public static readonly Dictionary<string, double> FlatRates = new Dictionary<string, double>()
        {
            { "MEX" , 0.024 },
            { "IND" , 2.437 },
            { "PHL" , 0.056 },
            { "GTM" , 3.213 },
        };
    }
}
