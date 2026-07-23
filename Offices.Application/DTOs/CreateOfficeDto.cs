using Offices.Domain.Models;

namespace Offices.Application.DTOs;

public record CreateOfficeDto(
    string? PhotoPath,
    string City,
    string Street,
    string HouseNumber,
    string OfficeNumber, 
    string RegistryPhoneNumber,
    OfficeStatus Status = OfficeStatus.Active
);