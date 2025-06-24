using Hymn_Book.Model;
using Hymn_Book.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;

namespace Hymn_Book.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly IHymnService _hymnService;

        public ObservableCollection<Hymn> Hymns { get; set; } = new();

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    OnPropertyChanged();
                    DebouncedSearch();
                }
            }
        }

        private bool _noResultsFound;
        public bool NoResultsFound
        {
            get => _noResultsFound;
            set
            {
                _noResultsFound = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchCommand { get; }
        public ICommand ToggleFavoriteCommand { get; }

        private CancellationTokenSource _debounceCts;

        public MainPageViewModel(IHymnService hymnService)
        {
            _hymnService = hymnService;

            SearchCommand = new Command(async () => await PerformSearch());
            ToggleFavoriteCommand = new Command<Hymn>(async (h) => await ToggleFavorite(h));
            _ = LoadHymns();
        }

        private async Task LoadHymns()
        {
            var hymns = await _hymnService.GetAllHymns();
            Hymns.Clear();
            foreach (var hymn in hymns.OrderBy(h => h.HymnNumber))
                Hymns.Add(hymn);

            NoResultsFound = false;
        }

        private void DebouncedSearch()
        {
            _debounceCts?.Cancel();
            _debounceCts = new CancellationTokenSource();

            var token = _debounceCts.Token;

            Task.Run(async () =>
            {
                await Task.Delay(300, token); // 300ms debounce
                if (!token.IsCancellationRequested)
                {
                    await PerformSearch();
                }
            }, token);
        }

        private async Task PerformSearch()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                await LoadHymns();
                return;
            }

            var hymns = await _hymnService.GetFilteredHymns(SearchQuery);
            Hymns.Clear();

            if (hymns.Any())
            {
                foreach (var hymn in hymns.OrderBy(h => h.HymnNumber))
                    Hymns.Add(hymn);

                NoResultsFound = false;
            }
            else
            {
                NoResultsFound = true;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        private async Task ToggleFavorite(Hymn hymn)
        {
            if (hymn == null) return;

            // Toggle the state
            hymn.IsFavourite = !hymn.IsFavourite;

            // Save the change
            await _hymnService.UpdateHymnAsync(hymn);

            // Option 1: Just refresh that item
            var index = Hymns.ToList().FindIndex(x => x.HymnNumber == hymn.HymnNumber);
            if (index >= 0)
            {
                Hymns[index] = hymn;
            }

            // Option 2 (if needed): Reload the whole list
            // await LoadHymns();
        }
    }
}
