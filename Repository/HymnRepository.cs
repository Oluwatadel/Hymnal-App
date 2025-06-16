

using Hymn_Book.Intefaces.Repository;
using Hymn_Book.Model;
using System.Linq.Expressions;

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
            => await _hymnDatabase.GetAllHymnsAsync();
    }
}