using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZalandoShop.Model.DataService
{
    public class Unit
    {
        public string id { get; set; }
        public string size { get; set; }
        public Price price { get; set; }
        public OriginalPrice originalPrice { get; set; }
        public bool available { get; set; }
        public int stock { get; set; }
    }
}
