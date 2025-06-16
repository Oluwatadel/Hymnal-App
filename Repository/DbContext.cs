using Hymn_Book.Model;
using SQLite;
using System.Text.RegularExpressions;

namespace Hymn_Book.Intefaces.Repository
{
    public class HymnDatabase
    {
        private readonly SQLiteAsyncConnection _database;
        public SQLiteAsyncConnection Database => _database;
        public HymnDatabase()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "app_database.db");
            _database = new SQLiteAsyncConnection(dbPath);
        }

        public async Task<ICollection<Hymn>> GetAllHymnAsync()
            => await _database.Table<Hymn>().ToListAsync();
        public async Task<ICollection<Hymn>> GetAllHymnAsync(string s)
            => await _database.Table<Hymn>()
            .Where(a => a.Lyric.Contains(s) || a.Code.Contains(s)).ToListAsync();

        public async Task<Hymn> GetbyIdAsync(int hymnNumber)
            => await _database.Table<Hymn>()
            .FirstOrDefaultAsync(a => a.HymnNumber == hymnNumber);

    }
}
