namespace Offices.Application.DTOs;

public record OfficeResponseDTO(
    Guid Id,
    string? PhotoPath, 
    string FullAddress,
    string RegistryPhoneNumber,
    string Status
);
