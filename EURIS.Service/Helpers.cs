using EURIS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EURIS.Service
{
    public class Helpers
    {
        public class ProductViewModel
        {
            private readonly CatalogManager _catalogManager = new CatalogManager();

            public Product Product { get; set; }
            public List<CatalogArea> Catalogs { get; set; }

            public ProductViewModel() 
            {
                Product = new Product();
                Catalogs = SetDefaultCatalogs();
            }

            public ProductViewModel(Product product)
            {
                Product = product;
                Catalogs = SetProductCatalogs(product.Catalog);
            }

            List<CatalogArea> SetProductCatalogs(ICollection<Catalog> productCatalogs)
            {
                var catalogsVmList = new List<CatalogArea>();

                var catalogs = _catalogManager.GetCatalogs();

                foreach (var catalog in catalogs)
                {
                    var catalogVm = new CatalogArea();

                    var isActive = productCatalogs.Any(x => x.CatalogId == catalog.CatalogId);

                    catalogVm.Active = isActive;
                    catalogVm.Catalog = catalog;

                    catalogsVmList.Add(catalogVm);
                }

                return catalogsVmList;
            }

            List<CatalogArea> SetDefaultCatalogs()
            {
                var catalogs = _catalogManager.GetCatalogs();

                var catalogsVmList = new List<CatalogArea>();

                foreach(var catalog in catalogs)
                {
                    var catalogVm = new CatalogArea
                    {
                        Catalog = catalog,
                        Active = false
                    };

                    catalogsVmList.Add(catalogVm);
                }

                return catalogsVmList;
            }
        }

        public class CatalogViewModel
        {
            private readonly ProductManager _productManager = new ProductManager();

            public Catalog Catalog { get; set; }
            public List<ProductArea> Products { get; set; }

            public CatalogViewModel()
            {
                Catalog = new Catalog();
                Products = SetDefaultProducts();
            }

            public CatalogViewModel(Catalog catalog)
            {
                Catalog = catalog;
                Products = SetCatalogProducts(catalog.Product);
            }

            List<ProductArea> SetDefaultProducts()
            {
                var products = _productManager.GetProducts();

                var productsVmList = new List<ProductArea>();

                foreach (var product in products)
                {
                    var productVm = new ProductArea
                    {
                        Product = product,
                        Active = false
                    };

                    productsVmList.Add(productVm);
                }

                return productsVmList;

            }

            List<ProductArea> SetCatalogProducts(ICollection<Product> catalogProducts)
            {
                var productsVmList = new List<ProductArea>();

                var products = _productManager.GetProducts();

                foreach (var product in products)
                {
                    var productVm = new ProductArea();

                    var isActive = catalogProducts.Any(x => x.ProductId == product.ProductId);

                    productVm.Active = isActive;
                    productVm.Product = product;

                    productsVmList.Add(productVm);
                }

                return productsVmList;
            }


        }

        public class CatalogArea
        {
            public Catalog Catalog { get; set; }
            public bool Active { get; set; }
        }

        public class ProductArea
        {
            public Product Product { get; set; }
            public bool Active { get; set; }
        }
    }
}
