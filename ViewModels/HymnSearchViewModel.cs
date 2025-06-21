using Hymn_Book.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hymn_Book.ViewModels
{
    public class HymnSearchViewModel
    {
        private readonly SQLiteAsyncConnection _db;

        public ObservableCollection<Hymn> SearchResults { get; set; } = new();
        public string SearchQuery { get; set; }

        public ICommand SearchCommand { get; }

        public HymnSearchViewModel(SQLiteAsyncConnection db)
        {
            _db = db;
            SearchCommand = new Command(async () => await PerformSearch());
        }

        private async Task PerformSearch()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                SearchResults.Clear();
                return;
            }

            var results = await _db.Table<Hymn>()
                .Where(h => h.Title.Contains(SearchQuery) || h.HymnNumber.ToString().Contains(SearchQuery))
                .ToListAsync();

            SearchResults.Clear();
            foreach (var hymn in results)
                SearchResults.Add(hymn);
        }
    }
}
