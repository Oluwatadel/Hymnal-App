using DocumentFormat.OpenXml.Packaging;
using Hymn_Book.Intefaces.Repository;
using Hymn_Book.Model;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Hymn_Book.Services
{
    public class HymnSeeder
    {
        public static async Task SeedFromJsonAsync(IHymnRepository repository)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Hymn_Book.Resources.Raw.CleanedHymns.json";

            try
            {
                var existing = await repository.GetAllHymnAsync();
                if (!existing.Any())
                {
                    var hymns = await ExtractTextFromJson(assembly, resourceName);
                    foreach (var hymn in hymns)
                    {
                        //Debug.WriteLine(hymn.HymnNumber, hymn.Title);
                        //Debug.WriteLine(hymn.Lyric);
                        await repository.AddHymnAsync(hymn);
                    }
                }
                //foreach (var hymn in existing)
                //{
                //    if(hymn.HymnNumber > 200)
                //        Debug.WriteLine(hymn.HymnNumber, hymn.Title);
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Embedded hymn json not found.: {ex.Message}");
            }
        }

        private static async Task<List<Hymn>> ExtractTextFromJson(Assembly assembly, string resourceName)
        {
            var names = assembly.GetManifestResourceNames();
            foreach (var name in names)
            {
                Debug.WriteLine($"RESOURCE: {name}");
            }
            using var stream = assembly.GetManifestResourceStream(resourceName);
            if(stream == null)
            {
                Debug.WriteLine("Stream is null");
            }

            using var reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync();

            var hymns = JsonSerializer.Deserialize<List<Hymn>>(json);

            return hymns!;
        }

        //private static List<Hymn> ParseHymns(string rawText)
        //{
        //    var hymns = new List<Hymn>();

        //    // Each hymn starts with a line like: "BHB 1.", "BHB: 2", etc.
        //    var pattern = @"(?:BHB[:\s-]+)(\d+)[.\s-]+(.*?)(?=BHB[:\s-]+\d+|$)";
        //    var matches = Regex.Matches(rawText, pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);

        //    foreach (Match match in matches)
        //    {
        //        var number = match.Groups[1].Value.Trim();
        //        var content = match.Groups[2].Value.Trim();

        //        var lines = content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        //        if (lines.Length == 0) continue;

        //        var title = lines[0].Trim();
        //        var lyrics = string.Join(Environment.NewLine, lines.Skip(1)).Trim();

        //        hymns.Add(new Hymn
        //        {
        //            HymnNumber = int.Parse(number),
        //            Title = title,
        //            Lyric = lyrics
        //        });
        //    }

        //    return hymns;
        //}
    }


}
