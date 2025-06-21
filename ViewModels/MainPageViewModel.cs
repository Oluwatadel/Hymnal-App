using Hymn_Book.Model;
using Hymn_Book.Services;
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
    public class MainPageViewModel
    {
        private readonly SQLiteAsyncConnection _db;

        public ObservableCollection<Hymn> Hymns { get; set; } = new();
        private readonly IHymnService _hymnService;
        public string SearchQuery { get; set; }
        public ICommand SearchCommand { get; }

        public MainPageViewModel(IHymnService hymnService)
        {
            _hymnService = hymnService;
            SearchCommand = new Command(async () => await PerformSearch());
            LoadHymns().ConfigureAwait(false);
        }

        private async Task LoadHymns()
        {
            var hymns = await _hymnService.GetAllHymns();
            Hymns.Clear();
            foreach (var hymn in hymns)
                Hymns.Add(hymn);
        }

        private async Task PerformSearch()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                await LoadHymns();
                return;
            }

            var hymns = await _hymnService.GetFilteredHymns(SearchQuery);
            var orderedHymn = hymns.ToList().OrderBy(h => h.HymnNumber);
            Hymns.Clear();
            foreach (var hymn in orderedHymn)
                Hymns.Add(hymn);
        }
    }

}
