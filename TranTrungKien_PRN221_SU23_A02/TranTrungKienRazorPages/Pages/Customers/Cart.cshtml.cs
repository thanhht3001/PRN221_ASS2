using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System;
using Newtonsoft.Json;

using TranTrungKienRazorPages.Utils;
using DataAccess.Repositories;
using BusinessObject.Models;
using BusinessObject;

namespace TranTrungKienRazorPages.Pages.Customers
{
    [Authorize(Roles = nameof(eRole.Customer))]
    public class CartModel : PageModel
    {
        [JsonIgnore]
        public IList<CartItem> cart { get; set; }
        private readonly IFlowerRepo repo = new FlowerRepo();
        private readonly IOrderRepo orderRepo = new OrderRepo();

        public CartModel() { }

        public decimal Total { get; set; }
        private static readonly JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public IActionResult OnGetAsync()
        {
            var cartJsonFromSession = SessionUtils.getFromJson<string>(HttpContext.Session, "cart");
            if (cartJsonFromSession != null)
            {
                cart = JsonConvert.DeserializeObject<List<CartItem>>(cartJsonFromSession, settings);
            }

            if (cart == null)
            {
                cart = new List<CartItem>();
            }
            Total = cart.Sum(i => i.FlowerBouquet.UnitPrice * i.Quantity);

            return Page();
        }

        public IActionResult OnGetAddToCart(int id)
        {
            try
            {
                var cartJsonFromSession = SessionUtils.getFromJson<string>(HttpContext.Session, "cart");
                if (cartJsonFromSession != null)
                {
                    cart = JsonConvert.DeserializeObject<List<CartItem>>(cartJsonFromSession, settings);
                }

                var flowerBouquetObject = repo.GetFlower(id);
                if (flowerBouquetObject == null)
                {
                    return NotFound();
                }

                if (cart == null)
                {
                    cart = new List<CartItem>
                    {
                        new CartItem
                        {
                            FlowerBouquet = flowerBouquetObject,
                            Quantity = 1
                        }
                    };
                    var cartJson = JsonConvert.SerializeObject(cart, settings);
                    SessionUtils.setAsJson(HttpContext.Session, "cart", cartJson);
                }
                else
                {
                    int index = doExistInList(cart, id);
                    if (index == -1)
                    {
                        cart.Add(new CartItem
                        {
                            FlowerBouquet = flowerBouquetObject,
                            Quantity = 1
                        });
                    }
                    else
                    {
                        cart[index].Quantity++;
                    }
                    var cartJson = JsonConvert.SerializeObject(cart, settings);
                    SessionUtils.setAsJson(HttpContext.Session, "cart", cartJson);
                }
                Total = cart.Sum(i => i.FlowerBouquet.UnitPrice * i.Quantity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return Page();
        }

        public IActionResult OnPostCheckout()
        {
            try
            {
                var cartJsonFromSession = SessionUtils.getFromJson<string>(HttpContext.Session, "cart");
                if (cartJsonFromSession != null)
                {
                    cart = JsonConvert.DeserializeObject<List<CartItem>>(cartJsonFromSession, settings);
                }

                if (cart == null)
                {
                    cart = new List<CartItem>();
                }
                if (cart.Count == 0)
                {
                    return Page();
                }

                Total = cart.Sum(i => i.FlowerBouquet.UnitPrice * i.Quantity);
                Random rand = new Random();
                int orderId = rand.Next(1, 5000);
                var order = new Order
                {
                    OrderId = orderId,
                    OrderDate = DateTime.Now,
                    OrderStatus = "Checked Out",
                    ShippedDate = DateTime.Now.AddDays(1),
                    CustomerId = HttpContext.getAccountId(),
                    Total = Total
                };
                orderRepo.Save(order);

                for (var i = 0; i < cart.Count(); i++)
                {
                    IOrderDetailRepo orderDetailRepo = new OrderDetailRepo();
                    var orderDetail = new OrderDetail
                    {
                        OrderId = orderId,
                        FlowerBouquetId = cart[i].FlowerBouquet.FlowerBouquetId,
                        Quantity = cart[i].Quantity,
                        Discount = 0,
                        UnitPrice = cart[i].Quantity * cart[i].FlowerBouquet.UnitPrice
                    };
                    orderDetailRepo.Save(orderDetail);
                }
                HttpContext.Session.Remove("cart");
                return RedirectToPage("./OrderHistory");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Page();
            }
        }

        public IActionResult OnPostUpdate(int[] quantities)
        {
            var cartJsonFromSession = SessionUtils.getFromJson<string>(HttpContext.Session, "cart");
            if (cartJsonFromSession != null)
            {
                cart = JsonConvert.DeserializeObject<List<CartItem>>(cartJsonFromSession, settings);
            }

            for (var i = 0; i < cart.Count; i++)
            {
                cart[i].Quantity = quantities[i];
            }
            var cartJson = JsonConvert.SerializeObject(cart, settings);
            SessionUtils.setAsJson(HttpContext.Session, "cart", cartJson);
            Total = cart.Sum(i => i.FlowerBouquet.UnitPrice * i.Quantity);

            return Page();
        }

        public IActionResult OnGetDelete(int id)
        {
            var cartJsonFromSession = SessionUtils.getFromJson<string>(HttpContext.Session, "cart");
            if (cartJsonFromSession != null)
            {
                cart = JsonConvert.DeserializeObject<List<CartItem>>(cartJsonFromSession, settings);
            }

            int index = doExistInList(cart, id);
            cart.RemoveAt(index);

            var cartJson = JsonConvert.SerializeObject(cart, settings);
            SessionUtils.setAsJson(HttpContext.Session, "cart", cartJson);
            Total = cart.Sum(i => i.FlowerBouquet.UnitPrice * i.Quantity);

            return Page();
        }

        private int doExistInList(IList<CartItem> cart, int id)
        {
            for (var i = 0; i < cart.Count; i++)
            {
                if (cart[i].FlowerBouquet.FlowerBouquetId == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
