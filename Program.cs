GetFeature().Execute();

IFeature GetFeature()
{
    IFeature feature = new NotSupportedFeature("Not defined");
    try
    {
        foreach(var arg in args)
        {
            feature = arg switch
            {
                "-r" => new ResolverFeature(new ArgumentParser("-r", args)),
                _ => new NotSupportedFeature(nameof(arg))
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