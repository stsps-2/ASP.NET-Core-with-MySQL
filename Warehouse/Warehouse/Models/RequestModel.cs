using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Models
{
    public class RequestModel
    {
        [JsonProperty(PropertyName = "toDelete")]
        public ProductModel[] ToDelete { get; set; } = new ProductModel[] { };

        [JsonProperty(PropertyName = "toUpdate")]
        public ProductModel[] ToUpdate { get; set; } = new ProductModel[] { };
    }
}
