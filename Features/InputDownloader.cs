public class InputDownloader : DownloaderBase
{
    public InputDownloader(string baseUrl, ArgumentParser parser) : base(baseUrl, parser) {}

    protected override async Task DownloadAsync()
    {
        using var client = new HttpClient();
        using var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{BaseUrl}/{Year}/day/{Day}/input")
        };
        request.Headers.Add("Cookie", $"session={SessionKey}");

        var response = await client.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"{response.ReasonPhrase}");
        }

        using FileStream fs = new FileStream(Path.Combine(OutputDirectory, "input.txt"), FileMode.Create);
        var buffer = await response.Content.ReadAsByteArrayAsync();
        await fs.WriteAsync(buffer, 0, buffer.Length);
    }
}