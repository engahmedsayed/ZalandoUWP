using Microsoft.Toolkit.Uwp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ZalandoShop.Contracts.Models;
using ZalandoShop.ViewModel;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ZalandoShop.Views
{
    public sealed partial class ZalandoSearchResultView : UserControl
    {
        static string _oldSearchValue = string.Empty;
        static Shared.FilterType _oldFilterType;
        private IncrementalLoadingCollection<SearchResultsViewModel, IZalandoProductItem> _dataSource; 

        public ZalandoSearchResultView()
        {
            this.InitializeComponent();
            _dataSource = new IncrementalLoadingCollection<SearchResultsViewModel, IZalandoProductItem>();
            if (this.DataContext is SearchResultsViewModel)
            {
                SearchResultsViewModel.SearchValueChanged += CurrentDataContext_SearchValueChanged;
            }
            itemGridView.ItemsSource = _dataSource;
        }

        private void CurrentDataContext_SearchValueChanged(string currentSearchValue, Shared.FilterType currentFilterType)
        {
            if (_oldSearchValue.ToLower() != currentSearchValue.ToLower() || _oldFilterType != currentFilterType)
            {
                _oldSearchValue = currentSearchValue;
                _oldFilterType = currentFilterType;
                var gridTemplate = itemGridView.Template;
                _dataSource = new IncrementalLoadingCollection<SearchResultsViewModel, IZalandoProductItem>();
                itemGridView.Template = null;
                itemGridView.Template = gridTemplate;
                itemGridView.ItemsSource = _dataSource;
            }
        }
    }
}
