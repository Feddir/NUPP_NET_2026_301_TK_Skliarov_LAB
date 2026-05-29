using BikeShop.Common;
using BikeShop.Infrastructure.Models;
using BikeShop.REST.Models;
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
    public async Task<ActionResult<IEnumerable<CustomerResponseModel>>> GetAll()
    {
        var customers = await _customerService.ReadAllAsync();

        var response = customers.Select(customer => new CustomerResponseModel
        {
            Id = customer.Id,
            FullName = customer.FullName,
            PhoneNumber = customer.PhoneNumber,
            Email = customer.Email,
            DiscountPercent = customer.DiscountPercent,
            Address = customer.Profile?.Address,
            RegistrationDate = customer.Profile?.RegistrationDate
        });

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CustomerResponseModel>> GetById(Guid id)
    {
        var customer = await _customerService.ReadAsync(id);

        if (customer == null)
        {
            return NotFound();
        }

        var response = new CustomerResponseModel
        {
            Id = customer.Id,
            FullName = customer.FullName,
            PhoneNumber = customer.PhoneNumber,
            Email = customer.Email,
            DiscountPercent = customer.DiscountPercent,
            Address = customer.Profile?.Address,
            RegistrationDate = customer.Profile?.RegistrationDate
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerResponseModel>> Create(CustomerCreateModel model)
    {
        var customer = new CustomerModel
        {
            Id = Guid.NewGuid(),
            FullName = model.FullName,
            PhoneNumber = model.PhoneNumber,
            Email = model.Email,
            DiscountPercent = model.DiscountPercent,
            Profile = new CustomerProfileModel
            {
                Id = Guid.NewGuid(),
                Address = model.Address,
                RegistrationDate = DateTime.UtcNow
            }
        };

        await _customerService.CreateAsync(customer);

        var response = new CustomerResponseModel
        {
            Id = customer.Id,
            FullName = customer.FullName,
            PhoneNumber = customer.PhoneNumber,
            Email = customer.Email,
            DiscountPercent = customer.DiscountPercent,
            Address = customer.Profile.Address,
            RegistrationDate = customer.Profile.RegistrationDate
        };

        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, CustomerCreateModel model)
    {
        var customer = await _customerService.ReadAsync(id);

        if (customer == null)
        {
            return NotFound();
        }

        customer.FullName = model.FullName;
        customer.PhoneNumber = model.PhoneNumber;
        customer.Email = model.Email;
        customer.DiscountPercent = model.DiscountPercent;

        await _customerService.UpdateAsync(customer);

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