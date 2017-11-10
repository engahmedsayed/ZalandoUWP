using GalaSoft.MvvmLight;
using Microsoft.Toolkit.Uwp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZalandoShop.Contracts.Models;
using ZalandoShop.Contracts.Services;
using ZalandoShop.Model.DataService;
using ZalandoShop.Shared;

namespace ZalandoShop.ViewModel
{
   public class SearchResultsViewModel:ViewModelBase,IIncrementalSource<IZalandoProductItem>
    {
        #region Private Fields
        private ObservableCollection<IZalandoProductItem> _zalandoProducts;
        private IZalandoDataService _zalandoDataService;
        private bool _isLoading;
        private bool _isInternetConnected;
        private bool _isDataFound;
        #endregion

        #region Events
        //This event is fired when the search value changed in order to update the UI.
        public static event Action<string> SearchValueChanged;
        #endregion

        #region Commands
        #endregion

        #region Public Properties
        public ObservableCollection<IZalandoProductItem> ZalandoProducts
        {
            get
            {
                return _zalandoProducts;
            }
            set
            {
                Set(() => ZalandoProducts, ref _zalandoProducts, value);
            }
        }

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                Set(() => IsLoading, ref _isLoading, value);
            }
        }

        public int TotalPagesCount { get; set; }

        public static string CurrentSearchValue { get; set; }

        public static FilterType CurrentFilterType { get; set; }

        public int CurrentPageNumber { get; set; }
        public bool IsInternetConnected
        {
            get
            {
                return _isInternetConnected;
            }
            set
            {
                Set(() => IsInternetConnected, ref _isInternetConnected, value);
            }
        }

        public bool IsDataFound
        {
            get
            {
                return _isDataFound;
            }
            set
            {
                Set(() => IsDataFound, ref _isDataFound, value);
            }
        }


        #endregion

        #region Constructors
        public SearchResultsViewModel()
        {
            _zalandoDataService = InstanceFactory.GetInstance<IZalandoDataService>();
            IsLoading = false;
            IsInternetConnected = true;
            IsDataFound = true;
        }
        public SearchResultsViewModel(IZalandoDataService zalandoDataService)
        {
            _zalandoDataService = zalandoDataService;
            IsInternetConnected = true;
            IsDataFound = true;
            // HasMoreItems = true;
            InitializeMessenger();
        }
        #endregion

        #region Private Methods
        private async Task InitializeMessenger()
        {
            this.MessengerInstance.Register<SearchObject>
                (this, async (s) =>
                {
                    CurrentPageNumber = 1;
                    TotalPagesCount = 0;
                    CurrentSearchValue = s.SearchKeyWord;
                    CurrentFilterType = s.FilterType;
                    await GetPagedItemsAsync(CurrentPageNumber, 30);
                    if (SearchValueChanged != null)
                        SearchValueChanged(s.SearchKeyWord);
                    // await LoadMoreItemsAsync(30);
                    //await Search(s.SearchKeyWord, s.FilterType);
                });
        }
        private async Task<ObservableCollection<IZalandoProductItem>> Search(string searchValue, FilterType filterType, int pageNumber = 1, int pageCount = 30)
        {
            if(!NetworkHelper.Instance.ConnectionInformation.IsInternetAvailable)
            {
                IsInternetConnected = false;
                return new ObservableCollection<IZalandoProductItem>();
            }
            else
            {
                IsInternetConnected = true;
                IsLoading = true;
                var results = await _zalandoDataService.GetArticlesPaged(searchValue, filterType, pageNumber);
                CurrentPageNumber++;
                TotalPagesCount = results != null ?results.TotalPages : 0;
                ZalandoProducts = new ObservableCollection<IZalandoProductItem>(results.IZalandoProductItems);
                IsLoading = false;
                if(ZalandoProducts == null || !ZalandoProducts.Any())
                {
                    IsDataFound = false;
                }
                else
                {
                    IsDataFound = true;
                }
                return ZalandoProducts;
            }
        }
        #endregion

        #region Public Methods
        public async Task<IEnumerable<IZalandoProductItem>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default(CancellationToken))
        {

            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            if (!string.IsNullOrWhiteSpace(CurrentSearchValue))
            {
                var result = await Search(CurrentSearchValue, CurrentFilterType, pageIndex);
                return result.ToList();
            }
            IsDataFound = false;
            return null;
        }
        #endregion
    }
}
