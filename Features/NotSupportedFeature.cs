public class NotSupportedFeature : IFeature
{
    private readonly string _featureName;

    public NotSupportedFeature(string featureName)
    {
        _featureName = featureName;
    }
    
    public Task ExecuteAsync()
    {
        Console.WriteLine($"Feature '{_featureName}' not supported.");

        return Task.CompletedTask;
    }
}