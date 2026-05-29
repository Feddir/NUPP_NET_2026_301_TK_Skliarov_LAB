using BikeShop.Common;
using BikeShop.Infrastructure.Models;
using BikeShop.REST.Models;
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
    public async Task<ActionResult<IEnumerable<BikeResponseModel>>> GetAll()
    {
        var bikes = await _bikeService.ReadAllAsync();

        var response = bikes.Select(bike => new BikeResponseModel
        {
            Id = bike.Id,
            Name = bike.Name,
            Brand = bike.Brand,
            FrameSize = bike.FrameSize,
            WheelSize = bike.WheelSize,
            Price = bike.Price
        });

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BikeResponseModel>> GetById(Guid id)
    {
        var bike = await _bikeService.ReadAsync(id);

        if (bike == null)
        {
            return NotFound();
        }

        var response = new BikeResponseModel
        {
            Id = bike.Id,
            Name = bike.Name,
            Brand = bike.Brand,
            FrameSize = bike.FrameSize,
            WheelSize = bike.WheelSize,
            Price = bike.Price
        };

        return Ok(response);
    }

    [HttpGet("page/{page:int}/amount/{amount:int}")]
    public async Task<ActionResult<IEnumerable<BikeResponseModel>>> GetPage(int page, int amount)
    {
        if (page <= 0 || amount <= 0)
        {
            return BadRequest("Page and amount must be greater than zero.");
        }

        var bikes = await _bikeService.ReadAllAsync(page, amount);

        var response = bikes.Select(bike => new BikeResponseModel
        {
            Id = bike.Id,
            Name = bike.Name,
            Brand = bike.Brand,
            FrameSize = bike.FrameSize,
            WheelSize = bike.WheelSize,
            Price = bike.Price
        });

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<BikeResponseModel>> Create(BikeCreateModel model)
    {
        var bike = new BikeModel
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Brand = model.Brand,
            FrameSize = model.FrameSize,
            WheelSize = model.WheelSize,
            Price = model.Price
        };

        await _bikeService.CreateAsync(bike);

        var response = new BikeResponseModel
        {
            Id = bike.Id,
            Name = bike.Name,
            Brand = bike.Brand,
            FrameSize = bike.FrameSize,
            WheelSize = bike.WheelSize,
            Price = bike.Price
        };

        return CreatedAtAction(nameof(GetById), new { id = bike.Id }, response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, BikeCreateModel model)
    {
        var bike = await _bikeService.ReadAsync(id);

        if (bike == null)
        {
            return NotFound();
        }

        bike.Name = model.Name;
        bike.Brand = model.Brand;
        bike.FrameSize = model.FrameSize;
        bike.WheelSize = model.WheelSize;
        bike.Price = model.Price;

        await _bikeService.UpdateAsync(bike);

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