using Billing.Helpers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIllingServiceTests
{
    internal class BillingMockFactoryData
    {
        public static IOptions<TierSettings> GetTierSettings() 
        { 
            var setting = new TierSettings 
            {
                DesignImplants = new DesignImplant
                {
                    Tier5 = 29.99,
                    Tier10 = 34.99
                },
                PrintImplants = new PrintImplant 
                {
                    Tier25 = 49.99,
                    Tier30 = 59.99
                }
            };

            return Options.Create(setting);
        }

        public static IOptions<TierQuantity> GetTierQuantity() 
        {
            var setting = new TierQuantity
            {
                DesignImplantLower = 5,
                PrintImplantLower = 25,
            };

            return Options.Create(setting);
        }
    }
}
