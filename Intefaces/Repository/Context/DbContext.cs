using Hymn_Book.Model;
using SQLite;
using System.Text.RegularExpressions;

namespace Hymn_Book.Intefaces.Repository.Context
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
                try
                {
                    using var stream = await FileSystem.OpenAppPackageFileAsync("HymnsBook.docx");
                    using var memStream = new MemoryStream();
                    await stream.CopyToAsync(memStream);
                    memStream.Seek(0, SeekOrigin.Begin);

                    var doc = Xceed.Words.NET.DocX.Load(memStream);
                    var hymns = new List<Hymn>();

                    Hymn? current = null;

                    List<string> lines = [];

                    foreach (var line in doc.Paragraphs)
                    {
                        var text = line.Text.Trim();

                        if(Regex.IsMatch(text, @"^BHB\s+\d+", RegexOptions.IgnoreCase))
                        {
                            if(current != null)
                            {
                                current.Lyric = string.Join("\n", lines);
                                hymns.Add(current);
                            }
                        }
                        lines.Clear();
                        current = new Hymn
                        {
                            Code = text,
                            HymnNumber = ExtractHymnNumber(text),
                            Title = ExtractTitleFromNextLine(doc, line)
                        };
                    }
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private int ExtractHymnNumber(string text)
        {
            var match = Regex.Match(text, @"BHB\s+(\d+)", RegexOptions.IgnoreCase);
            return match.Success ? int.Parse(match.Groups[1].Value) : 0;
        }

        private string ExtractTitleFromNextLine(Xceed.Words.NET.DocX doc, Xceed.Document.NET.Paragraph para)
        {
            var index = doc.Paragraphs.IndexOf(para);
            if (index + 1 < doc.Paragraphs.Count)
            {
                return doc.Paragraphs[index + 1].Text.Trim();
            }
            return "";
        }
    }
}
