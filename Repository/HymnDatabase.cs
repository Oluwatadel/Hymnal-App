using Hymn_Book.Model;
using SQLite;

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
        {
            var hymns = await _database.Table<Hymn>().ToListAsync();
            return hymns;
        }

        public async Task<ICollection<Hymn>> FindHymnAsync(string s)
            => await _database.Table<Hymn>()
            .Where
            (
                a => a.Lyric.ToLower().Contains(s.ToLower())
            ).ToListAsync();


        //public async Task<ICollection<Hymn>> GetAllHymnAsync(string s)
        //    => await _database.Table<Hymn>()
        //    .Where(a => a.Lyric.Contains(s)).ToListAsync();

        public async Task<Hymn> GetbyIdAsync(int hymnNumber)
            => await _database.Table<Hymn>()
            .FirstOrDefaultAsync(a => a.HymnNumber == hymnNumber);

        public async Task<int> AddHymnAsync(Hymn hymn)
            => await _database.InsertAsync(hymn);

        public async Task ResetDatabase()
        {
            await _database.DropTableAsync<Hymn>();
        }

        public async Task CreateDatabase()
        {
            await _database.CreateTableAsync<Hymn>();

        }

        public async Task UpdateHymnAsync(Hymn hymn)
        {
            await _database.UpdateAsync(hymn);
        }

    }
}
