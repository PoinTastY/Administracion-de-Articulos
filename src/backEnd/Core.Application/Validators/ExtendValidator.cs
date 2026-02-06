using Core.Application.DTOs;
using FluentValidation;

namespace Core.Application.Validators
{
    public class ExtendValidator : AbstractValidator<ExtendRequestDto>
    {
        public ExtendValidator()
        {
            RuleFor(x => x.StudentCode)
                .NotEmpty().WithMessage("StudentCode is required.")
                .Length(9).WithMessage("StudentCode must be exactly 9 characters long.");
            RuleFor(x => x.Article)
                .NotEmpty().WithMessage("Article is required.")
                .InclusiveBetween(33, 35).WithMessage("Article must be between 33 and 35.");
            RuleFor(x => x.EvidenceFileUrl)
                .NotEmpty().WithMessage("EvidenceFileUrl is required.")
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("EvidenceFileUrl must be a valid URL.");
            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("Reason is required.")
                .MaximumLength(420).WithMessage("Reason cannot exceed 420 characters.");
        }
    }
}
