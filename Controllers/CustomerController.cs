using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[Route("/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly DatabaseContext context;

    public CustomerController(DatabaseContext context)
    {
        this.context = context;
    }

    // Aqui está sendo feito um GET
    [HttpGet]

    public ActionResult<IEnumerable<Customer>> GetCustomer()
    {
        return this.context.Customers.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Customer> GetCustomer(int id)
    {
        var customer = this.context.Customers.Find(id);

        if (customer == null)
        {
            return NotFound();
        }

        return customer;
    }

    [HttpPost]
    public ActionResult<Customer> CreateCustomer(Customer customer)
    {
        if (customer == null)
        {
            return BadRequest();
        }

        this.context.Customers.Add(customer);
        this.context.SaveChanges();
        return CreatedAtAction(nameof(GetCustomer), new {id = customer.Id}, customer);
    }

    [HttpDelete("{id}")]
    public ActionResult<Customer> DeleteCustomer(int id)
    {
        var customer = this.context.Customers.Find(id);

        if (customer == null)
        {
            return NotFound();
        }

        this.context.Customers.Remove(customer);
        this.context.SaveChanges();

        return NoContent();
    }
}