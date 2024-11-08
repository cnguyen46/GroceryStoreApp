using GroceryStoreApp.Models;

namespace GroceryStoreApp.Tests
{
    [TestClass]
    public class PaymentTests
    {
        [TestMethod]
        public void getPaymentTest()
        /*
         * Testing method: Get payment information inside the database.
         * This test is for viewing the user's profile on the web page.
         */
        {
            PaymentMethodModel model = new PaymentMethodModel(2, 2, "Chief Keef", "4444555566667777", new DateTime(2024, 07, 06), 456);
            
            bool result = model.getPaymentMethod();

            Assert.IsTrue(result);
        }
    }
}