using Hymn_Book.ViewModels;
using SQLite;

namespace Hymn_Book.Views;

public partial class HymnSearchPage : ContentPage
{
    public HymnSearchPage(SQLiteAsyncConnection db)
    {
        InitializeComponent();
        BindingContext = new HymnSearchViewModel(db);
    }

    private void OnHymnSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
        {
            var selectedHymn = e.CurrentSelection[0] as Model.Hymn;

            if (selectedHymn != null)
            {
                Console.WriteLine($"Selected Hymn: {selectedHymn.Title}");
                // Navigate to details or display as needed
            }

            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
