using Billing.Data;
using Billing.Entity;
using Billing.Helpers;
using Microsoft.Extensions.Options;

namespace Billing.Services
{
    //TODO: Move to separate library to be shared with Testing Project.
    public class BillingService : IBillingService
    {
        private readonly IOptions<TierSettings> _optionsTier;
        private readonly IOptions<TierQuantity> _optionsQt;

        public BillingService(IOptions<TierSettings> optionsTier, IOptions<TierQuantity> optionsQt)
        {
            _optionsTier = optionsTier;
            _optionsQt = optionsQt;
        }

        public async Task<double> ComputeBill(Usage usage)
        {
            var total = 0.0;
            foreach (var item in usage.Usages) 
            {
                if (item.FeatureName == Feature.DesignImplant)
                {
                    var designImplantBill = 0.0;
                    designImplantBill = await GetBillDesignImplant(item);
                    total += designImplantBill;
                }
                else if (item.FeatureName == Feature.PrintImplant) 
                {
                    var printImplantBill = 0.0;
                    printImplantBill = await GetBillPrintImplant(item);
                    total += printImplantBill;
                }
            }
            total = (double)Math.Round(total, 2);
            return await Task.Run(() => total);
        }

        private async Task<double> GetBillDesignImplant(OrderItem orderItem)
        {
            var total = 0.0;
            if (orderItem.Quantity > _optionsQt.Value.DesignImplantLower)
            {
                total = _optionsQt.Value.DesignImplantLower * _optionsTier.Value.DesignImplants.Tier5 
                        + (orderItem.Quantity - _optionsQt.Value.DesignImplantLower) * _optionsTier.Value.DesignImplants.Tier10;
            }
            else 
            {
                total = orderItem.Quantity * _optionsTier.Value.DesignImplants.Tier5;
            }
            return await Task.Run(() => total);
        }

       private async Task<double> GetBillPrintImplant(OrderItem orderItem)
        {
            var total = 0.0;
            if (orderItem.Quantity > _optionsQt.Value.PrintImplantLower)
            {
                total = _optionsQt.Value.PrintImplantLower * _optionsTier.Value.PrintImplants.Tier25 
                        + (orderItem.Quantity - _optionsQt.Value.PrintImplantLower) * _optionsTier.Value.PrintImplants.Tier30;
            }
            else
            {
                total = orderItem.Quantity * _optionsTier.Value.PrintImplants.Tier25;
            }
            return await Task.Run(() => total);
        }
    }
}
