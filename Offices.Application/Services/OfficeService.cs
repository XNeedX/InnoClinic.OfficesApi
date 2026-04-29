using Offices.Application.Abstractions;
using Offices.Application.DTOs;
using Offices.Application.DTOs.Pagination;
using Offices.Application.Mappings;
using Offices.Application.Results;
using Offices.Domain.Models;

namespace Offices.Application.Services;

public class OfficeService : IOfficeService
{
    private readonly IRepository<Office> _officeRepository;

    public OfficeService(IRepository<Office> officeRepository)
    {
        _officeRepository = officeRepository;
    }

    public async Task<Result<OfficeResponseDTO>> CreateOfficeAsync(CreateOfficeDto request)
    {
        var office = request.ToEntity();
        office.Id = Guid.NewGuid();

        await _officeRepository.AddAsync(office);

        var responseDto = office.ToResponseDTO();
        return Result<OfficeResponseDTO>.Success(responseDto);
    }

    public async Task<Result<OfficeResponseDTO>> GetOfficeByIdAsync(Guid id)
    {
        var office = await _officeRepository.GetByIdAsync(id);

        if (office == null)
            return Result<OfficeResponseDTO>.Failure(OfficeErrors.NotFound);

        var responseDto = office.ToResponseDTO();
        return Result<OfficeResponseDTO>.Success(responseDto);
    }

    public async Task<Result<PagedResult<OfficeResponseDTO>>> GetAllOfficesAsync(PageParams pageParams)
    {
        var pagedOffices = await _officeRepository.GetAllAsync(pageParams);

        var responseList = pagedOffices.Items.ToResponseDTOAll();
        var pagedResult = new PagedResult<OfficeResponseDTO>(responseList, pagedOffices.TotalCount);

        return Result<PagedResult<OfficeResponseDTO>>.Success(pagedResult);
    }

    public async Task<Result<OfficeResponseDTO>> UpdateOfficeAsync(Guid id, UpdateOfficeDTO request)
    {
        var office = await _officeRepository.GetByIdAsync(id);

        if (office == null)
            return Result<OfficeResponseDTO>.Failure(OfficeErrors.NotFound);
        
        request.UpdateEntity(office);

        await _officeRepository.UpdateAsync(office);

        var responseDto = office.ToResponseDTO();
        return Result<OfficeResponseDTO>.Success(responseDto);
    }

    public async Task<Result> ChangeOfficeStatusAsync(Guid id, ChangeOfficeStatusDTO request)
    {
        var office = await _officeRepository.GetByIdAsync(id);

        if (office == null)
            return Result.Failure(OfficeErrors.NotFound);

        office.Status = request.Status;

        await _officeRepository.UpdateAsync(office);

        return Result.Success();
    }
}