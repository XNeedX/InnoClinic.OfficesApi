using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Offices.Application.Abstractions;
using Offices.Application.DTOs;
using Offices.Application.DTOs.Pagination;
using Offices.Application.Results;

namespace Offices.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OfficesController : ApiController
{
    private readonly IOfficeService _officeService;
    private readonly IValidator<CreateOfficeDto> _createValidator;
    private readonly IValidator<UpdateOfficeDTO> _updateValidator;
    private readonly IValidator<ChangeOfficeStatusDTO> _changeStatusValidator;

    public OfficesController(
        IOfficeService officeService,
        IValidator<CreateOfficeDto> createValidator,
        IValidator<UpdateOfficeDTO> updateValidator,
        IValidator<ChangeOfficeStatusDTO> changeStatusValidator)
    {
        _officeService = officeService;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _changeStatusValidator = changeStatusValidator;
    }

    [HttpPost]
    // [Authorize(Roles = "Receptionist")] 
    public async Task<IActionResult> CreateOffice([FromBody] CreateOfficeDto request)
    {
        var validationResult = await _createValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return HandleValidationFailure(validationResult.ToDictionary());

        var result = await _officeService.CreateOfficeAsync(request);

        if (result.IsFailure)
            return HandleFailure(result.Error);

        return HandleCreationResult
        (
            result,
            nameof(GetOfficeByIdAsync), 
            new { id = result.Value?.Id } 
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOfficeByIdAsync(Guid id)
    {
        var result = await _officeService.GetOfficeByIdAsync(id);

        if (result.IsFailure)
            return HandleFailure(result.Error);

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOfficesAsync([FromQuery] PageParams pageParams)
    {
        var result = await _officeService.GetAllOfficesAsync(pageParams);

        if (result.IsFailure)
            return HandleFailure(result.Error);

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateOffice(Guid id, [FromBody] UpdateOfficeDTO request)
    {
        var validationResult = await _updateValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return HandleValidationFailure(validationResult.ToDictionary());

        var result = await _officeService.UpdateOfficeAsync(id, request);

        if (result.IsFailure)
            return HandleFailure(result.Error);

        return Ok(result.Value);
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] ChangeOfficeStatusDTO request)
    {
        var validationResult = await _changeStatusValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return HandleValidationFailure(validationResult.ToDictionary());

        var result = await _officeService.ChangeOfficeStatusAsync(id, request);

        return HandleResult(result);
    }
}