using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZalandoShop.Shared;

namespace ZalandoShop.Model.DataService
{
    public class SearchObject
    {
        public string SearchKeyWord { get; set; }
        public FilterType FilterType { get; set; }
    }
}
