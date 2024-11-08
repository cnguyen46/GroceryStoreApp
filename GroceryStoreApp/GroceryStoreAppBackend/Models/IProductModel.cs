using System.Data;

namespace GroceryStoreApp.Models
{
    public interface IProductModel
    {
        public DataTable getProductsFromDatabase();
        public DataTable getProductsFromDatabaseById();
        public DataTable getProductsFromDatabaseByCategory();
    }
}
