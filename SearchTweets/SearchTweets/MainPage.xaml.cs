using System;
using Xamarin.Forms;

namespace SearchTweets
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public void NavigateToSearchPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.SearchTwitterPage());
        }

    }
}
