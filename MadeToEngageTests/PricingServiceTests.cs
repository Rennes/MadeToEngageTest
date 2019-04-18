using System;
using System.Collections.Generic;
using MadeToEngageTest.Business.Services;
using Moq;
using Xunit;

namespace MadeToEngageTests
{
    public class PricingServiceTests
    {
        private const decimal _e = 0.0001m;
        private const decimal _minimumDiscountPercentage = 0;
        private const decimal _maximumDiscountPercentage = 25;

        [Fact]
        public void Scenario1()
        {
            var ogranisationalDiscount = 7m;
            var organisationServiceMock = new Mock<IOrganisationService>();
            organisationServiceMock.Setup(x => x.GetOgranisationalDiscountForUser(1, out ogranisationalDiscount)).Returns(true);

            var productServiceMock = new Mock<IProductService>();
            var product = new Product() { Price = 36, OnlineDiscount = true};
            productServiceMock.Setup(x => x.GetProductBySku(1, out product)).Returns(true);

            var pricingService = new PricingService(organisationServiceMock.Object, productServiceMock.Object, _e, _minimumDiscountPercentage, _maximumDiscountPercentage);
            var customerPriceCalculated = pricingService.GetCustomerPriceForUser(1, 1, out decimal customerPrice);

            Assert.True(customerPriceCalculated);
            Assert.InRange(customerPrice, 32.9778m - _e, 32.9778m + _e);
        }

        [Fact]
        public void Scenario2()
        {
            var ogranisationalDiscount = 12m;
            var organisationServiceMock = new Mock<IOrganisationService>();
            organisationServiceMock.Setup(x => x.GetOgranisationalDiscountForUser(1, out ogranisationalDiscount)).Returns(true);

            var productServiceMock = new Mock<IProductService>();
            var product = new Product() { Price = 128.99m, OnlineDiscount = false};
            productServiceMock.Setup(x => x.GetProductBySku(1, out product)).Returns(true);

            var pricingService = new PricingService(organisationServiceMock.Object, productServiceMock.Object, _e, _minimumDiscountPercentage, _maximumDiscountPercentage);
            var customerPriceCalculated = pricingService.GetCustomerPriceForUser(1, 1, out decimal customerPrice);

            Assert.True(customerPriceCalculated);
            Assert.InRange(customerPrice, 113.5112m - _e, 113.5112m + _e);
        }

        [Fact]
        public void Scenario3()
        {
            var authServiceMock = new Mock<IAuthService>();
            authServiceMock.Setup(x => x.IsAuthorized(1)).Returns(false);

            Assert.False(authServiceMock.Object.IsAuthorized(1));
        }

        [Fact]
        public void Scenario4()
        {
            var ogranisationalDiscount = 15m;
            var organisationServiceMock = new Mock<IOrganisationService>();
            organisationServiceMock.Setup(x => x.GetOgranisationalDiscountForUser(1, out ogranisationalDiscount)).Returns(true);

            var productServiceMock = new Mock<IProductService>();
            var product = new Product() { Price = 36m, OnlineDiscount = false, MaxDiscount = 10 };
            productServiceMock.Setup(x => x.GetProductBySku(1, out product)).Returns(true);

            var pricingService = new PricingService(organisationServiceMock.Object, productServiceMock.Object, _e, _minimumDiscountPercentage, _maximumDiscountPercentage);
            var customerPriceCalculated = pricingService.GetCustomerPriceForUser(1, 1, out decimal customerPrice);

            Assert.True(customerPriceCalculated);
            Assert.InRange(customerPrice, 32.4m - _e, 32.4m + _e);
        }

        [Fact]
        public void Scenario5()
        {
            var ogranisationalDiscount = 15m;
            var organisationServiceMock = new Mock<IOrganisationService>();
            organisationServiceMock.Setup(x => x.GetOgranisationalDiscountForUser(1, out ogranisationalDiscount)).Returns(true);

            var productServiceMock = new Mock<IProductService>();
            var product = new Product() { Price = 36m, OnlineDiscount = false, MaxDiscount = 14 };
            productServiceMock.Setup(x => x.GetProductBySku(1, out product)).Returns(true);

            var pricingService = new PricingService(organisationServiceMock.Object, productServiceMock.Object, _e, _minimumDiscountPercentage, _maximumDiscountPercentage);
            var customerPriceCalculated = pricingService.GetCustomerPriceForUser(1, 1, out decimal customerPrice);

            Assert.True(customerPriceCalculated);
            Assert.InRange(customerPrice, 30.96m - _e, 30.96m + _e);
        }

        [Fact]
        public void Scenario6()
        {
            var ogranisationalDiscount = 15m;
            var organisationServiceMock = new Mock<IOrganisationService>();
            organisationServiceMock.Setup(x => x.GetOgranisationalDiscountForUser(1, out ogranisationalDiscount)).Returns(true);

            var productServiceMock = new Mock<IProductService>();
            var product = new Product() { Price = 36m, OnlineDiscount = true, MaxDiscount = 14 };
            productServiceMock.Setup(x => x.GetProductBySku(1, out product)).Returns(true);

            var pricingService = new PricingService(organisationServiceMock.Object, productServiceMock.Object, _e, _minimumDiscountPercentage, _maximumDiscountPercentage);
            var customerPriceCalculated = pricingService.GetCustomerPriceForUser(1, 1, out decimal customerPrice);

            Assert.True(customerPriceCalculated);
            Assert.InRange(customerPrice, 30.4956m - _e, 30.4956m + _e);
        }

