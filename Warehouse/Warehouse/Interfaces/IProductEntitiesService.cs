using Warehouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Interfaces
{
    public interface IProductEntitiesService
    {
        IEnumerable<ProductViewModel> GetProducts();
        IEnumerable<ProductViewModel> GetShoppingList();
        void UpdateEntities(RequestModel model);
    }
}
