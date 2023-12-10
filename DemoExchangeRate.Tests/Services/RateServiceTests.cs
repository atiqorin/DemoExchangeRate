using DemoExchangeRate.Interfaces.Clients;
using DemoExchangeRate.Models;
using DemoExchangeRate.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoExchangeRate.Tests.Services
{
    [TestClass]
    public class RateServiceTests
    {
        private Mock<IPartnerRateClient> partnerRateClient;
        private RateService rateService;

        [TestInitialize]
        public void Initialize()
        {
            partnerRateClient = new Mock<IPartnerRateClient>();
            rateService = new RateService(partnerRateClient.Object);
        }

        [TestMethod]
        public void GetPangeaExchngeRatesAsync_Returns_Result_On_Valid_Country()
        {
            // arrange
            IEnumerable<PartnerRate> rates = new List<PartnerRate>()
            {
                new PartnerRate()
                {
                    Currency = "MXN",
                    DeliveryMethod = "debit",
                    PaymentMethod = "debit",
                    Rate = 5.5,
                    AcquiredDate = DateTime.Now,
                }
            };
            partnerRateClient.Setup(m => m.GetAllPartnerRatesAsync()).Returns(Task.FromResult(rates));

            // act
            var result = rateService.GetPangeaExchngeRatesAsync("MEX");

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual("MEX", result.Result.FirstOrDefault().CountryCode);
        }
    }
}
