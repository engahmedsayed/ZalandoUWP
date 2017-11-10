

using ZalandoShop.Contracts.Models;
using ZalandoShop.Contracts.Services;
using ZalandoShop.Model;
using ZalandoShop.Services;
using ZalandoShop.Shared;
using ZalandoShop.ViewModel;

namespace ZalandoShop
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            InstanceFactory.RegisterWithTransientLifetime<IZalandoProductItem,
               ZalandoProductItem>();

            InstanceFactory.RegisterWithTransientLifetime<IZalandoProductsWithPagingInfo,
               ZalandoProductsWithPagingInfo>();

            InstanceFactory.RegisterType<IZalandoDataService,
                ZalandoDataService>();

        }

        public IZalandoProductItem ZalandoProductItem
        {
            get
            {
                return InstanceFactory.GetInstance<IZalandoProductItem>();
            }
        }

        public SearchZalandoViewModel SearchZalandoViewModel
        {
            get
            {
                return InstanceFactory.GetInstance<SearchZalandoViewModel>();
            }
        }
        public SearchResultsViewModel SearchResultsViewModel
        {
            get
            {
                return InstanceFactory.GetInstance<SearchResultsViewModel>();
            }
        }

        public IZalandoDataService ZalandoDataService
        {
            get
            {
                return InstanceFactory.GetInstance<IZalandoDataService>();
            }
        }

        public IZalandoProductsWithPagingInfo ZalandoProductsWithPaging
        {
            get
            {
                return InstanceFactory.GetInstance<IZalandoProductsWithPagingInfo>();
            }
        }
    }
}
