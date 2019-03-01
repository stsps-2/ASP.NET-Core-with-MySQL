using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Data.Models
{
    public class Product
    {
        private WarehouseContext context;
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int? DefaultValue { get; set; }
    }
}
