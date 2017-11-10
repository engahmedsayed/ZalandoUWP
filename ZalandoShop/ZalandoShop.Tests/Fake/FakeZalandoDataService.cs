using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZalandoShop.Contracts.Models;
using ZalandoShop.Contracts.Services;
using ZalandoShop.Model;
using ZalandoShop.Shared;

namespace ZalandoShop.Tests.Fake
{
    public class FakeZalandoDataService : IZalandoDataService
    {
        public IZalandoProductsWithPagingInfo ProductItemsWithPagingInfo;
        public FakeZalandoDataService()
        {
            CreateFakeZalandoData();
        }

        public void CreateFakeZalandoData()
        {
            ProductItemsWithPagingInfo = new ZalandoProductsWithPagingInfo();
            ProductItemsWithPagingInfo.IZalandoProductItems = new List<ZalandoProductItem>
            {
                new ZalandoProductItem
                {
                BrandName = "Tommy Hilfiger",
                ProductName = "Test1",
                Size = "M",
                PriceFormatted = "22$"
                },
                new ZalandoProductItem
                {
                BrandName = "Tommy Hilfiger",
                ProductName = "Test2",
                Size = "L",
                PriceFormatted = "22$"
                }
            };
        }
        public async Task<IZalandoProductsWithPagingInfo> GetArticlesPaged(string searchKeyWord, FilterType filterType = FilterType.NoFilter, int pageNumber = 1, int pageCount = 20)
        {
            return await Task.Factory.StartNew(()=> ProductItemsWithPagingInfo);
           
        }

        public Task<ObservableCollection<string>> Search(string searchKeyWord, FilterType filterType)
        {
            throw new NotImplementedException();
        }
    }
}
