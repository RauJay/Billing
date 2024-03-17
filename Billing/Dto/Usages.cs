using Billing.Data;

namespace Billing.Entity
{
    public class Usage
    {
        public IEnumerable<OrderItem> Usages { get; set; }
    }
}
