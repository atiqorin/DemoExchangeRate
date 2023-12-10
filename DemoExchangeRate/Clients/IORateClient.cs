using DemoExchangeRate.Interfaces.Clients;
using DemoExchangeRate.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DemoExchangeRate.Clients
{
    public class IORateClient : IPartnerRateClient
    {
        /// <summary>
        /// Gets partner rates from JSON asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PartnerRate>> GetAllPartnerRatesAsync()
        {
            PartnerRatesModel? partnerRates;
            using (FileStream jsonStream = new FileStream("Files/partner_rates.json", FileMode.Open, FileAccess.Read))
            {
                partnerRates = await JsonSerializer.DeserializeAsync<PartnerRatesModel>(jsonStream, new JsonSerializerOptions
                {
                    Converters ={
                        new JsonStringEnumConverter()
                    },
                    NumberHandling = JsonNumberHandling.AllowReadingFromString,
                    PropertyNameCaseInsensitive = true
                });
            }
            return partnerRates?.PartnerRates ?? [];
        }
    }
}
