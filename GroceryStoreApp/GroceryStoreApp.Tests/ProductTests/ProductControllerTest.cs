using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryStoreApp;
using GroceryStoreApp.Controllers;
using GroceryStoreApp.Models;
using Moq;
using Microsoft.AspNetCore.Builder;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStoreApp.Tests
{
    [TestClass]
    public class ProductControllerTest
    {

        [TestMethod]
        public void TestMethod()
        {
            //TODO Doesn't work correctly just shows how to do it
            ProductController controller = new ProductController();

            JsonResult result = controller.GetProducts();

            Assert.IsNotNull(result);
        }
    }
}