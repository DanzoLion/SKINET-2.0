using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;                               // ActionResult
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    [ApiController]                                                                                 // endpoint attribute
    [Route("api/[controller]")]                                                               // route attribute
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext __context;
        public ProductsController(StoreContext _context)                        // gives us access to the StoreContext.cs entity
        {
            __context = _context;
        }

        [HttpGet]                                                                                       // returns below string via https://localhost:5001/api/products/
        public async Task<ActionResult<List<Product>>> GetProducts()                   // we return ActionResult that is some form of Http resonse ie OK 200 / 400etc
        {                                                                                                                       // Task passes off our request to a delegate  // prevents current thread from being blocked
           // return "this will be a list of products";
           var products = await __context.Products.ToListAsync();                          // a query that goes to our database .. ToList(); executes select query and returns results in our var producs variable
           return Ok(products);
        }

        [HttpGet("{id}")]                                                                           // differentiates our attribute via {id} // https://localhost:5001/api/products/123
        public async Task<ActionResult<Product>> GetProduct(int id)                                                    // id is our product id
        {
           // return "single product";
           return await __context.Products.FindAsync(id);                       // id returns our products form the database 
        }
    }
}