using DemoExchangeRate.Controllers;
using DemoExchangeRate.Interfaces.Services;
using DemoExchangeRate.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace DemoExchangeRate.Tests.Controllers
{
    [TestClass]
    public class RateControllerTests
    {
        private Mock<IRateService> rateServiceMock;
        private Mock<ILogger<RateController>> loggerMock;
        private RateController rateController;
        [TestInitialize]
        public void Initialize()
        {
            rateServiceMock = new Mock<IRateService>();
            loggerMock = new Mock<ILogger<RateController>>();
            rateController = new RateController(loggerMock.Object, rateServiceMock.Object);

        }
        [TestMethod]
        public void GetExchangeRates_Returns_Rates()
        {
            // arrange
            IEnumerable<PangeaExchangeRate> rates = new List<PangeaExchangeRate>()
            {
                new PangeaExchangeRate()
                {
                    PangeaRate = 1.0,
                    CountryCode = "TST",
                    CurrencyCode = "TST",
                    PaymentMethod = "debit",
                    DeliveryMethod = "cash",
                }
            };
            rateServiceMock.Setup(m => m.GetPangeaExchngeRatesAsync(It.IsAny<string>())).Returns(Task.FromResult(rates));

            // act
            var result = rateController.GetExchagneRates("MEX");

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(TaskStatus.RanToCompletion, result.Status);
        }
        [TestMethod]
        public void GetExchangeRates_Returns_NotFound()
        {

            // act
            var result = rateController.GetExchagneRates("TST");

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.NotFoundResult", result.Result.ToString());
        }
    }
}