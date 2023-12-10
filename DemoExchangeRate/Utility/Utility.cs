namespace DemoExchangeRate.Utility
{
    public static class Utility
    {
        private static readonly Dictionary<string, string> Currencies = new Dictionary<string, string>()
        {
            { "MEX" , "MXN" },
            { "IND" , "INR" },
            { "PHL" , "PHP" },
            { "GTM" , "GTQ" },
        };
        public static string? GetCurrency(string country)
        {
            string currency = "";
            try
            {
                currency = Currencies[country];
            } catch (Exception ex)
            {
                return null;
            }
            return currency;
        }
    }
}
