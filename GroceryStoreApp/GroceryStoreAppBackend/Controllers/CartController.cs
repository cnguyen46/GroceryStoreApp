using GroceryStoreApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GroceryStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase, ICartController
    {
        private readonly CartModel model;
        public CartController()
        {
            model = new CartModel();
        }
        [HttpPost("addItemToCart")]
        public HttpStatusCode addItemToCart(ProductModel productModel, int userId)
        {
            if (model.addItemToCart(productModel.ProductId, userId))
            {
                return HttpStatusCode.OK;
            }
            else
            {
                return HttpStatusCode.BadRequest;
            }
        
        }

        [HttpPost("checkout")]
        public JsonResult checkout(int userId)
        {
            return new JsonResult(model.checkout(userId));
        }

        [HttpGet("getCartItems")]
        public JsonResult getCartItems(int userId)
        {
            return new JsonResult(model.getItemsFromCart(userId));
        }
        [HttpGet("getPrice")]
        public JsonResult getPrice(int userId)
        {
            decimal x = model.getPrice(userId);
            return new JsonResult(new { price = x });
        }

        [HttpPost("removeItemFromCart")]
        public HttpStatusCode removeItemFromCart(ProductModel product, int userId)
        {
            if (model.removeItemFromCart(product.ProductId, userId))
            {
                return HttpStatusCode.OK;
            }
            else
            {
                return HttpStatusCode.BadRequest;

            }
        }

    }
}
