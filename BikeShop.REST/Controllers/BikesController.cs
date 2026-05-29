using BikeShop.Common;
using BikeShop.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.REST.Controllers;

[ApiController]
[Route("api/bikes")]
public class BikesController : ControllerBase
{
    private readonly CrudService<BikeModel> _bikeService;

    public BikesController(CrudService<BikeModel> bikeService)
    {
        _bikeService = bikeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BikeModel>>> GetAll()
    {
        var bikes = await _bikeService.ReadAllAsync();

        return Ok(bikes);
    }

    [HttpGet("page/{page:int}/amount/{amount:int}")]
    public async Task<ActionResult<IEnumerable<BikeModel>>> GetPage(int page, int amount)
    {
        if (page <= 0 || amount <= 0)
        {
            return BadRequest("Page and amount must be greater than zero.");
        }

        var bikes = await _bikeService.ReadAllAsync(page, amount);

        return Ok(bikes);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BikeModel>> GetById(Guid id)
    {
        var bike = await _bikeService.ReadAsync(id);

        if (bike == null)
        {
            return NotFound();
        }

        return Ok(bike);
    }

    [HttpPost]
    public async Task<ActionResult<BikeModel>> Create(BikeModel bike)
    {
        bike.Id = Guid.NewGuid();

        await _bikeService.CreateAsync(bike);

        return CreatedAtAction(nameof(GetById), new { id = bike.Id }, bike);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, BikeModel bike)
    {
        var existingBike = await _bikeService.ReadAsync(id);

        if (existingBike == null)
        {
            return NotFound();
        }

        existingBike.Name = bike.Name;
        existingBike.Brand = bike.Brand;
        existingBike.FrameSize = bike.FrameSize;
        existingBike.WheelSize = bike.WheelSize;
        existingBike.Price = bike.Price;

        await _bikeService.UpdateAsync(existingBike);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var bike = await _bikeService.ReadAsync(id);

        if (bike == null)
        {
            return NotFound();
        }

        await _bikeService.RemoveAsync(bike);

        return NoContent();
    }
}