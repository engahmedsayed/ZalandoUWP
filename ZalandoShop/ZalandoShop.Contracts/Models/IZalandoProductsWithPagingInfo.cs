using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZalandoShop.Contracts.Models
{
    public interface IZalandoProductsWithPagingInfo
    {
        IEnumerable<IZalandoProductItem> IZalandoProductItems { get; set; }

        int TotalPages { get; set; }
    }
}
