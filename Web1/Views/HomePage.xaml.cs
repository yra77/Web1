

namespace Web1.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
           // (App.Current.MainPage as Microsoft.Maui.Controls.NavigationPage).On<iOS>().EnableTranslucentNavigationBar();
        }

        protected override bool OnBackButtonPressed()
        {
            // Return true to prevent back button 
            return true;
        }
    }
}