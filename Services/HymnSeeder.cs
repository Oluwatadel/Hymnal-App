using Hymn_Book.Intefaces.Repository;
using Hymn_Book.Model;
using System.Reflection;
using System.Text.RegularExpressions;
using Xceed.Words.NET;

namespace Hymn_Book.Services
{
    public class HymnSeeder
    {
        public static async Task SeedFromDocxAsync(IHymnRepository repository)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "YourAppNamespace.Resources.Raw.IWE_ORIN_BNJCC_HYMNS.docx";

            using var stream = assembly.GetManifestResourceStream(resourceName)
                ?? throw new FileNotFoundException("Embedded hymn DOCX not found.");

            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            ms.Position = 0;

            using var doc = DocX.Load(ms);
            var text = doc.Text;

            var hymns = ParseHymns(text);

            var existing = await repository.GetAllHymnAsync();
            if (!existing.Any())
            {
                foreach (var hymn in hymns)
                    await repository.AddHymnAsync(hymn);
            }
        }

        private static List<Hymn> ParseHymns(string rawText)
        {
            var hymns = new List<Hymn>();

            // Each hymn starts with a line like: "BHB 1.", "BHB: 2", etc.
            var pattern = @"(?:BHB[:\s-]+)(\d+)[.\s-]+(.*?)(?=BHB[:\s-]+\d+|$)";
            var matches = Regex.Matches(rawText, pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            foreach (Match match in matches)
            {
                var number = match.Groups[1].Value.Trim();
                var content = match.Groups[2].Value.Trim();

                var lines = content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (lines.Length == 0) continue;

                var title = lines[0].Trim();
                var lyrics = string.Join(Environment.NewLine, lines.Skip(1)).Trim();

                hymns.Add(new Hymn
                {
                    HymnNumber = int.Parse(number),
                    Title = title,
                    Lyric = lyrics
                });
            }

            return hymns;
        }
    }


}
