using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryStoreApp;
using GroceryStoreApp.Controllers;
using GroceryStoreApp.Models;
using Moq;
using Microsoft.AspNetCore.Builder;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;
using Newtonsoft.Json;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace GroceryStoreApp.Tests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void addItemToCartTest()
        /*
         * Test Method : Method to add product into a cart for one user.
         * This test is for purchasing product.
         */
        {
            try
            {
                // Arange
                int userId = 1;
                int productId = 1;
                CartModel modelCart = new CartModel();
                CartController controllerCart = new CartController();
                ProductModel modelProduct = new ProductModel(1, "54861385315", "Apple", "Red Apple", "Sun Orchard",
                "Fruit", 2.29, "~/Resources/images/fruit/apple.jfif", "4x4x4 in", "0.5 lbs", 5);
                // Act
                bool resultModel = modelCart.addItemToCart(productId, userId);
                HttpStatusCode resultControl = controllerCart.addItemToCart(modelProduct, userId);

                // Assert
                Assert.IsTrue(resultModel);
                Assert.AreEqual(resultControl, HttpStatusCode.OK);
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        [TestMethod]
        public void removeItemFromCartTest()
        /*
         * Test Method : Method to remove product from a cart for one user.
         * This test is for purchasing product.
         * Assume the user does not have any products in the cart, so the user cannot remove anything.
         */
        {
            try
            {
                // Arange
                int userId = 1;
                int productId = 1;
                CartModel modelCart = new CartModel();
                CartController controllerCart = new CartController();
                ProductModel modelProduct = new ProductModel(1, "54861385315", "Apple", "Red Apple", "Sun Orchard",
                "Fruit", 2.29, "~/Resources/images/fruit/apple.jfif", "4x4x4 in", "0.5 lbs", 5);
                
                // Act
                bool resultModel = modelCart.removeItemFromCart(productId, userId);
                HttpStatusCode resultControl = controllerCart.removeItemFromCart(modelProduct, userId);

                // Assert
                Assert.IsFalse(resultModel);
                Assert.AreEqual(resultControl, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        [TestMethod]
        public void getItemFromCartTest()
        /*
         * Test Method : Method to see all product in a cart for one user.
         * This test is for purchasing product.
         * At the start, the cart is empty, so there is not products showed in the cart.
         */
        {
            try
            {
                // Arange
                int userId = 1;
                CartModel model = new CartModel();
                CartController controller = new CartController();
                
                // Act
                DataTable resultModel = model.getItemsFromCart(userId);
                JsonResult resultControl = controller.getCartItems(userId);
                string resultJson = JsonConvert.SerializeObject(resultControl.Value);
                string expected = "[]";

                // Assert
                Assert.IsNotNull(resultModel);
                Assert.AreEqual(resultJson, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        [TestMethod]
        public void getPriceTest()
        /*
         * Test Method :  Method to see the price of all product in a cart for one user.
         * At the start, there is no product, so the price should be 0.
         * This test is for purchasing product.
         */
        {
            try
            {
                // Arange
                int userId = 1;
                CartModel model = new CartModel();
                CartController controller = new CartController();
                // Act

                decimal resultModel = model.getPrice(userId);
                JsonResult resultControl = controller.getPrice(userId);
                string resultJson = JsonConvert.SerializeObject(resultControl.Value);
                string expected = "{\"price\":0.0}";

                // Assert
                Assert.AreEqual(resultModel, 0);
                Assert.AreEqual(resultJson, expected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        [TestMethod]
        public void checkOutTest()
        /*
         * Test Method : Method to delete all cart from one user after they check out.
         * This test is for purchasing product.
         */
        {
            try
            {
                // Arange
                int userId = 1;
                CartModel model = new CartModel();
                CartController controller = new CartController();

                // Act
                bool resultModel = model.checkout(userId);
                JsonResult resultControl = controller.checkout(userId);
                
                //Assert
                Assert.IsTrue(resultModel);
                Assert.IsNull(resultControl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}