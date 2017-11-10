using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZalandoShop.Contracts.Models;

namespace ZalandoShop.Model
{
    public class ZalandoProductItem : IZalandoProductItem
    {
        public string BrandName
        {
            get;
            set;
        }

        public string ImageUrl
        {
            get; set;
        }

        public string PriceFormatted
        {
            get; set;
        }

        public string ProductName
        {
            get; set;
        }

        public string Size
        {
            get; set;
        }

    }
}
