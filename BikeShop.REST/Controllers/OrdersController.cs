using BikeShop.Common;
using BikeShop.Infrastructure.Models;
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
    public async Task<ActionResult<IEnumerable<OrderModel>>> GetAll()
    {
        var orders = await _orderService.ReadAllAsync();

        return Ok(orders);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderModel>> GetById(Guid id)
    {
        var order = await _orderService.ReadAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<OrderModel>> Create(OrderModel order)
    {
        var bike = await _bikeService.ReadAsync(order.BikeId);

        if (bike == null)
        {
            return BadRequest("Bike with this Id does not exist.");
        }

        var customer = await _customerService.ReadAsync(order.CustomerId);

        if (customer == null)
        {
            return BadRequest("Customer with this Id does not exist.");
        }

        order.Id = Guid.NewGuid();
        order.OrderDate = DateTime.UtcNow;
        order.UnitPrice = bike.Price;

        await _orderService.CreateAsync(order);

        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, OrderModel order)
    {
        var existingOrder = await _orderService.ReadAsync(id);

        if (existingOrder == null)
        {
            return NotFound();
        }

        var bike = await _bikeService.ReadAsync(order.BikeId);

        if (bike == null)
        {
            return BadRequest("Bike with this Id does not exist.");
        }

        var customer = await _customerService.ReadAsync(order.CustomerId);

        if (customer == null)
        {
            return BadRequest("Customer with this Id does not exist.");
        }

        existingOrder.Quantity = order.Quantity;
        existingOrder.BikeId = order.BikeId;
        existingOrder.CustomerId = order.CustomerId;
        existingOrder.UnitPrice = bike.Price;

        await _orderService.UpdateAsync(existingOrder);

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