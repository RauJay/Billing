using Billing.Data;
using Billing.Entity;

namespace Billing.Services
{
    public interface IBillingService
    {
        Task<double> ComputeBill(Usage usage);
    }
}
