using Hymn_Book.Intefaces.Repository;
using Hymn_Book.Model;

namespace Hymn_Book.Repository
{
    public class HymnRepository : IHymnRepository
    {
        private readonly HymnDatabase _hymnDatabase;

        public HymnRepository(HymnDatabase hymnDatabase)
        {
            _hymnDatabase = hymnDatabase;
        }

        public Task AddHymnAsync(Hymn hymn)
           => _hymnDatabase.AddHymnAsync(hymn);
      

        public async Task<ICollection<Hymn>> FindHymnAsync(string s)
            => await _hymnDatabase.FindHymnAsync(s);

        public async Task<Hymn> GetHymnAsync(int hymnNumber)
            => await _hymnDatabase.GetbyIdAsync(hymnNumber);

        public async Task<ICollection<Hymn>> GetAllHymnAsync() 
            => await _hymnDatabase.GetAllHymnAsync();

        public async Task ResetDatabase()
        {
            await _hymnDatabase.ResetDatabase();
        }

        public async Task CreateDatabase()
        {
            await _hymnDatabase.CreateDatabase();
        }

        public async Task UpdateHymnAsync(Hymn hymn)
        {
            await _hymnDatabase.UpdateHymnAsync(hymn);
        }
    }
}