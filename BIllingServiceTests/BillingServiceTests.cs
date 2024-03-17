using Billing.Data;
using Billing.Entity;
using Billing.Helpers;
using Billing.Services;
using Microsoft.Extensions.Options;

namespace BIllingServiceTests
{
    public class Tests
    {
        private IBillingService _billingService;
        private IOptions<TierSettings> _optionsTier;
        private IOptions<TierQuantity> _optionsQt;
        [SetUp]
        public void Setup()
        {
            _optionsTier =  BillingMockFactoryData.GetTierSettings();
            _optionsQt = BillingMockFactoryData.GetTierQuantity();
            _billingService = new BillingService(_optionsTier, _optionsQt);
        }

        [Test]
        public void BillingServiceComputeBillShouldReturnTwoFiifityFourAndNinteyTwoChange()
        {
            var item = new OrderItem
            {
                FeatureName = Feature.DesignImplant,
                Quantity = 8,
            };
            // 5 * 29.99 + 3 * 34.99 = 254.92
            var expectedTotal = _optionsQt.Value.DesignImplantLower * _optionsTier.Value.DesignImplants.Tier5 + 
                                (item.Quantity - _optionsQt.Value.DesignImplantLower) * _optionsTier.Value.DesignImplants.Tier10;


            var result = _billingService.ComputeBill(new Usage { Usages = new List<OrderItem> { item} }).Result;
            Assert.That(result, Is.EqualTo(expectedTotal));
        }

        [Test]
        public void BillingServiceComputeBillShouldReturnEightNinteyNineAndEightyTwoChange()
        {
            var item = new OrderItem
            {
                FeatureName = Feature.PrintImplant,
                Quantity = 18,
            };
            // 18 * 49.99 = 899.82
            var expectedTotal =  item.Quantity * _optionsTier.Value.PrintImplants.Tier25;

            var result = _billingService.ComputeBill(new Usage { Usages = new List<OrderItem> { item } }).Result;
            Assert.That(result, Is.EqualTo(expectedTotal));
        }

        [Test]
        public void BillingServiceComputeBillShouldReturnOneThousandOneFiftyFourAndNinteySevenChange()
        {

            // 5 * 29.99 + 3 * 34.99 = 254.92
            // 18 * 49.99 = 899.82
            var designItem = new OrderItem
            {
                FeatureName = Feature.DesignImplant,
                Quantity = 8,
            };
            var printItem = new OrderItem
            {
                FeatureName = Feature.PrintImplant,
                Quantity = 18,
            };

            var expectedTotal = _optionsQt.Value.DesignImplantLower * _optionsTier.Value.DesignImplants.Tier5 
                                 + (designItem.Quantity - _optionsQt.Value.DesignImplantLower) * _optionsTier.Value.DesignImplants.Tier10
                                 + printItem.Quantity *_optionsTier.Value.PrintImplants.Tier25;

            var result = _billingService.ComputeBill(new Usage { Usages = new List<OrderItem> { designItem, printItem } }).Result;
            Assert.That(result, Is.EqualTo(expectedTotal));
        }

        [Test]
        public void BillingServiceComputeBillShouldReturnOneThousandFiveFortyNineAndSevenChange()
        {
            var item = new OrderItem
            {
                FeatureName = Feature.PrintImplant,
                Quantity = 30,
            };
            // 25 * 49.99 = 1249.75 
            // 5 * 59.99 = 299.95
            // 1249.75 + 299.95 = 1549.7
            var expectedTotal = _optionsQt.Value.PrintImplantLower * _optionsTier.Value.PrintImplants.Tier25
                                + (item.Quantity - _optionsQt.Value.PrintImplantLower) * _optionsTier.Value.PrintImplants.Tier30;

            var result = _billingService.ComputeBill(new Usage { Usages = new List<OrderItem> { item } }).Result;
            Assert.That(result, Is.EqualTo(expectedTotal));
        }

        [Test]
        public void BillingServiceComputeBillShouldReturnTwoThousandSevenHundredAndFourAndFortyFourChange()
        {

            // 5 * 29.99 + 3 * 34.99 = 254.92
            // 18 * 49.99 = 899.82
            var designItem = new OrderItem
            {
                FeatureName = Feature.DesignImplant,
                Quantity = 8,
            };
            var printItem1 = new OrderItem
            {
                FeatureName = Feature.PrintImplant,
                Quantity = 18,
            };

            var printItem2 = new OrderItem
            {
                FeatureName = Feature.PrintImplant,
                Quantity = 30,
            };


            // 5 * 29.99 + 3 * 34.99 = 254.92
            // 18 * 49.99 = 899.82
            // 25 * 49.99 = 1249.75 
            // 5 * 59.99 = 299.95
            // 30 > 1249.75 + 299.95 = 1549.7
            // 254.92 + 899.82 + 1549.7 = 2704.44
            var expectedTotal = _optionsQt.Value.DesignImplantLower * _optionsTier.Value.DesignImplants.Tier5
                                 + (designItem.Quantity - _optionsQt.Value.DesignImplantLower) * _optionsTier.Value.DesignImplants.Tier10
                                 + printItem1.Quantity * _optionsTier.Value.PrintImplants.Tier25
                                 + _optionsQt.Value.PrintImplantLower * _optionsTier.Value.PrintImplants.Tier25
                                 + (printItem2.Quantity - _optionsQt.Value.PrintImplantLower) * _optionsTier.Value.PrintImplants.Tier30;

            expectedTotal = (double)Math.Round((decimal)expectedTotal, 2);
            var result = _billingService.ComputeBill(new Usage { Usages = new List<OrderItem> { designItem, printItem1, printItem2 } }).Result;
            Assert.That(result, Is.EqualTo(expectedTotal));
        }
    }
}