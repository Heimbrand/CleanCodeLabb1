using Microsoft.AspNetCore.Mvc;
using WebShop.Models;
using WebShop.UnitOfWork;
using WebShopSolution.Sql.Entities;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Endpoint för att hämta alla produkter
        [HttpGet]
        public ActionResult<IEnumerable<DtoProduct>> GetProducts()
        {
            // Behöver använda repository via Unit of Work för att hämta produkter
            return Ok();
        }

        // Endpoint för att lägga till en ny produkt
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
           if (product is null)
               return BadRequest("Product is null");

           try
           {
                _unitOfWork.Products.AddAsync(product);
           }
           catch (Exception e)
           {
               return StatusCode(500, $"Internal server error: {e.Message}");
           }

            return Ok();
        }
    }
}
