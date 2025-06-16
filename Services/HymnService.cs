using Hymn_Book.Intefaces.Repository;
using Hymn_Book.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _hymnRepository.AddHymnAsync(hymn);
            return Task.CompletedTask;
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
    }
}
