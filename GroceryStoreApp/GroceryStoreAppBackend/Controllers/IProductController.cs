using GroceryStoreApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStoreApp.Controllers
{
    public interface IProductController
    {

        public JsonResult GetProducts();
        public JsonResult GetProductsById(ProductModel product);
        public JsonResult GetProductsByCategory(ProductModel product);

    }
}
