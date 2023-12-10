using DemoExchangeRate.Clients;
using DemoExchangeRate.Interfaces.Clients;
using DemoExchangeRate.Interfaces.Services;
using DemoExchangeRate.Models;
using Microsoft.AspNetCore.DataProtection;
using System.Linq;

namespace DemoExchangeRate.Services
{
    public class RateService : IRateService
    {
        private IPartnerRateClient _partnerRateClient;
        public RateService(IPartnerRateClient partnerRateClient)
        {
            _partnerRateClient = partnerRateClient;
        }

        /// <summary>
        /// Get list of PangeaExchangeRate from Country code
        /// </summary>
        /// <param name="Country"></param>
        /// <returns>IEnumerable<PangeaExchangeRate></returns>
        public async Task<IEnumerable<PangeaExchangeRate>> GetPangeaExchngeRatesAsync(string Country)
        {

            // get currency
            string? currency = Utility.Utility.GetCurrency(Country);

            // get rates and then filter by country
            IEnumerable<PartnerRate> allPartnerRates = await _partnerRateClient.GetAllPartnerRatesAsync();

            // filter by currency
            IList<PartnerRate> filteredRates = allPartnerRates.Where(rate => rate.Currency == currency).ToList();


            // get unique partner rates based on payment and delivery method
            IList<PartnerRate> uniquePartnerRates = GetUniquePartnerRates(filteredRates);

            // get all pangea rates
            IList<PangeaExchangeRate> allPangeaRates = GetAllPangeaRates(uniquePartnerRates, Country);

            // adjust rates by adding flat rate
            IList<PangeaExchangeRate> adjustedRates = GetAdjustedRates(allPangeaRates, Country);

            return adjustedRates;
        }

        /// <summary>
        /// Get adjusted rates based on Country
        /// </summary>
        /// <param name="pangeaRates"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        private IList<PangeaExchangeRate> GetAdjustedRates(IList<PangeaExchangeRate> pangeaRates, string country)
        {
            double flatRate = Constants.Constants.FlatRates[country];

            List<PangeaExchangeRate> adjustedRates = new List<PangeaExchangeRate>();
            foreach(var rate in pangeaRates)
            {
                adjustedRates.Add(new PangeaExchangeRate()
                {
                    PangeaRate = Math.Round(rate.PangeaRate += flatRate, 2),
                    CountryCode = rate.CountryCode,
                    CurrencyCode = rate.CurrencyCode,
                    PaymentMethod = rate.PaymentMethod,
                    DeliveryMethod = rate.DeliveryMethod,
                });
            }
            return adjustedRates;
        }

        /// <summary>
        /// Get Unique Rates based on most recent Acquired date based on payment and delivery method
        /// </summary>
        /// <param name="partnerRates"></param>
        /// <returns></returns>
        private IList<PartnerRate> GetUniquePartnerRates(IList<PartnerRate> partnerRates)
        {
            IList<PartnerRate> uniquePartnerRates = new List<PartnerRate>();
            foreach (var paymentMethod in Constants.Constants.PaymentMethods)
            {
                foreach(var deliveryMethod in Constants.Constants.DeliveryMethods)
                {
                    List<PartnerRate> uniqueRates = partnerRates.Where(rate => rate.PaymentMethod == paymentMethod && rate.DeliveryMethod == deliveryMethod).ToList();
                    PartnerRate? uniqueRate = uniqueRates.OrderByDescending(r => r.AcquiredDate).FirstOrDefault();
                    if (uniqueRate != null) { uniquePartnerRates.Add(uniqueRate); }
                }
            }
            return uniquePartnerRates;
        }

        /// <summary>
        /// Get List of all PangeaRates based on filteredRates provided
        /// </summary>
        /// <param name="filteredRates"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        private IList<PangeaExchangeRate> GetAllPangeaRates(IEnumerable<PartnerRate> filteredRates, string country)
        {
            List<PangeaExchangeRate> pangeaExchangeRates = new List<PangeaExchangeRate>();
            foreach (var rate in filteredRates)
            {
                pangeaExchangeRates.Add(new PangeaExchangeRate()
                {
                    PangeaRate = rate.Rate,
                    CountryCode = country,
                    CurrencyCode = rate.Currency,
                    PaymentMethod = rate.PaymentMethod,
                    DeliveryMethod = rate.DeliveryMethod,
                });
            }
            return pangeaExchangeRates;
        }
    }
}
