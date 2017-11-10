using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using ZalandoShop.Contracts.Services;
using ZalandoShop.Model.DataService;
using ZalandoShop.Shared;

namespace ZalandoShop.ViewModel
{
    public class SearchZalandoViewModel : ViewModelBase
    {
        #region Private Fields
        IZalandoDataService _zalandoDataService;
        private int _menFontSize;
        private int _womenFontSize;
        private SolidColorBrush _maleForeGroundColor;
        private SolidColorBrush _femaleForeGroundColor;
        private string _searchText;
        private ObservableCollection<string> _articles;
        #endregion

        #region Commands
        public RelayCommand MenFilterCommand
        {
            get;
            private set;
        }

        public RelayCommand WomenFilterCommand
        {
            get;
            private set;
        }

        public RelayCommand SearchZalandoCommand
        {
            get;
            private set;
        }
        #endregion

        #region Public Properties
        public int MenFontSize
        {
            get { return _menFontSize; }
            set
            {
                Set(() => MenFontSize, ref _menFontSize, value);
            }
        }
        public int WomenFontSize
        {
            get { return _womenFontSize; }
            set
            {
                Set(() => WomenFontSize, ref _womenFontSize, value);
            }
        }
        public SolidColorBrush MaleForeGroundColor
        {
            get { return _maleForeGroundColor; }
            set
            {
                Set(() => MaleForeGroundColor, ref _maleForeGroundColor, value);
            }
        }

        public SolidColorBrush FemaleForeGroundColor
        {
            get { return _femaleForeGroundColor; }
            set
            {
                Set(() => FemaleForeGroundColor, ref _femaleForeGroundColor, value);
            }
        }

        public ObservableCollection<string> Articles
        {
            get { return _articles; }
            set
            {
                Set(() => Articles, ref _articles, value);
            }
        }
        public string SearchText
        {
            get { return _searchText; }
            set { Set(() => SearchText, ref _searchText, value); }
        }

        public FilterType FilterType { get; set; }
        #endregion

        #region Constructors
        public SearchZalandoViewModel(IZalandoDataService zalandoDataService)
        {
            _zalandoDataService = zalandoDataService;
            InitializeCommands();
            _maleForeGroundColor = new SolidColorBrush();
            _femaleForeGroundColor = new SolidColorBrush();
            Articles = new ObservableCollection<string>();
            _menFontSize = 20;
            _womenFontSize = 20;
        }
        #endregion

        #region Private Methods
        private void InitializeCommands()
        {
            SearchZalandoCommand =
                new RelayCommand(async () =>
                {
                    var results = await _zalandoDataService.Search(SearchText, FilterType);
                    this.MessengerInstance.Send<SearchObject>(new SearchObject { SearchKeyWord = SearchText, FilterType = FilterType });
                },
                () => true);

            MenFilterCommand = new RelayCommand(() =>
            {
                MenFontSize = 25;
                RaisePropertyChanged("MenFontSize");
                MaleForeGroundColor = new SolidColorBrush(Colors.Orange);
                RaisePropertyChanged("MaleForeGroundColor");
                WomenFontSize = 20;
                RaisePropertyChanged("WomenFontSize");
                FemaleForeGroundColor = new SolidColorBrush(Colors.White);
                RaisePropertyChanged("FemaleForeGroundColor");
                FilterType = FilterType.Male;
            }, () => true);
            WomenFilterCommand = new RelayCommand(() =>
            {
                WomenFontSize = 25;
                FemaleForeGroundColor = new SolidColorBrush(Colors.Orange);
                RaisePropertyChanged("FemaleForeGroundColor");
                RaisePropertyChanged("WomenFontSize");
                MenFontSize = 20;
                RaisePropertyChanged("MenFontSize");
                MaleForeGroundColor = new SolidColorBrush(Colors.White);
                RaisePropertyChanged("MaleForeGroundColor");
                FilterType = FilterType.Female;
            }, () => true);

        }
        #endregion

        #region Public Methods
        public async void FilterArticles()
        {
            Articles = await _zalandoDataService.Search(SearchText, FilterType);
        }

        public void ProcessQuery()
        {
            this.MessengerInstance.Send<SearchObject>(new SearchObject { SearchKeyWord = SearchText, FilterType = FilterType });
        }
        #endregion

    }
}
