using CC.OnlineShopping.Interfaces;
using CC.OnlineShopping.RepositoryEF;
using CC.OnlineShopping.RepositoryEF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CC.OnlineShopping.WebApp.Controllers
{
    public class StoreController : Controller
    {
        IProductRepository _productRepository = new ProductRepository();
        IUserRepository _userRepository = new UserRepository();
        DatabaseOnlineShopping db = new DatabaseOnlineShopping();
        // GET: Store
        public ActionResult Index(string search, float? minPrice, float? maxPrice, string category)
        {
            var products = _productRepository.GetAll().OrderByDescending(p => p.ProductId).ToList();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();

                products = products.Where(p => p.Title.ToLower().Contains(search)).ToList();
            }

            if(minPrice > 0 || maxPrice > 0)
            {
                if (minPrice == null)
                {
                    minPrice = 0;
                }
                if (maxPrice == null)
                {
                    float maximum = db.Products.Max(p => p.Price);

                    maxPrice = maximum;
                }

                products = products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
            }

            if ( category != "All" && !string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category == category).ToList();
            }

            return View(products);
        }

        public ActionResult Buy(int id)
        {
            var product = _productRepository.GetById(id);

            return View(product);
        }

        public JsonResult Buys(int prodId, int? sold, int price, string owner)
        {
            var theOne = db.Users.Single(u => u.Email == User.Identity.Name);

            var theOwner = db.Users.Single(u => u.Name == owner);

            var product = db.Products.Single(p => p.ProductId == prodId);

            if (theOne != null && theOwner != null)
            {
                var minus = theOne.Credits - price;
                var plus = theOwner.Credits + price;

                theOne.Credits = minus;
                theOwner.Credits = plus;

                if (sold != null)
                {
                    sold++;
                }
                else
                {
                    sold = 1;
                }

                product.Sold = sold;

                _userRepository.Update(theOne);
                _userRepository.Update(theOwner);
                _productRepository.Update(product);

                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}