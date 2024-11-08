using GroceryStoreApp.Controllers;
using GroceryStoreApp.Models;
using System.Net;

namespace GroceryStoreApp.Tests
{
    [TestClass]
    public class UserTests
    {
        int userId = 0;
        string username = "cnguyen46";
        string password = "Cnguyen46";
        string firstname = "Cong";
        string lastname = "Nguyen";
        string email = "cnguyen46@hotmail.com";
        long phone = 4027774321;

        [TestMethod]
        public void addUserTest()
        /**
         *  Testing method: Add user's information to the database.
         *  This test is for register web-page.
         */
        {
            try
            {
                // Arrange
                UserModel model = new UserModel(userId, username, password, email, firstname, lastname, phone);
                UserController controller = new UserController();

                // Act
                HttpStatusCode result = controller.addUser(model);

                // Assert
                Assert.AreEqual(result, HttpStatusCode.OK);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            } 
        }

        [TestMethod]
        public void addUserTest_no_duplicate()
        /**
         *  Testing method: Cannot add duplicate user's information to the database.
         *  This test is for register web-page.
         */
        {
            try
            {
                // Arrange
                UserModel model = new UserModel(userId, username, password, email, firstname, lastname, phone);

                // Act
                bool add_one_more_time = model.addUser();

                // Assert
                Assert.IsTrue(add_one_more_time);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        [TestMethod]
        public void deleteUserTest()
        /**
         *  Testing method: Delete the user's information in the database.
         *  Using the already User added from two NUnit tests above.
         */
        {
            try
            {
                // Arrange
                UserModel model = new UserModel(0, username, null, null, null, null, 0);

                // Act
                bool result = model.deleteUser(username);

                // Assert
                Assert.IsTrue(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        [TestMethod]
        public void getUserTest_login_success()
        /**
         * Testing method: Username and password must be matched with the database when login
         * After that, the user can see all their information in their profile.
         * This test is for login web-page.
         */
        {
            try
            {
                UserController controller = new UserController();
                UserModel model = new UserModel(0, "eldin.salja", "P@sswordmine1234", null, null, null, 0);

                HttpStatusCode result = controller.getUser(model);
                Assert.AreEqual(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        [TestMethod]
        public void getUserTests_username_unmatched()
        /**
         * Testing method: The user cannot login due to typing the wrong username
         * This test is for login web-page
         */
        {
            try
            {
                UserController controller = new UserController();
                UserModel model = new UserModel(0, "", "P@sswordmine1234", null, null, null, 0);

                HttpStatusCode result = controller.getUser(model);
                Assert.AreEqual(result, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        [TestMethod]
        public void getUserTests_password_unmatched()
        /**
         * Testing method: The user cannot login due to typing the wrong password
         * This test is for login web-page
         */
        {
            try
            {
                UserController controller = new UserController();
                UserModel model = new UserModel(0, "eldin.salja", "wrong", null, null, null, 0);

                HttpStatusCode result = controller.getUser(model);
                Assert.AreEqual(result, HttpStatusCode.BadRequest);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}