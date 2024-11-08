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
    public class AddressController : ControllerBase, IAddressController
    {
        [HttpPost("addAddress")]

        public HttpStatusCode addAddress(AddressModel address)
        {
            if (address.addAddress())
            {
                return HttpStatusCode.OK;
            }
            else
            {
                return HttpStatusCode.BadRequest; 
            }
        }

        [HttpPost("getAddress")]
        public HttpStatusCode getAddress(AddressModel address)
        {
            /* 
             * Needs to call addressModel so that the addressModel can check to see if there is a user with corresponding userID and address 
             */

            if (address.getAddress())
            {
                return HttpStatusCode.OK;
            }
            else
            {
                return HttpStatusCode.BadRequest;
            }
        }
            
        
    }
}
