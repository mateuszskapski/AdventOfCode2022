public class NotSupportedFeature : IFeature
{
    private readonly string _featureName;

    public NotSupportedFeature(string featureName)
    {
        _featureName = featureName;
    }
    
    public void Execute()
    {
        Console.WriteLine($"Feature '{_featureName}' not supported.");
    }
}