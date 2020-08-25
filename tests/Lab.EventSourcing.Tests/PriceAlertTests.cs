using Lab.EventSourcing.PriceAlert;
using Xunit;
using EventAssert = Lab.EventSourcing.Tests.Extensions.Assert;

namespace Lab.EventSourcing.Tests
{
    public class PriceAlertTests
    {
        private const string _symbol = "$";
        private const decimal _price = 10.0m;
        private PriceAlert.PriceAlert _priceAlert;

        [Fact]
        public void Should_Create_PriceAlertCreated_Event_When_PriceAlert_Is_Created()
        {
            _priceAlert = PriceAlert.PriceAlert.Create(_symbol, _price);

            EventAssert.EventEmitted<PriceAlertCreated>(_priceAlert.PendingEvents);
        }

        [Fact]
        public void Should_Create_PriceAlertTriggered_Event_When_PriceAlert_Is_Triggered()
        {
            _priceAlert = PriceAlert.PriceAlert.Create(_symbol, _price);
            _priceAlert.Commit();

            _priceAlert.Trigger();

            EventAssert.EventEmitted<PriceAlertTriggered>(_priceAlert.PendingEvents);
        }

        [Fact]
        public void Should_Create_PriceAlertCancelled_Event_When_PriceAlert_Is_Canceled()
        {
            _priceAlert = PriceAlert.PriceAlert.Create(_symbol, _price);
            _priceAlert.Commit();

            _priceAlert.Cancel();

            EventAssert.EventEmitted<PriceAlertCancelled>(_priceAlert.PendingEvents);
        }
    }
}
