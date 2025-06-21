using Hymn_Book.ViewModels;

namespace Hymn_Book
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void OnHymnSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection?.FirstOrDefault() is Model.Hymn selectedHymn)
            {
                await Navigation.PushAsync(new Views.HymnDetailPage(selectedHymn));
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }
}