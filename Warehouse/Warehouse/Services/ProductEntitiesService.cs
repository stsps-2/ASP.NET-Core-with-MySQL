using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Warehouse.Models;
using Warehouse.Interfaces;
using Warehouse.Data.Models;
using Warehouse.Data;

namespace Warehouse.Services
{
    public class ProductEntitiesService : IProductEntitiesService
    {
        private readonly WarehouseContext Context;

        public ProductEntitiesService(WarehouseContext context)
        {
            Context = context;
        }

        public IEnumerable<ProductViewModel> GetProducts()
        {
            return Context.GetProducts().Select(ConvertToProductViewModel);
        }

        public IEnumerable<ProductViewModel> GetShoppingList()
        {
            return Context.GetShoppingList().Select(ConvertToProductViewModel);
        }

        public void UpdateEntities(RequestModel model)
        {
            foreach (var entity in model.ToDelete)
            {
                var product = Context.GetProductByName(entity.Name.Replace("'", "").Replace("\"", "").Trim()).First();
                if (product != null)
                    Context.DeleteProduct(product.ProductId);
            }

            foreach (var entity in model.ToUpdate)
            {
                var product = Context.GetProductByName(entity.Name.Replace("'", "").Replace("\"", "").Trim()).First();
                if (product != null)
                    Context.UpdateProduct(product.ProductId, entity.Quantity);
                else
                    Context.CreateProduct(new Product()
                    {
                        Name = entity.Name.Replace("'", "").Replace("\"", "").Trim(),
                        Quantity = entity.Quantity,
                        DefaultValue = entity.Quantity
                    });
            }
        }

        #region Helpers
        private static ProductViewModel ConvertToProductViewModel(Product product)
        {
            return new ProductViewModel()
            {
                Name = product.Name,
                Quantity = product.Quantity,
                DefaultValue = product.DefaultValue ?? 1
            };
        }
        #endregion
    }
}