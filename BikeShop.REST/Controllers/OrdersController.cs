using BikeShop.Common;
using BikeShop.Infrastructure.Models;
using BikeShop.REST.Models;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.REST.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly CrudService<OrderModel> _orderService;
    private readonly CrudService<BikeModel> _bikeService;
    private readonly CrudService<CustomerModel> _customerService;

    public OrdersController(
        CrudService<OrderModel> orderService,
        CrudService<BikeModel> bikeService,
        CrudService<CustomerModel> customerService)
    {
        _orderService = orderService;
        _bikeService = bikeService;
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderResponseModel>>> GetAll()
    {
        var orders = await _orderService.ReadAllAsync();

        var response = orders.Select(order => new OrderResponseModel
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            CustomerId = order.CustomerId,
            BikeId = order.BikeId,
            Quantity = order.Quantity,
            UnitPrice = order.UnitPrice,
            TotalPrice = order.UnitPrice * order.Quantity
        });

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderResponseModel>> GetById(Guid id)
    {
        var order = await _orderService.ReadAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        var response = new OrderResponseModel
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            CustomerId = order.CustomerId,
            BikeId = order.BikeId,
            Quantity = order.Quantity,
            UnitPrice = order.UnitPrice,
            TotalPrice = order.UnitPrice * order.Quantity
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<OrderResponseModel>> Create(OrderCreateModel model)
    {
        var bike = await _bikeService.ReadAsync(model.BikeId);

        if (bike == null)
        {
            return BadRequest("Bike with this Id does not exist.");
        }

        var customer = await _customerService.ReadAsync(model.CustomerId);

        if (customer == null)
        {
            return BadRequest("Customer with this Id does not exist.");
        }

        var order = new OrderModel
        {
            Id = Guid.NewGuid(),
            OrderDate = DateTime.UtcNow,
            CustomerId = model.CustomerId,
            BikeId = model.BikeId,
            Quantity = model.Quantity,
            UnitPrice = bike.Price
        };

        await _orderService.CreateAsync(order);

        var response = new OrderResponseModel
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            CustomerId = order.CustomerId,
            BikeId = order.BikeId,
            Quantity = order.Quantity,
            UnitPrice = order.UnitPrice,
            TotalPrice = order.UnitPrice * order.Quantity
        };

        return CreatedAtAction(nameof(GetById), new { id = order.Id }, response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, OrderCreateModel model)
    {
        var order = await _orderService.ReadAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        var bike = await _bikeService.ReadAsync(model.BikeId);

        if (bike == null)
        {
            return BadRequest("Bike with this Id does not exist.");
        }

        var customer = await _customerService.ReadAsync(model.CustomerId);

        if (customer == null)
        {
            return BadRequest("Customer with this Id does not exist.");
        }

        order.CustomerId = model.CustomerId;
        order.BikeId = model.BikeId;
        order.Quantity = model.Quantity;
        order.UnitPrice = bike.Price;

        await _orderService.UpdateAsync(order);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var order = await _orderService.ReadAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        await _orderService.RemoveAsync(order);

        return NoContent();
    }
}