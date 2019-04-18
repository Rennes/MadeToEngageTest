using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Logging;
using EPiServer.ServiceLocation;

namespace MadeToEngageTest.Business.Services
{
    [ServiceConfiguration(ServiceType = typeof(IPricingService))]
    public class PricingService : IPricingService
    {
        private readonly IOrganisationService _iOrganisationService;
        private readonly IProductService _iProductService;

        private readonly decimal _e;
        private readonly decimal _minimumDiscountPercentage;
        private readonly decimal _maximumDiscountPercentage;

        private readonly ILogger Log = LogManager.GetLogger();

        public PricingService(IOrganisationService iOrganisationService,
            IProductService iProductService, decimal e, decimal minimumDiscountPercentage, decimal maximumDiscountPercentage)
        {
            _iOrganisationService = iOrganisationService;
            _iProductService = iProductService;

            _e = e;
            _minimumDiscountPercentage = minimumDiscountPercentage;
            _maximumDiscountPercentage = maximumDiscountPercentage;
        }

        public bool GetCustomerPriceForUser(int userId, int productSku, out decimal customerPrice)
        {
            customerPrice = 0;

            if (!CheckConfigurationParameters())
            {
                return false;
            }

            if (!_iOrganisationService.GetOgranisationalDiscountForUser(userId, out decimal organisationalDiscount))
            {
                Log.Error("Couldnt get organisational discount for user");
                return false;
            }

            if (organisationalDiscount < _minimumDiscountPercentage || organisationalDiscount > _maximumDiscountPercentage || Math.Abs(organisationalDiscount % 0.5m) > _e)
            {
                Log.Error("Organisational discount is out of allowed range");
                return false;
            }

            if (!_iProductService.GetProductBySku(productSku, out Product product))
            {
                Log.Error("Couldnt get product by sku");
                return false;
            }

            if (product.Price < 0)
            {
                Log.Error("Product price cant be negative");
                return false;
            }

            if (product.MaxDiscount.HasValue && product.MaxDiscount < _minimumDiscountPercentage || product.MaxDiscount > _maximumDiscountPercentage)
            {
                Log.Error("Max discount is out of allowed range");
                return false;
            }

            customerPrice = product.Price;
            if (product.MaxDiscount.HasValue && product.MaxDiscount < organisationalDiscount)
            {
                customerPrice *= 1 - (product.MaxDiscount.Value / 100);
            }
            else
            {
                customerPrice *= 1 - (organisationalDiscount / 100);
            }

            if (product.OnlineDiscount)
            {
                customerPrice *= (1m - 0.015m);
            }

            return true;
        }

        private bool CheckConfigurationParameters()
        {
            if (_e < 0)
            {
                Log.Error("Error margin cant be negative");
                return false;
            }

            if (_minimumDiscountPercentage < 0)
            {
                Log.Error("Minimum discount cant be negative");
                return false;
            }

            if (_minimumDiscountPercentage >= _maximumDiscountPercentage)
            {
                Log.Error("Maximum discount cant be equal or less than minimum discount");
                return false;
            }

            return true;
        }
    }
}