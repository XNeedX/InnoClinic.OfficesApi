using Offices.Application.DTOs;
using Offices.Domain.Models;
using Riok.Mapperly.Abstractions;

namespace Offices.Application.Mappings;

[Mapper]
public static partial class OfficeMapper
{
    [MapperIgnoreTarget(nameof(Office.Id))]
    public static partial Office ToEntity(this CreateOfficeDto request);
    public static partial OfficeResponseDTO ToResponseDTO(this Office office);
    public static partial IEnumerable<OfficeResponseDTO> ToResponseDTOAll(this IEnumerable<Office> offices);
    public static partial Office UpdateEntity(this UpdateOfficeDTO request, Office office);
}