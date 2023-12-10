using DemoExchangeRate.Models;

namespace DemoExchangeRate.Interfaces.Services
{
    public interface IRateService
    {
        Task<IEnumerable<PangeaExchangeRate>> GetPangeaExchngeRatesAsync(string Country);
    }
}
