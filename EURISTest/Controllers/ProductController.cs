using System.Web.Mvc;
using EURIS.Service;

namespace EURISTest.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductManager _productManager;

        public ProductController()
        {
            _productManager = new ProductManager();
        }

        public ActionResult Index()
        {
            var products = _productManager.GetProducts();

            return View(products);
        }

        [HttpGet]
        public ActionResult Create() 
        {
            var productVm = new Helpers.ProductViewModel();

            return View(productVm);
        }

        [HttpPost]
        public ActionResult Create(Helpers.ProductViewModel postData) 
        {
            if (!ModelState.IsValid) return View(postData);

            var result = _productManager.CreateNewProduct(postData.Product, postData.Catalogs);

            if (result.IsSuccess) return RedirectToAction("Index");

            return View(postData);
        }

        [HttpGet]
        public ActionResult Details(int productId)
        {
            var product = _productManager.FindProduct(productId);

            if (product == null) return RedirectToAction("Index");

            return View(product);
        }

        [HttpGet]
        public ActionResult Edit(int productId) 
        {
            var product = _productManager.FindProduct(productId);

            if (product == null) return RedirectToAction("Index");

            var productVm = new Helpers.ProductViewModel(product);

            return View(productVm);
        }

        [HttpPost]
        public ActionResult Edit(int productId, Helpers.ProductViewModel updateData)
        {
            if (productId != updateData.Product.ProductId) return View();

            var result = _productManager.UpdateProduct(updateData);

            if (result.IsSuccess) return RedirectToAction("Index");

            return View(updateData);
        }

        [HttpGet]
        public ActionResult Delete(int productId) 
        {
            var product = _productManager.FindProduct(productId);

            if (product == null) return RedirectToAction("Index");

            return View(product);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int productId) 
        {
            var product = _productManager.FindProduct(productId);

            if (product == null) return View();

            var result = _productManager.DeleteProduct(product);

            if(result.IsSuccess) return RedirectToAction("Index");

            return View();
        }

    }
}
