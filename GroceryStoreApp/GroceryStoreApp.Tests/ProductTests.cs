using GroceryStoreApp.Controllers;
using GroceryStoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GroceryStoreApp.Tests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void getProductsFromDatabaseTest()
        /*
         * Testing method: Get all products' information inside the database.
         * This test is for viewing the product on the web page.
         */
        {
            // Arange
            ProductController controller = new ProductController();
            JsonResult result = controller.GetProducts();
            
            // Act
            string resultJson = JsonConvert.SerializeObject(result.Value);
            string expectedJson =
                "[{\"ProductID\":1,\"SKU\":\"54861385315\",\"Name\":\"Apple\",\"Description\":\"Red Apple\",\"Manufacturer\":\"Sun Orchard\",\"Category\":\"Fruit\",\"Price\":2.29,\"Image\":\"~/Resources/images/fruit/apple.jfif\",\"Size\":\"4x4x4 in\",\"Weight\":\".5 lbs\",\"Rating\":5.0}," +
                "{\"ProductID\":2,\"SKU\":\"431835165135\",\"Name\":\"Banana\",\"Description\":\"Yellow Banana\",\"Manufacturer\":\"Dole\",\"Category\":\"Fruit\",\"Price\":3.19,\"Image\":\"~/Resources/images/fruit/banana.jfif\",\"Size\":\"8x2x2 in\",\"Weight\":\".2 lbs\",\"Rating\":4.8}," +
                "{\"ProductID\":3,\"SKU\":\"3548318355\",\"Name\":\"Grape\",\"Description\":\"Purple Grapes\",\"Manufacturer\":\"Grapeman Farms\",\"Category\":\"Fruit\",\"Price\":5.14,\"Image\":\"~/Resources/images/fruit/grape.jpeg\",\"Size\":\"1x1x1 in\",\"Weight\":\".02 lbs\",\"Rating\":3.9}," +
                "{\"ProductID\":4,\"SKU\":\"54861385315\",\"Name\":\"Spinach\",\"Description\":\"Fresh Spinach\",\"Manufacturer\":\"Green Farms\",\"Category\":\"Vegetable\",\"Price\":1.99,\"Image\":\"~/Resources/images/vegetables/spinach.jfif\",\"Size\":\"10x8x2 in\",\"Weight\":\"0.3 lbs\",\"Rating\":4.7}," +
                "{\"ProductID\":5,\"SKU\":\"431835165135\",\"Name\":\"Carrot\",\"Description\":\"Organic Carrot\",\"Manufacturer\":\"Harvest Farms\",\"Category\":\"Vegetable\",\"Price\":2.49,\"Image\":\"~/Resources/images/vegetables/carrot.jfif\",\"Size\":\"6x2x2 in\",\"Weight\":\"0.25 lbs\",\"Rating\":4.5}," +
                "{\"ProductID\":6,\"SKU\":\"3548318355\",\"Name\":\"Tomato\",\"Description\":\"Red Tomato\",\"Manufacturer\":\"Sunrise Gardens\",\"Category\":\"Vegetable\",\"Price\":0.99,\"Image\":\"~/Resources/images/vegetables/tomato.jfif\",\"Size\":\"3x3x3 in\",\"Weight\":\"0.1 lbs\",\"Rating\":4.8}," +
                "{\"ProductID\":7,\"SKU\":\"54861385315\",\"Name\":\"Orange Juice\",\"Description\":\"Fresh Orange Juice\",\"Manufacturer\":\"Tropicana\",\"Category\":\"Beverage\",\"Price\":3.99,\"Image\":\"~/Resources/images/beverages/orange_juice.jfif\",\"Size\":\"6x3x3 in\",\"Weight\":\"1.5 lbs\",\"Rating\":4.6}," +
                "{\"ProductID\":8,\"SKU\":\"431835165135\",\"Name\":\"Green Tea\",\"Description\":\"Organic Green Tea\",\"Manufacturer\":\"Arizona\",\"Category\":\"Beverage\",\"Price\":4.29,\"Image\":\"~/Resources/images/beverages/green_tea.jfif\",\"Size\":\"4x4x6 in\",\"Weight\":\"0.8 lbs\",\"Rating\":4.9}," +
                "{\"ProductID\":9,\"SKU\":\"3548318355\",\"Name\":\"Coffee\",\"Description\":\"Premium Coffee Beans\",\"Manufacturer\":\"Morning Brew\",\"Category\":\"Beverage\",\"Price\":8.99,\"Image\":\"~/Resources/images/beverages/coffee.jfif\",\"Size\":\"5x5x8 in\",\"Weight\":\"1.2 lbs\",\"Rating\":4.7}," +
                "{\"ProductID\":10,\"SKU\":\"54861385315\",\"Name\":\"Frozen Pizza\",\"Description\":\"Supreme Pizza\",\"Manufacturer\":\"Digiorno\",\"Category\":\"Frozen Food\",\"Price\":6.99,\"Image\":\"~/Resources/images/frozen/pizza.jfif\",\"Size\":\"12x12x2 in\",\"Weight\":\"1.8 lbs\",\"Rating\":4.5}," +
                "{\"ProductID\":11,\"SKU\":\"431835165135\",\"Name\":\"Frozen Berries\",\"Description\":\"Mixed Berries\",\"Manufacturer\":\"Dole\",\"Category\":\"Frozen Food\",\"Price\":5.49,\"Image\":\"~/Resources/images/frozen/berries.png\",\"Size\":\"8x4x2 in\",\"Weight\":\"0.6 lbs\",\"Rating\":4.8}," +
                "{\"ProductID\":12,\"SKU\":\"3548318355\",\"Name\":\"Frozen Dumplings\",\"Description\":\"Pork Dumplings\",\"Manufacturer\":\"Gyoza\",\"Category\":\"Frozen Food\",\"Price\":7.99,\"Image\":\"~/Resources/images/frozen/dumplings.jfif\",\"Size\":\"6x4x1 in\",\"Weight\":\"0.5 lbs\",\"Rating\":4.6}]"
                 ;

            // Assert
            Assert.AreEqual(expectedJson, resultJson);
        }

        [TestMethod]
        public void getProducts_category_test()
        /**
        *  Testing method: Get all products' information inside the database sorted by category.
        *  This test is for viewing the product on the web page.
        */
        {
            // Arange
            ProductController controller = new ProductController();
            ProductModel model = new ProductModel(1, "54861385315", "Apple", "Red Apple", "Sun Orchard",
                "Fruit", 2.29, "~/Resources/images/fruit/apple.jfif", "4x4x4 in", "0.5 lbs", 5);

            // Act
            JsonResult result = controller.GetProductsByCategory(model);
            string resultJson = JsonConvert.SerializeObject(result.Value);
            string expectedJson = "[]";

            // Assert
            Assert.AreEqual(expectedJson, resultJson);

        }

        [TestMethod]
        public void getProducts_ID_test()
        /**
        *  Testing method: Get all products' information inside the database sorted by product's id.
        *  This test is for viewing the product on the web page.
        */
        {
            // Arange
            ProductController controller = new ProductController();
            ProductModel model = new ProductModel(1, "54861385315", "Apple", "Red Apple", "Sun Orchard",
                "Fruit", 2.29, "~/Resources/images/fruit/apple.jfif", "4x4x4 in", "0.5 lbs", 5);

            // Act
            JsonResult result = controller.GetProductsById(model);
            string resultJson = JsonConvert.SerializeObject(result.Value);
            string expectedJson = "[{\"ProductID\":1," +
                "\"SKU\":\"54861385315\"," +
                "\"Name\":\"Apple\"," +
                "\"Description\":\"Red Apple\"," +
                "\"Manufacturer\":\"Sun Orchard\"," +
                "\"Category\":\"Fruit\"," +
                "\"Price\":2.29," +
                "\"Image\":\"~/Resources/images/fruit/apple.jfif\"," +
                "\"Size\":\"4x4x4 in\"," +
                "\"Weight\":\".5 lbs\"," +
                "\"Rating\":5.0}]";

            // Assert
            Assert.AreEqual(expectedJson, resultJson);

        }
    }
}