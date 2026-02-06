using Core.Application.DTOs;
using FluentValidation;

namespace Core.Application.Validators;

public class StudentValidator : AbstractValidator<StudentDto>
{
    public StudentValidator()
    {
        RuleFor(x => x.StudentCode)
            .NotEmpty().WithMessage("Student code is required.")
            .Length(9).WithMessage("Student code must be exactly 9 characters long.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Student First Name is required.")
            .MaximumLength(60).WithMessage("Student First Name cannot exceed 60 characters.");

        RuleFor(x => x.Lastname)
            .NotEmpty().WithMessage("Student Last Name is required.")
            .MaximumLength(60).WithMessage("Student Last Name cannot exceed 60 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address is required.");

        RuleFor(x => x.CareerStart)
            .NotEmpty().WithMessage("Career start date is required.");
    }
}
