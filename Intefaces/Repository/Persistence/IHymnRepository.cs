using Hymn_Book.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hymn_Book.Services.Intefaces.Repository.Persistence
{
    public interface IHymnRepository
    {
        public Task<Hymn> AddHymnAsync(Hymn hymn);
        public Task<IEnumerable<Hymn>> GetHymnAsync(Expression<Func<int, bool>> pred);
        public Task<IEnumerable<Hymn>> GetAllHymnAsync();
    }
}
