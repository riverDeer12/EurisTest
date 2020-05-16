using System;
using System.Collections.Generic;
using System.Linq;
using EURIS.Entities;
using EURISTest.Constants;
using static EURIS.Service.Helpers;

namespace EURIS.Service
{
    public class ProductManager
    {
        private readonly LocalDbEntities _db;
        private readonly CatalogManager _catalogManager;

        public ProductManager()
        {
            _db = new LocalDbEntities();
            _catalogManager = new CatalogManager();
        }

        public List<Product> GetProducts()
        {
            return _db.Product.ToList();
        }

        public Product FindProduct(int productId)
        {
            var product = _db.Product.FirstOrDefault(x => x.ProductId == productId);

            return product;
        }

        public ServiceResponse ProductExists(int productId)
        {
            var product = FindProduct(productId);

            if (product == null) return ServiceResponse.Error(ServiceMessages.NotFound);

            return ServiceResponse.Success(ServiceMessages.Found);
        }

        public ServiceResponse CreateNewProduct(Product newProduct, List<CatalogArea> catalogs)
        {
            var newCatalogs = GetNewCatalogs(catalogs);

            newProduct.Catalog = newCatalogs;

            _db.Product.Add(newProduct);

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

        void ManageRelatedCatalogs(Product product, List<CatalogArea> catalogs)
        {
            var newCatalogs = GetNewCatalogs(catalogs);

            ClearCatalogs(product);

            AddCatalogs(product, newCatalogs);
        }

        void ClearCatalogs(Product product)
        {
            _db.Entry(product).Collection("Catalog").Load();
            product.Catalog.Clear();
            _db.SaveChanges();
        }

        void AddCatalogs(Product product, List<Catalog> newCatalogs)
        {
            _db.Entry(product).Collection("Catalog").Load();

            foreach(var newCatalog in newCatalogs)
            {
                product.Catalog.Add(newCatalog);
            }

            _db.SaveChanges();
        }

        List<Catalog> GetNewCatalogs(List<CatalogArea> catalogs)
        {
            var newCatalogs = new List<Catalog>();

            foreach (var catalogVm in catalogs)
            {
                if (catalogVm.Active)
                {
                    var catalog = _db.Catalog.FirstOrDefault(x => x.CatalogId == catalogVm.Catalog.CatalogId);

                    newCatalogs.Add(catalog);
                }
            }

            return newCatalogs;
        }

        public ServiceResponse UpdateProduct(ProductViewModel updateData)
        {
            // automapper would be better option
            var product = FindProduct(updateData.Product.ProductId);
            product.Code = updateData.Product.Code;
            product.Description = updateData.Product.Description;

            ManageRelatedCatalogs(product, updateData.Catalogs);

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

        public ServiceResponse DeleteProduct(Product product)
        {
            _db.Product.Remove(product);

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
