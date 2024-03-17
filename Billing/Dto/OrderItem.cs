using Newtonsoft.Json.Converters;

namespace Billing.Data
{
    //TODO: Move to separate library to be shared with Testing Project.
    public class OrderItem
    {
        public Feature FeatureName { get; set; }

        public int Quantity { get; set; }
    }

    public enum Feature
    {
        DesignImplant = 1,
        PrintImplant = 2,
        UNK = -1
    }
}
