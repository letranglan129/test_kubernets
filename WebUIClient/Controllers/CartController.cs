using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using WebUIClient.Models;
using WebUIClient.Ultis;

namespace WebUIClient.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public async Task<ActionResult> Index()
        {
            var carts = await ProductRestAPI.GetClient("/api-cart").GetAsync<List<Cart>>(new RestRequest());
            return View(carts);
        }

        // GET: Cart/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var cart = await ProductRestAPI.GetClient("/api-cart/" + id.ToString()).GetAsync<Cart>(new RestRequest());
            return View(cart);
        }

        // GET: Cart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cart/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Cart cart)
        {
            try
            {
                var req = new RestRequest().AddBody(cart);
                await ProductRestAPI.GetClient("/api-cart").PostAsync<Cart>(req);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Cart/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var cart = await ProductRestAPI.GetClient("/api-cart/" + id.ToString()).GetAsync<Cart>(new RestRequest());
            return View(cart);
        }

        // POST: Cart/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Cart cart)
        {
            try
            {
                var req = new RestRequest().AddBody(cart);
                await ProductRestAPI.GetClient("/api-cart/" + id.ToString()).PutAsync<Cart>(req);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Cart/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var cart = await ProductRestAPI.GetClient("/api-cart/" + id.ToString()).GetAsync<Cart>(new RestRequest());
            return View(cart);
        }

        // POST: Cart/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, IFormCollection collection)
        {
            try
            {
                await ProductRestAPI.GetClient("/api-cart/" + id.ToString()).DeleteAsync(new RestRequest());
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
