using BikeShop.Common;
using BikeShop.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.REST.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly CrudService<CustomerModel> _customerService;

    public CustomersController(CrudService<CustomerModel> customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerModel>>> GetAll()
    {
        var customers = await _customerService.ReadAllAsync();

        return Ok(customers);
    }

    [HttpGet("page/{page:int}/amount/{amount:int}")]
    public async Task<ActionResult<IEnumerable<CustomerModel>>> GetPage(int page, int amount)
    {
        if (page <= 0 || amount <= 0)
        {
            return BadRequest("Page and amount must be greater than zero.");
        }

        var customers = await _customerService.ReadAllAsync(page, amount);

        return Ok(customers);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CustomerModel>> GetById(Guid id)
    {
        var customer = await _customerService.ReadAsync(id);

        if (customer == null)
        {
            return NotFound();
        }

        return Ok(customer);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerModel>> Create(CustomerModel customer)
    {
        customer.Id = Guid.NewGuid();

        if (customer.Profile != null)
        {
            customer.Profile.Id = Guid.NewGuid();
            customer.Profile.CustomerId = customer.Id;
        }

        await _customerService.CreateAsync(customer);

        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, CustomerModel customer)
    {
        var existingCustomer = await _customerService.ReadAsync(id);

        if (existingCustomer == null)
        {
            return NotFound();
        }

        existingCustomer.FullName = customer.FullName;
        existingCustomer.PhoneNumber = customer.PhoneNumber;
        existingCustomer.Email = customer.Email;
        existingCustomer.DiscountPercent = customer.DiscountPercent;

        await _customerService.UpdateAsync(existingCustomer);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var customer = await _customerService.ReadAsync(id);

        if (customer == null)
        {
            return NotFound();
        }

        await _customerService.RemoveAsync(customer);

        return NoContent();
    }
}