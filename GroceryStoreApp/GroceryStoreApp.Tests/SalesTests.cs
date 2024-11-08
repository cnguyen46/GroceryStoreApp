using GroceryStoreApp.Models;

namespace GroceryStoreApp.Tests
{
    [TestClass]
    public class SaleTests
    {
        [TestMethod]
        public void getSaleTest()
        /*
         * Testing method: Get sales information inside the database.
         * This test is for viewing the product on the web page.
         */
        {
            SaleModel model = new SaleModel(1, 1, 0.25m, new DateTime(2024, 03, 31), new DateTime(2024, 04, 10));

            bool result = model.getSale();

            Assert.IsTrue(result);
        }
    }
}
