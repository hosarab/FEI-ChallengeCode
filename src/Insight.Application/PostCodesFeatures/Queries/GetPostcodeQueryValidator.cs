using FluentValidation;

namespace Insight.Application.PostCodesFeatures.Queries
{
    public class GetPostcodeQueryValidator : AbstractValidator<GetPostcodeQuery>
    {
        public GetPostcodeQueryValidator()
        {
            RuleFor(x => x.PostCode)
                .NotEmpty()
                .NotNull()
                .WithMessage("The postcode is mandatory");

        }
    }
}
