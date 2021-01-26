using FluentValidation;
using KBS.Cities.Application.CQRS.Cities;
using System;

namespace KBS.Cities.Application.Validators
{
    public class CityValidator : AbstractValidator<AddOrUpdateRequest>
    {
        public CityValidator()
        {
            RuleFor(d => d.Data.Name)
                .MinimumLength(1).WithMessage("Length cannot be less than 1.").OverridePropertyName("name");

            RuleFor(d => d.Data.Population)
                .GreaterThanOrEqualTo(0).WithMessage("Population count can't be less than zero.").OverridePropertyName("population");

            RuleFor(d => d.Data.Established.Date)
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Established date cannot be in the future").OverridePropertyName("established");
        }
    }
}
