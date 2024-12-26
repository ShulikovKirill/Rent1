using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class ProductService
    {
        public List<Product> GetAllProducts()
        {
            
            using ProductApplicationContext db = new ProductApplicationContext();
            db.Database.EnsureCreated();
            return db.Products.ToList();
        }

        public void AddProduct(Product product)
        {
            using ProductApplicationContext db = new ProductApplicationContext();
            db.Products.Add(product);
            db.SaveChanges();
        }

        public void RemoveProduct(int productId)
        {
            using ProductApplicationContext db = new ProductApplicationContext();
            Product product = null;

            foreach (Product item in db.Products)
            {
                if (item.Id == productId)
                {
                    product = item;
                    break;
                }
            }

            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
        }
    }
}
