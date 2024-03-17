using Billing.Data;
using Billing.Entity;
using FluentValidation;

namespace Billing.Validators
{
    public class UsageValidator: AbstractValidator<Usage>
    {
        public UsageValidator()
        {
            RuleForEach(model => model.Usages).SetValidator(new OrderItemValidator());
        }
    }
}
