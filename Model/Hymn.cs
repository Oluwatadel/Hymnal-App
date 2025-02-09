using SQLite;

namespace Hymn_Book.Model
{
    public class Hymn
    {
        [PrimaryKey, AutoIncrement]
        public int HymnNo { get; set; }
        string Title { get; set; }
        string Lyric { get; set; }
    }
}
