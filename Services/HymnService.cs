using Hymn_Book.Intefaces.Repository;
using Hymn_Book.Model;

namespace Hymn_Book.Services
{
    public class HymnService : IHymnService
    {
        private readonly IHymnRepository _hymnRepository;

        public HymnService(IHymnRepository hymnRepository)
        {
            _hymnRepository = hymnRepository;
        }
        public Task AddHymn(Hymn hymn)
        {
            return _hymnRepository.AddHymnAsync(hymn);
        }

        public async Task<ICollection<Hymn>> GetAllHymns()
        {
            return await _hymnRepository.GetAllHymnAsync();
        }

        public async Task<ICollection<Hymn>> GetFilteredHymns(string filter)
        {
           return await _hymnRepository.FindHymnAsync(filter);
        }

        public async Task<Hymn> GetHymn(int hymnNumber)
        {
            return await _hymnRepository.GetHymnAsync(hymnNumber);
        }

        public async Task UpdateHymnAsync(Hymn hymn)
        {
            await _hymnRepository.UpdateHymnAsync(hymn);
        }
    }
}
