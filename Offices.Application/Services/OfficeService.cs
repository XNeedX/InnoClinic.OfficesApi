using InnoClinic.Contracts.Events.Offices;
using MassTransit;
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
    private readonly IPublishEndpoint _publishEndpoint;

    public OfficeService(IRepository<Office> officeRepository, IPublishEndpoint publishEndpoint)
    {
        _officeRepository = officeRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Result<OfficeResponseDTO>> CreateOfficeAsync(CreateOfficeDto request)
    {
        var office = request.ToEntity();
        office.Id = Guid.NewGuid();

        await _officeRepository.AddAsync(office);

        var responseDto = office.ToResponseDTO();

        await _publishEndpoint.Publish<IOfficeCreatedEvent>(new
        {
            Id = office.Id,
            Address = office.FullAddress,
            PhotoPath = office.PhotoPath,
            City = office.City,
            Street = office.Street,
            HouseNumber = office.HouseNumber,
            OfficeNumber = office.OfficeNumber,
            RegistryPhoneNumber = office.RegistryPhoneNumber,
            Status = (InnoClinic.Contracts.Enums.OfficeStatus)office.Status
        });

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

        await _publishEndpoint.Publish<IOfficeUpdatedEvent>(new
        {
            Id = office.Id,
            PhotoPath = office.PhotoPath,
            City = office.City,
            Street = office.Street,
            HouseNumber = office.HouseNumber,
            OfficeNumber = office.OfficeNumber,
            RegistryPhoneNumber = office.RegistryPhoneNumber,
            Status = (InnoClinic.Contracts.Enums.OfficeStatus)office.Status
        });

        return Result<OfficeResponseDTO>.Success(responseDto);
    }

    public async Task<Result> ChangeOfficeStatusAsync(Guid id, ChangeOfficeStatusDTO request)
    {
        var office = await _officeRepository.GetByIdAsync(id);

        if (office == null)
            return Result.Failure(OfficeErrors.NotFound);

        office.Status = request.Status;

        await _officeRepository.UpdateAsync(office);

        await _publishEndpoint.Publish<IOfficeStatusUpdatedEvent>(new
        {
            Id = office.Id,
            Status = (InnoClinic.Contracts.Enums.OfficeStatus)office.Status
        });

        return Result.Success();
    }
}