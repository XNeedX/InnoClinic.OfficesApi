using Offices.Application.Abstractions;

namespace Offices.Application.Results;

public static class OfficeErrors
{
    public static readonly Error CreationFailed = new("Office.CreationFailed", "Failed to create the office.");
    public static readonly Error InvalidPhoneNumber = new("Office.InvalidPhoneNumber", "You've entered an invalid phone number.", ErrorType.Validation);
    public static readonly Error NotFound = new("Office.NotFound", "Office not found.", ErrorType.NotFound);
}