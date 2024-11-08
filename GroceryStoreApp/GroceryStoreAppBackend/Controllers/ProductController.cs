using GroceryStoreApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase, IProductController
    {
        private IProductModel productModel;
        public ProductController()
        {
            productModel = new ProductModel(0, null, null, null, null, null, 0, null, null, null, 0);
        }

        [HttpGet("getProducts")]
        public JsonResult GetProducts()
        {
            return new JsonResult(productModel.getProductsFromDatabase());
        }

        [HttpPost("getProductsByCategory")]
        public JsonResult GetProductsByCategory(ProductModel product)
        {
            return new JsonResult(product.getProductsFromDatabaseByCategory());
        }

        [HttpPost("getProductsById")]
        public JsonResult GetProductsById(ProductModel product)
        {
            return new JsonResult(product.getProductsFromDatabaseById());
        }
    }
}
