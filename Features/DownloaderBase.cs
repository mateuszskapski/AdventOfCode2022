public abstract class DownloaderBase : IFeature
{
    protected string BaseUrl {get; init;}
    protected int Year {get; init;}
    protected int Day {get; init;}
    protected string SessionKey {get; init;}
    protected string OutputDirectory {get; private set;}

    protected DownloaderBase(string baseUrl, ArgumentParser parser)
    {
        BaseUrl = baseUrl;

        try
        {
            var args = parser.GetArguments<int, int, string>();
            Year = args.A1;
            Day = args.A2;
            SessionKey = args.A3;
        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.Message);
        }
    }

    public Task ExecuteAsync()
    {
        var yearDirPath = Path.Combine(Directory.GetCurrentDirectory(), Year.ToString());
        var dirInfo = new DirectoryInfo(yearDirPath);
        if (!dirInfo.Exists)
        {
            Directory.CreateDirectory(yearDirPath);
        }

        var dayDirName = $"Day{Day.ToString("00")}";
        OutputDirectory = Path.Combine(yearDirPath, dayDirName);
        var dayDirInfo = new DirectoryInfo(OutputDirectory);
        if (!dayDirInfo.Exists)
        {
            Directory.CreateDirectory(OutputDirectory);
        }

        return DownloadAsync();
    }

    protected abstract Task DownloadAsync();
}