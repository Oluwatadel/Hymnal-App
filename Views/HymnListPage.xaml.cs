using Hymn_Book.ViewModels;
using SQLite;

namespace Hymn_Book.Views;

public partial class HymnListPage : ContentPage
{
    public HymnListPage(SQLiteAsyncConnection db)
    {
        InitializeComponent();
        BindingContext = new HymnListViewModel(db);
    }

    private void OnHymnSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
        {
            var selectedHymn = e.CurrentSelection[0] as Model.Hymn;

            if (selectedHymn != null)
            {
                // Handle navigation or display logic here
                Console.WriteLine($"Selected Hymn: {selectedHymn.Title}");
            }

            ((CollectionView)sender).SelectedItem = null;
        }
    }
}