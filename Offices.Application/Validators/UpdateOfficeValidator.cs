using FluentValidation;
using Offices.Application.DTOs;

namespace Offices.Application.Validators;

public class UpdateOfficeValidator : AbstractValidator<UpdateOfficeDTO>
{
    public UpdateOfficeValidator()
    {
        RuleFor(x => x.City)
            .NotEmpty().WithMessage("Please, enter the office’s city");

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Please, enter the office’s street");

        RuleFor(x => x.HouseNumber)
            .NotEmpty().WithMessage("Please, enter the office’s house number");

        RuleFor(x => x.RegistryPhoneNumber)
            .NotEmpty().WithMessage("Please, enter the phone number")
            .Matches(@"^\+[0-9]+$").WithMessage("You've entered an invalid phone number"); 

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid status value");
    }
}