using System.Net.Http.Json;
using PdfApi.Shared;

namespace PdfApi.Client;
public static class Program
{
    public static async Task Main(string[] args)
    {
        while (true)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://104.210.129.44:5000");
                var wk = await client.PostAsJsonAsync("/wk", new WkPdfRequest()
                {
                    Url =
                        "https://google.com",
                    FooterRight = "[page]/[toPage]",
                    FooterLeft = "[datetime]",
                    Replacements = new Dictionary<string, string>
                    {
                        {"datetime", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}
                    },
                    PageMargins = new Margins
                    {
                        Right = 4,
                        Left = 4
                    },
                    FooterSpacing = 3,
                    IsLowQuality = true,
                    NoOutline = true,
                    ImageDpi = 300,
                    DisableExternalLinks = true,
                    DisableInternalLinks = true,
                    ImageQuality = 80,
                    PrintMediaType = true,
                    Dpi = 70,
                    FooterLine = true
                });

                wk.EnsureSuccessStatusCode();
                var content = await wk.Content.ReadAsByteArrayAsync();
                File.WriteAllBytes("hello.pdf", content);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
            }
        }
    }
}
