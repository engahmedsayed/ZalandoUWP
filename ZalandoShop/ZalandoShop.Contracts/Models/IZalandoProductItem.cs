using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZalandoShop.Contracts.Models
{
    public interface IZalandoProductItem
    {
        string PriceFormatted { get; set; }
        string Size { get; set; }
        string ProductName { get; set; }
        string BrandName { get; set; }
        string ImageUrl { get; set; }

    }
}
