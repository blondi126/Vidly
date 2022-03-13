using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Vidly.Dtos;
using Vidly.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using Vidly.Data;
using System.Linq;
using System;

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomersController(IMapper mapper)
        {
            _context = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

            _mapper = mapper;
        }

        // GET /api/customers
        [HttpGet]
        public IActionResult GetCustomers(string? query = null)
        {
            var customersQuery = _context.Customers
                .Include(c => c.MembershipType);

            if (!String.IsNullOrWhiteSpace(query))

                customersQuery = customersQuery
                    .Where(c => c.Name.Contains(query))
                    .Include(c => c.MembershipType);
            
                var customerDto = customersQuery
                .ToList()
                .Select(_mapper.Map<Customer, CustomerDto>);

            return Ok(customerDto); 
        }

        //GET /api/customers/{id}
        [HttpGet("{id:int}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();
            return Ok(_mapper.Map<Customer, CustomerDto>(customer));
        }

        // POST /api/customers
        [HttpPost]
        public IActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(); 

            var customer = _mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

           return Created(new Uri(Request.GetDisplayUrl() + "/" + customer.Id),customerDto);
        }

        // PUT /api/customers/{id}
        [HttpPut("{id:int}")]
        public IActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {   
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            
            _mapper.Map(customerDto, customerInDb);
            
            _context.SaveChanges();

            return Ok(); 
        }

        // DELETE /api/customers/{id}
        [HttpDelete("{id:int}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c=>c.Id == id);

            if (customerInDb == null)
                return NotFound();

            _context.Customers.Remove(customerInDb);

            _context.SaveChanges();

            return Ok();
        }
    }
}
