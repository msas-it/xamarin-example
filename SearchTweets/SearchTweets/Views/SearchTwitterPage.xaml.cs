using SearchTweets.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchTweets.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchTwitterPage : ContentPage
    {
        public SearchTwitterPage()
        {
            InitializeComponent();
            BindingContext = new SearchTwitterViewModel();
        }
        
    }
}