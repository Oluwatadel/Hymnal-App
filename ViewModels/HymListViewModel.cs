using CommunityToolkit.Mvvm.ComponentModel;
using Hymn_Book.Model;
using SQLite;
using System.Collections.ObjectModel;

namespace Hymn_Book.ViewModels
{
    public class HymnListViewModel : ObservableObject
    {
        private readonly SQLiteAsyncConnection _db;
        public ObservableCollection<Hymn> Hymns { get; } = new();
        public ObservableCollection<Hymn> FilteredHymns { get; } = new();

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                FilterHymns();
            }
        }

        public HymnListViewModel(SQLiteAsyncConnection db)
        {
            _db = db;
            LoadHymnsAsync();
        }

        private async Task LoadHymnsAsync()
        {
            var hymns = await _db.Table<Hymn>().OrderBy(h => h.HymnNumber).ToListAsync();
            Hymns.Clear();
            foreach (var hymn in hymns)
                Hymns.Add(hymn);

            FilterHymns();
        }

        private void FilterHymns()
        {
            FilteredHymns.Clear();
            var results = string.IsNullOrWhiteSpace(SearchText)
                ? Hymns
                : Hymns.Where(h => h.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase)
                                || h.HymnNumber.ToString().Contains(SearchText));

            foreach (var hymn in results)
                FilteredHymns.Add(hymn);
        }
    }
}

