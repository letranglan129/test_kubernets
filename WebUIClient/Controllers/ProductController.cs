using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using WebUIClient.Ultis;
using WebUIClient.Models;

namespace WebUIClient.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            var prods = await ProductRestAPI.GetClient("/api-prod").GetAsync<List<Product>>(new RestRequest());
            return View(prods);
        }

        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var prod = await ProductRestAPI.GetClient("/api-prod/" + id.ToString()).GetAsync<Product>(new RestRequest());
            return View(prod);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product prod)
        {
            try
            {
                var req = new RestRequest().AddBody(prod);
                await ProductRestAPI.GetClient("/api-prod").PostAsync<Product>(req);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var prod = await ProductRestAPI.GetClient("/api-prod/" + id.ToString()).GetAsync<Product>(new RestRequest());
            return View(prod);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Product prod)
        {
            try
            {
                var req = new RestRequest().AddBody(prod);
                await ProductRestAPI.GetClient("/api-prod/" + id.ToString()).PutAsync<Product>(req);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var prod = await ProductRestAPI.GetClient("/api-prod/" + id.ToString()).GetAsync<Product>(new RestRequest());
            return View(prod);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await ProductRestAPI.GetClient("/api-prod/" + id.ToString()).DeleteAsync(new RestRequest());
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
