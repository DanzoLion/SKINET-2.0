using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc; // ActionResult
using Microsoft.EntityFrameworkCore;

namespace API.Controllers {

    [ApiController] // endpoint attribute
    [Route ("api/[controller]")] // route attribute
    public class ProductsController : ControllerBase {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        //      private readonly StoreContext __context;                                   // replaced with instance of repo
        //  private readonly IProductRepository _repo;
        //    public ProductsController (IProductRepository repo) // gives us access to the StoreContext.cs entity replaced StoreContext with IProductRepository
        public ProductsController (IGenericRepository<Product> productsRepo, IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo, IMapper mapper) // gives us access to the StoreContext.cs entity replaced StoreContext with IProductRepository
        {
            _productsRepo = productsRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
            //    _repo = repo;
            //     __context = _context;                                                              // replaced with instance of rep
        }

        [HttpGet] // returns below string via https://localhost:5001/api/products/
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts () // we return ActionResult that is some form of Http resonse ie OK 200 / 400etc
        { // Task passes off our request to a delegate  // prevents current thread from being blocked
            // return "this will be a list of products";
        //    var products = await __context.Products.ToListAsync (); // a query that goes to our database .. ToList(); executes select query and returns results in our var producs variable
        //    var products = await _repo.GetProductsAsync(); // a query that goes to our database .. ToList(); executes select query and returns results in our var producs variable

            var spec = new ProductsWithTypesAndBrandsSpecification();   // this method hits our database

            var products = await _productsRepo.ListAsync(spec); // a query that goes to our database .. ToList(); executes select query and returns results in our var producs variable
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));  
            //return products.Select(product => new ProductToReturnDto  
        //     {                                                                                                       // at this point we already have our products in memory and we return from memory .. its not in the database
        //       Id = product.Id,
        //       Name = product.Name,
        //       Description = product.Description,
        //       PictureUrl = product.PictureUrl,
        //       Price = product.Price,
        //       ProductBrand = product.ProductBrand.Name,
        //       ProductType = product.ProductType.Name
        //   }).ToList();                                                                      // we return the properties we want from our product and return it into a list
        }

        [HttpGet ("{id}")] // differentiates our attribute via {id} // https://localhost:5001/api/products/123
        public async Task<ActionResult<ProductToReturnDto>> GetProduct (int id) // id is our product id
        {
            // return "single product";
          //  return await _repo.GetProductByIdAsync(id); // id returns our products form the database 

            var spec = new ProductsWithTypesAndBrandsSpecification(id);   // we pass in id from our root parameter above 
          //  return await _productsRepo.GetByIdAsync(id); // id returns our products form the database 
          //  return await _productsRepo.GetEntityWithSpec(spec); // implements the method from IGenericRepository
          var product = await _productsRepo.GetEntityWithSpec(spec); // implements the method from IGenericRepository
          return _mapper.Map<Product, ProductToReturnDto>(product); // we map from product to ProductToReturnDto and pass in product
        //   {
        //       Id = product.Id,
        //       Name = product.Name,
        //       Description = product.Description,
        //       PictureUrl = product.PictureUrl,
        //       Price = product.Price,
        //       ProductBrand = product.ProductBrand.Name,
        //       ProductType = product.ProductType.Name
        //   };
        }

        [HttpGet("brands")]

        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
           // return Ok(await _repo.GetProductBrandsAsync());                         // we wrap into Ok method as we are retrieving from an unsopported type
            return Ok(await _productBrandRepo.ListAllAsync());                         // we wrap into Ok method as we are retrieving from an unsopported type
        }

        [HttpGet("types")]

        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            //return Ok(await _repo.GetProductTypesAsync());                         // we wrap into Ok method as we are retrieving from an unsopported type
            return Ok(await _productTypeRepo.ListAllAsync());                         // we wrap into Ok method as we are retrieving from an unsopported type
        }

        
    }
}