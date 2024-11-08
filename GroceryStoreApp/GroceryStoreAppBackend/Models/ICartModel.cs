using System.Data;

namespace GroceryStoreApp.Models
{
    public interface ICartModel
    {
        public bool addItemToCart(int productID, int userId);
        public bool removeItemFromCart(int productID, int userId);
        public DataTable getItemsFromCart(int userId);
        public bool checkout(int userId);
        public decimal getPrice(int userId);

    }
}