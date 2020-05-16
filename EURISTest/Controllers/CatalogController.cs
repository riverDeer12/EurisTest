using EURIS.Entities;
using EURIS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EURISTest.Controllers
{
    public class CatalogController : Controller
    {
        private readonly CatalogManager _catalogManager;

        public CatalogController()
        {
            _catalogManager = new CatalogManager();
        }

        public ActionResult Index()
        {
            var catalogs = _catalogManager.GetCatalogs();

            return View(catalogs);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var catalogVm = new Helpers.CatalogViewModel();

            return View(catalogVm);
        }

        [HttpPost]
        public ActionResult Create(Helpers.CatalogViewModel postData)
        {
            if (!ModelState.IsValid) return View(postData);

            var result = _catalogManager.CreateNewCatalog(postData.Catalog, postData.Products);

            if (result.IsSuccess) return RedirectToAction("Index");

            return View(postData);
        }

        [HttpGet]
        public ActionResult Details(int catalogId)
        {
            var catalog = _catalogManager.FindCatalog(catalogId);

            if (catalog == null) return RedirectToAction("Index");

            return View(catalog);
        }

        [HttpGet]
        public ActionResult Edit(int catalogId)
        {
            var catalog = _catalogManager.FindCatalog(catalogId);

            if (catalog == null) return RedirectToAction("Index");

            var catalogVm = new Helpers.CatalogViewModel(catalog);

            return View(catalogVm);
        }

        [HttpPost]
        public ActionResult Edit(int catalogId, Helpers.CatalogViewModel updateData)
        {
            if (catalogId != updateData.Catalog.CatalogId) return View();

            var result = _catalogManager.UpdateCatalog(updateData);

            if (result.IsSuccess) return RedirectToAction("Index");

            return View(updateData);
        }

        [HttpGet]
        public ActionResult Delete(int catalogId)
        {
            var catalog = _catalogManager.FindCatalog(catalogId);

            if (catalog == null) return RedirectToAction("Index");

            return View(catalog);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int catalogId)
        {
            var catalog = _catalogManager.FindCatalog(catalogId);

            if (catalog == null) return View();

            var result = _catalogManager.DeleteCatalog(catalog);

            if (result.IsSuccess) return RedirectToAction("Index");

            return View();
        }

    }
}
