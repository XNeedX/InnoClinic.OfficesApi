using Offices.Application.DTOs;
using Offices.Application.DTOs.Pagination; 
using Offices.Application.Results;

namespace Offices.Application.Abstractions;

public interface IOfficeService
{
    Task<Result<OfficeResponseDTO>> CreateOfficeAsync(CreateOfficeDto request);
    Task<Result<OfficeResponseDTO>> GetOfficeByIdAsync(Guid id);
    Task<Result<PagedResult<OfficeResponseDTO>>> GetAllOfficesAsync(PageParams pageParams);
    Task<Result<OfficeResponseDTO>> UpdateOfficeAsync(Guid id, UpdateOfficeDTO request);
    Task<Result> ChangeOfficeStatusAsync(Guid id, ChangeOfficeStatusDTO request);
}