using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Warehouse.Models
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public int Quantity{ get; set; }
        public int? DefaultValue { get; set; }
    }
}