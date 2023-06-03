using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessObject;
using HoTanThanhRazorPages.Helpers;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using Microsoft.AspNetCore.Http;
using HoTanThanhRazorPages.ViewModels;
using Repository.FlowerBouquetRepo;
using Newtonsoft.Json;
using Repository.OrderRepo;
using Repository.OrderDetailRepo;

namespace HoTanThanhRazorPages.Pages.CustomerPages
{
    public class CartModel : PageModel
    {
        [JsonIgnore]
        public IList<CartItem> cart { get; set; }
        private readonly IFlowerRepo repo = new FlowerRepo();
        private readonly IOrderRepo orderRepo = new OrderRepo();

        public decimal Total { get; set; }
        public CartModel() { }
        private static readonly JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public IActionResult OnGetBuyNow(int id)
        {
            try
            {
                var cartJsonFromSession = SessionHelper.GetObjectFromJson<string>(HttpContext.Session, "cart");
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
                    cart = new List<CartItem>();
                    cart.Add(new CartItem
                    {
                        FlowerBouquet = flowerBouquetObject,
                        Quantity = 1
                    });
                    var cartJson = JsonConvert.SerializeObject(cart, settings);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cartJson);
                }
                else
                {
                    int index = Exists(cart, id);
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
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cartJson);
                }
                Total = cart.Sum(i => i.FlowerBouquet.UnitPrice * i.Quantity);
                return Page();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Page();
                
            }
        }

        public IActionResult OnPostCheckout()
        {
            try
            {
                var cartJsonFromSession = SessionHelper.GetObjectFromJson<string>(HttpContext.Session, "cart");
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
                var customerId = HttpContext.Session.GetInt32("id");
                Random rand = new Random();
                int orderId = rand.Next(1, 5000);
                var order = new Order
                {
                    OrderId = orderId,
                    OrderDate = DateTime.Now,
                    OrderStatus = "CheckedOut",
                    ShippedDate = DateTime.Now.AddDays(1),
                    CustomerId = customerId,
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
                for (var i = 0; i < cart.Count(); i++)
                {
                    cart.RemoveAt(i);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                }
                return RedirectToPage("./OrderHistory", new { id = customerId });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Page();
            }
        }

        public IActionResult OnGetAsync()
        {
            var cartJsonFromSession = SessionHelper.GetObjectFromJson<string>(HttpContext.Session, "cart");
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

        public IActionResult OnGetDelete(int id)
        {
            var cartJsonFromSession = SessionHelper.GetObjectFromJson<string>(HttpContext.Session, "cart");
            if (cartJsonFromSession != null)
            {
                cart = JsonConvert.DeserializeObject<List<CartItem>>(cartJsonFromSession, settings);
            }
            int index = Exists(cart, id);
            cart.RemoveAt(index);
            var cartJson = JsonConvert.SerializeObject(cart, settings);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cartJson);
            Total = cart.Sum(i => i.FlowerBouquet.UnitPrice * i.Quantity);
            return Page();
        }

        public IActionResult OnPostUpdate(int[] quantities)
        {
            var cartJsonFromSession = SessionHelper.GetObjectFromJson<string>(HttpContext.Session, "cart");
            if (cartJsonFromSession != null)
            {
                cart = JsonConvert.DeserializeObject<List<CartItem>>(cartJsonFromSession, settings);
            }
            for (var i = 0; i < cart.Count; i++)
            {
                cart[i].Quantity = quantities[i];
            }
            var cartJson = JsonConvert.SerializeObject(cart, settings);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cartJson);
            Total = cart.Sum(i => i.FlowerBouquet.UnitPrice * i.Quantity);
            return Page();
        }

        private int Exists(IList<CartItem> cart, int id)
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
