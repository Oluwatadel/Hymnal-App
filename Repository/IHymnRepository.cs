using Hymn_Book.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hymn_Book.Intefaces.Repository
{
    public interface IHymnRepository
    {
        public Task AddHymnAsync(Hymn hymn);
        public Task<ICollection<Hymn>> FindHymnAsync(string param);
        public Task<ICollection<Hymn>> GetAllHymnAsync();
        public Task<Hymn> GetHymnAsync(int hymnNumber);
        public Task ResetDatabase();
        public Task CreateDatabase();
        public Task UpdateHymnAsync(Hymn hymn);
    }
}
