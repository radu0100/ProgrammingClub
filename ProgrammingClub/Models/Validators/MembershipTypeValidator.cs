using FluentValidation;
using ProgrammingClub.CQRS.DTOs;


namespace ProgrammingClub.Models.Validators
{
    public class MembershipTypeValidator : AbstractValidator<MembershipTypeDto>
    {
        public MembershipTypeValidator()
        {
            RuleFor(mt => mt.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
            RuleFor(mt => mt.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
            RuleFor(mt => mt.SubscriptionLengthInMonths)
                .GreaterThan(0).WithMessage("Subscription length must be greater than zero.");
        }
    }
}
