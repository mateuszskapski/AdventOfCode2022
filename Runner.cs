public class Runner
{
    private readonly IFeature _feature;

    public Runner(IFeature feature)
    {
        _feature = feature;
    }

    public void Run()
    {
        _feature.Execute();
    }
}