using Hymn_Book.Model;
using SQLite;

namespace Hymn_Book.Services.Intefaces.Repository.Context
{
    public class DbContext
    {
        private readonly SQLiteAsyncConnection _database;
        public SQLiteAsyncConnection Database => _database;
        public DbContext()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "app_database.db");
            _database = new SQLiteAsyncConnection(dbPath);
        }

        public async Task InitializeAsync()
        {
            try
            {
                await _database.CreateTableAsync<Hymn>();
                await ImportHymnsFromFileAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database Initialization Error: {ex.Message}");
                return;
            }
        }

        private async Task ImportHymnsFromFileAsync()
        {
            var count = await _database.Table<Hymn>().CountAsync();
            if(count == 0)
            {

            }
        }
    }
}
