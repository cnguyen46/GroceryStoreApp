using GroceryStoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GroceryStoreApp.Controllers
{
    public interface ICartController
    {
        public JsonResult getCartItems(int userId);

        public HttpStatusCode addItemToCart(ProductModel product, int userId);

        public HttpStatusCode removeItemFromCart(ProductModel product, int userId);
            
        public JsonResult checkout(int userId);

        public JsonResult getPrice(int userId);


    }
}