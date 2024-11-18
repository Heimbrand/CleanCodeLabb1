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

        // Endpoint f�r att h�mta alla produkter
        [HttpGet]
        public ActionResult<IEnumerable<DtoProduct>> GetProducts()
        {
            // Beh�ver anv�nda repository via Unit of Work f�r att h�mta produkter
            return Ok();
        }

        // Endpoint f�r att l�gga till en ny produkt
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
