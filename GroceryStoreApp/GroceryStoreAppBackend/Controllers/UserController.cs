using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using GroceryStoreApp.Models;
using System.Net;

namespace GroceryStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase,  IUserController
    {
        public HttpStatusCode addUser(UserModel user)
        {
            if (user.addUser())
            {
                return HttpStatusCode.OK;
            }
            else
            {
                return HttpStatusCode.BadRequest;
            }
        }

        public HttpStatusCode getUser(UserModel user)
        {

            if (user.getUser())
            {
                return HttpStatusCode.OK;
            }
            else
            {
                return HttpStatusCode.BadRequest;
            }
        }

        [HttpPost("getUser")]
        public JsonResult returnUser(UserModel user)
        {
            if(getUser(user) == HttpStatusCode.OK)
            {
                return (user.toJson());
            } else
            {
                return new JsonResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("addUser")]
        public JsonResult createUser(UserModel user)
        {
            if (addUser(user) == HttpStatusCode.OK)
            {
                return (user.toJson());
            }
            else
            {
                return new JsonResult(HttpStatusCode.BadRequest);
            }
        }
    }
}
