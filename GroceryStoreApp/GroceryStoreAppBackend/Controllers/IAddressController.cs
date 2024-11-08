using GroceryStoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GroceryStoreApp.Controllers
{
    public interface IAddressController
    {
        HttpStatusCode addAddress(AddressModel address);

        HttpStatusCode getAddress(AddressModel address);
    }
}
