using SQLite;

namespace Hymn_Book.Model
{
    public class Hymn
    {
        public Guid Id { get; set; }
        public int HymnNumber { get; set; } = default!;
        public string Code { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Lyric { get; set; } = default!;

    }
}
