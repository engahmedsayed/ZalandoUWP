using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZalandoShop.Contracts.Models;
using ZalandoShop.Shared;

namespace ZalandoShop.Contracts.Services
{
    public interface IZalandoDataService
    {
        Task<ObservableCollection<string>> Search(string searchKeyWord, FilterType filterType);
        Task<IZalandoProductsWithPagingInfo> GetArticlesPaged(string searchKeyWord, FilterType filterType = FilterType.NoFilter, int pageNumber = 1, int pageCount = 20);
    }
}
