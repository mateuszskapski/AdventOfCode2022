var baseUrl = "https://adventofcode.com";

await GetFeature().ExecuteAsync();

IFeature GetFeature()
{
    IFeature feature = new NotSupportedFeature("Not defined");
    try
    {
        foreach(var arg in args)
        {
            feature = arg switch
            {
                "-r" => new Resolver(new ArgumentParser("-r", args)),
                "-u" => new InputDownloader(baseUrl, new ArgumentParser("-u", args)),
                _ => new NotSupportedFeature(arg.ToString())
            };

            if (feature is not NotSupportedFeature)
                return feature;
        }
    }
    catch
    {
        Console.WriteLine("Invalid arguments.");
    }

    return feature;
}