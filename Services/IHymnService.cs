using Hymn_Book.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hymn_Book.Services
{
    public interface IHymnService
    {
        public Task<ICollection<Hymn>> GetAllHymns();
        public Task<Hymn> GetHymn(int hymnNumber);
        public Task<ICollection<Hymn>> GetFilteredHymns(string filter);
        public Task AddHymn(Hymn hymn);
    }
}
