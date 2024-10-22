using Airport.Service.Queries;
using FluentValidation;

namespace Airport.Service.Validations
{
    public class AirportQueryValidator : AbstractValidator<AirportRequest>
    {
        public AirportQueryValidator()
        {
            RuleFor(x => x.FirstAirportIATA).NotEmpty().NotNull().Length(3).WithMessage("First IATA is not null and length 3");
            RuleFor(x => x.SecondAirportIATA).NotEmpty().NotNull().Length(3).WithMessage("First IATA is not null and length 3");
        }
    }
}
