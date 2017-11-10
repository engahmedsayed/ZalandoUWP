using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZalandoShop.Contracts.Models
{
    public class ZalandoProductsWithPagingInfo : IZalandoProductsWithPagingInfo
    {
        public IEnumerable<IZalandoProductItem> IZalandoProductItems
        {
            get;set;
        }

        public int TotalPages
        {
            get;set;
        }
    }
}
