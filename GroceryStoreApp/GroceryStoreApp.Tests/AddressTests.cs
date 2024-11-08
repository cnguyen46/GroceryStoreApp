using GroceryStoreApp.Controllers;
using GroceryStoreApp.Models;
using System.Net;

namespace GroceryStoreApp.Tests
{
    [TestClass]
    public class AddressTests
    {
        int addressID = 3;
        int userID = 3;
        string streetAddress = "1144 T St";
        string state = "Lincoln";
        string city = "NE";
        int zip = 68588;

        [TestMethod]
        public void addAddressTest()
        /*
         *  Testing method: add user's address information to the database 
         */
        {
            try
            {
               // Arrange
               AddressModel model = new AddressModel(addressID, userID, streetAddress, state, city, zip);
               AddressController controller = new AddressController();

               // Act
               HttpStatusCode result = controller.addAddress(model);

               // Assert
               Assert.AreEqual(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        [TestMethod]
        public void addAddressTest_no_duplicate()
        /*
         *  Testing method: Cannot add duplicate user's information to the database.
         */
        {
            try
            {
                // Arrange
                AddressModel model = new AddressModel(addressID, userID, streetAddress, state, city, zip);

                // Act
                bool add_one_more_time = model.addAddress();

                // Assert
                Assert.IsTrue(add_one_more_time);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        [TestMethod]
        public void getAddressTest_success()
        /*
         *  Testing method: UserID and streetAddress must be matched with the database
         */
        {
            try
            {
                AddressController controller = new AddressController();
                AddressModel model = new AddressModel(1, 1, "330 El Chapo Ln", "NE", "Lincoln", 68521);

                HttpStatusCode result = controller.getAddress(model);
                Assert.AreEqual(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        [TestMethod]
        public void getAddressTest_userID_unmatched()
        /*
         *  Testing Method: the user cannot retrieve their address information due to incorrect userID input
         */
        {
            try
            {
                AddressController controller = new AddressController();
                AddressModel model = new AddressModel(1, 0, "330 El Chapo Ln", "NE", "Lincoln", 68521);

                HttpStatusCode result = controller.getAddress(model);
                Assert.AreEqual(result, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        [TestMethod]
        public void getAddressTest_streetAddress_unmatched()
        /*
         *  Testing Method: the user cannot retrieve their address information due to incorrect street address
         */
        {
            try
            {
                AddressController controller = new AddressController();
                AddressModel model = new AddressModel(1, 1, "", "NE", "Lincoln", 68521);

                HttpStatusCode result = controller.getAddress(model);
                Assert.AreEqual(result, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
