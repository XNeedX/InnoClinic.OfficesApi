using Offices.Application.Abstractions;
using Offices.Application.DTOs;
using Offices.Application.Mappings;
using Offices.Domain.Models;
using Offices.Application.Results;

namespace Offices.Application.Services;

public class OfficeService : IOfficeService
{
    private readonly IRepository<Office> _officeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OfficeService(IRepository<Office> officeRepository, IUnitOfWork unitOfWork)
    {
        _officeRepository = officeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<OfficeResponseDTO>> CreateOfficeAsync(CreateOfficeDto request)
    {
        var office = request.ToEntity();
        office.Id = Guid.NewGuid();

        await _officeRepository.AddAsync(office);
        await _unitOfWork.SaveChangesAsync();

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

    public async Task<Result<IEnumerable<OfficeResponseDTO>>> GetAllOfficesAsync()
    {
        var offices = await _officeRepository.GetAllAsync();

        var responseList = offices.ToResponseDTOAll();

        return Result<IEnumerable<OfficeResponseDTO>>.Success(responseList);
    }

    public async Task<Result<OfficeResponseDTO>> UpdateOfficeAsync(Guid id, UpdateOfficeDTO request)
    {
        var office = await _officeRepository.GetByIdAsync(id);

        if (office == null)
            return Result<OfficeResponseDTO>.Failure(OfficeErrors.NotFound);

        office.PhotoPath = request.PhotoPath;
        office.City = request.City;
        office.Street = request.Street;
        office.HouseNumber = request.HouseNumber;
        office.OfficeNumber = request.OfficeNumber;
        office.RegistryPhoneNumber = request.RegistryPhoneNumber;
        office.Status = request.Status;

        await _officeRepository.UpdateAsync(office);
        await _unitOfWork.SaveChangesAsync();

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

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}