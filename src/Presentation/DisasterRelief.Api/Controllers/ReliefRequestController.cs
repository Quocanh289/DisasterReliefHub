using DisasterRelief.Application.DTOs;
using DisasterRelief.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DisasterRelief.Api.Controllers;

[Authorize]
public sealed class ReliefRequestController(ReliefRequestService requestService) : BaseApiController
{
    [HttpGet]
    public Task<IReadOnlyList<ReliefRequestDto>> GetAll(CancellationToken cancellationToken)
        => requestService.GetAllAsync(cancellationToken);

    [HttpPost]
    public async Task<IActionResult> Create(CreateReliefRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.RequesterName) || string.IsNullOrWhiteSpace(request.Phone)
            || !double.IsFinite(request.Longitude) || !double.IsFinite(request.Latitude)
            || request.Longitude is < -180 or > 180 || request.Latitude is < -90 or > 90)
        {
            return BadRequest(new { message = "Provide contact details and valid longitude/latitude." });
        }

        var created = await requestService.CreateAsync(request, cancellationToken);
        return Created($"/api/reliefrequest/{created.Id}", created);
    }
}