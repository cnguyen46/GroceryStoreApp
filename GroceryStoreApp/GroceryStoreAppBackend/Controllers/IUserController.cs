using GroceryStoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GroceryStoreApp.Controllers
{
    public interface IUserController
    {
        HttpStatusCode addUser(UserModel user);

        HttpStatusCode getUser(UserModel user);
    }
}
