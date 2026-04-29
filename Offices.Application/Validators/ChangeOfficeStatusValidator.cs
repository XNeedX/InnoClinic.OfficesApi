using FluentValidation;
using Offices.Application.DTOs;

namespace Offices.Application.Validators;

public class ChangeOfficeStatusValidator : AbstractValidator<ChangeOfficeStatusDTO>
{
    public ChangeOfficeStatusValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid status value");
    }
}