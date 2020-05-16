using EURIS.Entities;
using EURISTest.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EURIS.Service
{
    public class CatalogManager
    {
        private readonly LocalDbEntities _db;

        public CatalogManager()
        {
            _db = new LocalDbEntities();
        }

        public List<Catalog> GetCatalogs()
        {
            return _db.Catalog.ToList();
        }

        public Catalog FindCatalog(int catalogId)
        {
            var catalog = _db.Catalog.FirstOrDefault(x => x.CatalogId == catalogId);

            return catalog;
        }

        public ServiceResponse CatalogExists(int catalogId)
        {
            var catalog = FindCatalog(catalogId);

            if (catalog == null) return ServiceResponse.Error(ServiceMessages.NotFound);

            return ServiceResponse.Success(ServiceMessages.Found);
        }

        List<Product> GetNewProducts(List<Helpers.ProductArea> products)
        {
            var newProducts = new List<Product>();

            foreach (var productVm in products)
            {
                if (productVm.Active)
                {
                    var product = _db.Product.FirstOrDefault(x => x.ProductId == productVm.Product.ProductId);

                    newProducts.Add(product);
                }
            }

            return newProducts;
        }

        public ServiceResponse CreateNewCatalog(Catalog newCatalog, List<Helpers.ProductArea> products)
        {
            var newProducts = GetNewProducts(products);

            newCatalog.Product = newProducts;

            _db.Catalog.Add(newCatalog);

            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return ServiceResponse.Error(ex.Message);
            }

            return ServiceResponse.Success(ServiceMessages.Created);
        }

        public ServiceResponse UpdateCatalog(Helpers.CatalogViewModel updateData)
        {
            // automapper would be better option
            var catalog = FindCatalog(updateData.Catalog.CatalogId);
            catalog.Code = updateData.Catalog.Code;
            catalog.Description = updateData.Catalog.Description;

            ManageRelatedProducts(catalog, updateData.Products);

            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return ServiceResponse.Error(ex.Message);
            }

            return ServiceResponse.Success(ServiceMessages.Modified);
        }

        void ManageRelatedProducts(Catalog catalog, List<Helpers.ProductArea> products)
        {
            var newProducts = GetNewProducts(products);

            ClearProducts(catalog);

            AddProducts(catalog, newProducts);
        }

        void ClearProducts(Catalog catalog)
        {
            _db.Entry(catalog).Collection("Product").Load();
            catalog.Product.Clear();
            _db.SaveChanges();
        }

        void AddProducts(Catalog catalog, List<Product> newProducts)
        {
            _db.Entry(catalog).Collection("Product").Load();

            foreach (var newProduct in newProducts)
            {
                catalog.Product.Add(newProduct);
            }

            _db.SaveChanges();
        }

        public ServiceResponse DeleteCatalog(Catalog catalog)
        {
            _db.Catalog.Remove(catalog);

            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return ServiceResponse.Error(ex.Message);
            }

            return ServiceResponse.Success(ServiceMessages.Deleted);
        }
    }
}
