using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class CartService
    {
        public void AddToCart(int productId, int quantity)
        {
            using ProductApplicationContext db = new ProductApplicationContext();
            {
                var cartItem = db.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

                if (cartItem != null)
                {
                    cartItem.Quantity += quantity;
                }
                else
                {
                    cartItem = new CartItem
                    {
                        ProductId = productId,
                        Quantity = quantity
                    };
                    db.CartItems.Add(cartItem);
                }

                db.SaveChanges();
            }
        }

        public void RemoveFromCart(int productId)
        {
            using ProductApplicationContext db = new ProductApplicationContext();
            {
                var cartItem = db.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
                if (cartItem != null)
                {
                    db.CartItems.Remove(cartItem);
                    db.SaveChanges();
                }
            }
        }

        internal List<CartItem> GetCartItems()
        {
            using ProductApplicationContext db = new ProductApplicationContext();
            {
                db.Database.EnsureCreated();
                return db.CartItems.ToList();
            }
        }
    }
}