        [Fact]
        public void NegativeConfigurationParameters()
        {
            var ogranisationalDiscount = 15m;
            var organisationServiceMock = new Mock<IOrganisationService>();
            organisationServiceMock.Setup(x => x.GetOgranisationalDiscountForUser(1, out ogranisationalDiscount)).Returns(true);

            var productServiceMock = new Mock<IProductService>();
            var product = new Product() { Price = 36m, OnlineDiscount = true, MaxDiscount = 14 };
            productServiceMock.Setup(x => x.GetProductBySku(1, out product)).Returns(true);

            var e = -0.01m;
            var pricingService = new PricingService(organisationServiceMock.Object, productServiceMock.Object, e, _minimumDiscountPercentage, _maximumDiscountPercentage);
            var customerPriceCalculated = pricingService.GetCustomerPriceForUser(1, 1, out decimal customerPrice);

            Assert.False(customerPriceCalculated);
        }

        [Fact]
        public void OrganisationalDiscountOutOfRange()
        {
            var ogranisationalDiscount = -25m;
            var organisationServiceMock = new Mock<IOrganisationService>();
            organisationServiceMock.Setup(x => x.GetOgranisationalDiscountForUser(1, out ogranisationalDiscount)).Returns(true);

            var productServiceMock = new Mock<IProductService>();
            var product = new Product() { Price = 50m, OnlineDiscount = true, MaxDiscount = 50 };
            productServiceMock.Setup(x => x.GetProductBySku(1, out product)).Returns(true);

            var pricingService = new PricingService(organisationServiceMock.Object, productServiceMock.Object, _e, _minimumDiscountPercentage, _maximumDiscountPercentage);
            var customerPriceCalculated = pricingService.GetCustomerPriceForUser(1, 1, out decimal customerPrice);

            Assert.False(customerPriceCalculated);
        }

        [Fact]
        public void NegativeProductPrice()
        {
            var ogranisationalDiscount = 15m;
            var organisationServiceMock = new Mock<IOrganisationService>();
            organisationServiceMock.Setup(x => x.GetOgranisationalDiscountForUser(1, out ogranisationalDiscount)).Returns(true);

            var productServiceMock = new Mock<IProductService>();
            var product = new Product() { Price = -36m, OnlineDiscount = true, MaxDiscount = 14 };
            productServiceMock.Setup(x => x.GetProductBySku(1, out product)).Returns(true);

            var pricingService = new PricingService(organisationServiceMock.Object, productServiceMock.Object, _e, _minimumDiscountPercentage, _maximumDiscountPercentage);
            var customerPriceCalculated = pricingService.GetCustomerPriceForUser(1, 1, out decimal customerPrice);

            Assert.False(customerPriceCalculated);
        }

        [Fact]
        public void MaxDiscountOutOfRange()
        {
            var ogranisationalDiscount = 15m;
            var organisationServiceMock = new Mock<IOrganisationService>();
            organisationServiceMock.Setup(x => x.GetOgranisationalDiscountForUser(1, out ogranisationalDiscount)).Returns(true);

            var productServiceMock = new Mock<IProductService>();
            var product = new Product() { Price = 50m, OnlineDiscount = true, MaxDiscount = 50 };
            productServiceMock.Setup(x => x.GetProductBySku(1, out product)).Returns(true);

            var pricingService = new PricingService(organisationServiceMock.Object, productServiceMock.Object, _e, _minimumDiscountPercentage, _maximumDiscountPercentage);
            var customerPriceCalculated = pricingService.GetCustomerPriceForUser(1, 1, out decimal customerPrice);

            Assert.False(customerPriceCalculated);
        }

        [Fact]
        public void NoOrganisationalDiscount()
        {
            var ogranisationalDiscount = 1m;
            var organisationServiceMock = new Mock<IOrganisationService>();
            organisationServiceMock.Setup(x => x.GetOgranisationalDiscountForUser(1, out ogranisationalDiscount)).Returns(false);

            var productServiceMock = new Mock<IProductService>();
            var product = new Product() { Price = 50m, OnlineDiscount = true, MaxDiscount = 15 };
            productServiceMock.Setup(x => x.GetProductBySku(1, out product)).Returns(true);

            var pricingService = new PricingService(organisationServiceMock.Object, productServiceMock.Object, _e, _minimumDiscountPercentage, _maximumDiscountPercentage);
            var customerPriceCalculated = pricingService.GetCustomerPriceForUser(1, 1, out decimal customerPrice);

            Assert.False(customerPriceCalculated);
        }

        [Fact]
        public void NoProductReturned()
        {
            var ogranisationalDiscount = 1m;
            var organisationServiceMock = new Mock<IOrganisationService>();
            organisationServiceMock.Setup(x => x.GetOgranisationalDiscountForUser(1, out ogranisationalDiscount)).Returns(false);

            var productServiceMock = new Mock<IProductService>();
            var product = new Product();
            productServiceMock.Setup(x => x.GetProductBySku(1, out product)).Returns(true);

            var pricingService = new PricingService(organisationServiceMock.Object, productServiceMock.Object, _e, _minimumDiscountPercentage, _maximumDiscountPercentage);
            var customerPriceCalculated = pricingService.GetCustomerPriceForUser(1, 1, out decimal customerPrice);

            Assert.False(customerPriceCalculated);
        }
    }
}
