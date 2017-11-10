using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZalandoShop.Model.DataService
{
    public class FacetResult
    {
        public string filter { get; set; }
        public List<Facet> facets { get; set; }
    }
}
