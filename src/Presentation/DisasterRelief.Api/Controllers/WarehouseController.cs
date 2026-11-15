using DisasterRelief.Application.DTOs;
using DisasterRelief.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DisasterRelief.Api.Controllers;

[Authorize]
public sealed class WarehouseController(WarehouseService warehouseService) : BaseApiController
{
    [HttpGet]
    public Task<IReadOnlyList<WarehouseDto>> GetAll(CancellationToken cancellationToken)
        => warehouseService.GetAllAsync(cancellationToken);

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var warehouse = await warehouseService.GetByIdAsync(id, cancellationToken);
        return warehouse is null ? NotFound() : Ok(warehouse);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateWarehouseRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || request.Capacity < 0 || !IsValidCoordinate(request.Longitude, request.Latitude))
        {
            return BadRequest(new { message = "Provide a name, non-negative capacity, and valid longitude/latitude." });
        }

        var warehouse = await warehouseService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = warehouse.Id }, warehouse);
    }

    private static bool IsValidCoordinate(double longitude, double latitude)
        => double.IsFinite(longitude) && double.IsFinite(latitude) && longitude is >= -180 and <= 180 && latitude is >= -90 and <= 90;
}