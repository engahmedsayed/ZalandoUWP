using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZalandoShop.Model.DataService
{
    public class Brand
    {
        public string key { get; set; }
        public string name { get; set; }
        public string logoUrl { get; set; }
        public string logoLargeUrl { get; set; }
        public string shopUrl { get; set; }
        public BrandFamily brandFamily { get; set; }
    }
}
