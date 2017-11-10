using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using ZalandoShop.Contracts.Models;
using ZalandoShop.Contracts.Services;
using ZalandoShop.Model.DataService;
using ZalandoShop.Shared;

namespace ZalandoShop.Services
{
    public class ZalandoDataService : IZalandoDataService
    {
        //ToDo: Add to configuration file.
        private string baseUri = "https://api.zalando.com/";
        private string articlesEndPointName = "articles";
        private string facetEndpointName = "facets";
        public async Task<IZalandoProductsWithPagingInfo> GetArticlesPaged(string searchKeyWord, FilterType filterType = FilterType.NoFilter, int pageNumber = 1, int pageCount = 20)
        {

            IZalandoProductsWithPagingInfo zalandoProductItemsWithPaging = InstanceFactory.GetInstance<IZalandoProductsWithPagingInfo>(); ;
            using (var client = new HttpClient())
            {
                string repUrl = string.Format("{0}?fullText={1}", baseUri + articlesEndPointName, searchKeyWord);
                HttpResponseMessage response;
                //
                switch (filterType)
                {
                    case FilterType.Male:
                        response = await client.GetAsync(string.Format("{0}&gender={1}&page={2}&pageSize={3}&fields=name%2Ccolor%2Cbrand%2Cunits%2Cmedia",
                                                                        repUrl, filterType.ToString(), pageNumber, pageCount));
                        break;
                    case FilterType.Female:
                        response = await client.GetAsync(string.Format("{0}&gender={1}&page={2}&pageSize={3}&fields=name%2Ccolor%2Cbrand%2Cunits%2Cmedia",
                                                                        repUrl, filterType.ToString(), pageNumber, pageCount));
                        break;
                    default:
                        response = await client.GetAsync(string.Format("{0}&page={1}&pageSize={2}&fields=name%2Ccolor%2Cbrand%2Cunits%2Cmedia",
                                                                        repUrl, pageNumber, pageCount));
                        break;
                }
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    var rootResult = JsonConvert.DeserializeObject<RootObject>(result);
                    var zalandoProductItem = InstanceFactory.GetInstance<IZalandoProductItem>();
                    ObservableCollection<IZalandoProductItem> zalandoProductItems = new ObservableCollection<IZalandoProductItem>();
                    if (rootResult != null && rootResult.content != null)
                    {
                        foreach (var item in rootResult.content)
                        {
                            zalandoProductItem.BrandName = item.brand.name;
                            zalandoProductItem.ProductName = item.name;
                            zalandoProductItem.Size = item.units.FirstOrDefault().size;
                            zalandoProductItem.PriceFormatted = item.units.FirstOrDefault().price.formatted;
                            zalandoProductItem.ImageUrl = item.media.images.FirstOrDefault() != null ? item.media.images.FirstOrDefault().smallUrl : null;
                            zalandoProductItems.Add(zalandoProductItem);
                            zalandoProductItem = InstanceFactory.GetInstance<IZalandoProductItem>();
                        }
                        zalandoProductItemsWithPaging.TotalPages = rootResult.totalPages;
                        zalandoProductItemsWithPaging.IZalandoProductItems = zalandoProductItems;
                    }
                }
            }
            return zalandoProductItemsWithPaging;
        }

        public async Task<ObservableCollection<string>> Search(string searchKeyWord, FilterType filterType)
        {
            if (string.IsNullOrWhiteSpace(searchKeyWord))
            {
                return null;
            }
            string repUrl = string.Format("{0}", baseUri + facetEndpointName);
            string cashedFacets = "Facets.json";
            ObservableCollection<string> searchResults = new ObservableCollection<string>();
            //Cash results for the next 5 min.
            //
            //Check if setting #cashValueInMinutes exists and create it if it does not
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            var value = localSettings.Values["#cashValueInMinutes"];
            StorageFolder localFolder =
                  ApplicationData.Current.LocalFolder;
            if (value == null)
            {
                localSettings.Values["#cashValueInMinutes"] = DateTime.Now.ToString();
            }
            //
            if ((DateTime.Now - Convert.ToDateTime(localSettings.Values["#cashValueInMinutes"])).Minutes > 2 || !await StorageHelper.DoesFileExistAsync
                   (cashedFacets, localFolder))
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response;

                    response = await client.GetAsync(repUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        var rootResult = JsonConvert.DeserializeObject<List<FacetResult>>(result);

                        var filteredFacets = rootResult.SelectMany(t => t.facets).Where(f => f.displayName.ToLower().Contains(searchKeyWord.ToLower()));
                        foreach (var item in filteredFacets)
                        {
                            searchResults.Add(item.displayName);
                        }
                        localSettings.Values["#cashValueInMinutes"] = DateTime.Now.ToString();
                        if (await StorageHelper.DoesFileExistAsync(cashedFacets, localFolder))
                        {
                            await StorageHelper.ClearCache(localFolder);
                        }
                        try
                        {

                            StorageFile storageFile = await localFolder.CreateFileAsync(cashedFacets);
                            await Windows.Storage.FileIO.WriteTextAsync(storageFile, result);
                        }
                        catch (Exception ex)
                        {
                            //todo: Log exception
                        }
                    }
                }
            }
            else
            {
                //Get from cashed version
                string result = string.Empty;
                if (await StorageHelper.DoesFileExistAsync
                   (cashedFacets, localFolder))
                {
                    //use cached version
                    StorageFile file =
                        await localFolder.GetFileAsync(cashedFacets);
                    result = await Windows.Storage.FileIO.ReadTextAsync(file);
                }

                var rootResult = JsonConvert.DeserializeObject<List<FacetResult>>(result);

                var filteredFacets = rootResult.SelectMany(t => t.facets).Where(f => f.displayName.ToLower().Contains(searchKeyWord.ToLower()));
                foreach (var item in filteredFacets)
                {
                    searchResults.Add(item.displayName);
                }
            }
            return searchResults;
        }
    }
}
