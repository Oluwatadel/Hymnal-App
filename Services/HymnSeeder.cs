using Hymn_Book.Intefaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var existing = await repository.GetAllAsync();
            if (!existing.Any())
            {
                foreach (var hymn in hymns)
                    await repository.AddAsync(hymn);
            }
        }
    }
}
