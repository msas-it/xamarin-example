using SearchTweets.Data;
using SearchTweets.Models;
using SearchTweets.Rules;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace SearchTweets.ViewModels
{
    public class SearchTwitterViewModel : INotifyPropertyChanged
    {
        #region Constructor
        public SearchTwitterViewModel()
        {
            SearchTwitterCommand = new Command(SearchTwitter);
        }
        #endregion

        #region Properties
        public Command SearchTwitterCommand { get; }
        public SearchOptions SearchOptions { get; set; } = new SearchOptions();

        private List<SearchResultItem> _searchResultItems = new List<SearchResultItem>();
        public List<SearchResultItem> SearchResultItems
        {
            get
            {
                return _searchResultItems;
            }

            set
            {
                _searchResultItems = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(SearchResultItems)));
            }
        }

        public bool ReadyToSearch
        {
            get
            {
                return SearchOptionsRules.Validate(Query);
            }
        }

        public string Query
        {
            get
            {
                return SearchOptions.SearchQuery;
            }
            set
            {
                SearchOptions.SearchQuery = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Query)));
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(ReadyToSearch)));
            }
        }

        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Methods
        public void SearchTwitter()
        {
            SearchResultItems = RestService.SearchTwittter(SearchOptions);
        }
        #endregion
    }
}
