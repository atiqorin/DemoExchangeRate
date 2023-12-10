using DemoExchangeRate.Models;

namespace DemoExchangeRate.Interfaces.Clients
{
    public interface IPartnerRateClient
    {
        Task<IEnumerable<PartnerRate>> GetAllPartnerRatesAsync();
    }
}
