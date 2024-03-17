using Billing.Data;
using FluentValidation;

namespace Billing.Validators
{
    public class OrderItemValidator: AbstractValidator<OrderItem>
    {
        public OrderItemValidator()
        {
            RuleFor(o => o.FeatureName).NotNull().NotEmpty().WithMessage(o=> $"{o.FeatureName} Invalid Feature");
            RuleFor(o => o.Quantity).Must((o, list, context) =>
            {
                if (o.Quantity.ToString() != "")
                {
                    context.MessageFormatter.AppendArgument("Quantity", o.Quantity);
                    return Int32.TryParse(o.Quantity.ToString(), out int number);
                }
                return true;
            })
            .WithMessage(o => $"{o.Quantity} must be a number.")
            .GreaterThan(0).WithMessage(o => $"{o.Quantity} Invalid Input");
        }
    }
}
