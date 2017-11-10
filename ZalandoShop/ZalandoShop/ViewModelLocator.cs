

using ZalandoShop.Contracts.Models;
using ZalandoShop.Contracts.Services;
using ZalandoShop.Model;
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

        public IZalandoProductsWithPagingInfo ZalandoProductsWithPaging
        {
            get
            {
                return InstanceFactory.GetInstance<IZalandoProductsWithPagingInfo>();
            }
        }
    }
}
