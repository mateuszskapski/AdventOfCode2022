public class Resolver : IFeature
{
    private readonly ArgumentParser _parser;

    public string FileName { get; set; } = "input.txt";

    public Resolver(ArgumentParser parser)
    {
        _parser = parser;
    }

    public Task ExecuteAsync()
    {
        var year = 0;
        var day = 0;

        try
        {
            var args = _parser.GetArguments<int, int>();
            year = args.A1;
            day = args.A2;
        }
        catch
        {
            Console.WriteLine($"Invalid arguments.");
            return Task.CompletedTask;
        }

        var problem = Type.GetType($"Day{day.ToString("00")}");
        if (problem is null)
        {
            Console.WriteLine("The problem does not exist, or has not be solved yet.");
            return Task.CompletedTask;
        }

        var resolver = Activator.CreateInstance(problem, year, day, FileName) as Problem;

        var results = resolver.Run();
        Console.WriteLine("*** Part One ***");
        Console.WriteLine($"Answer: {results.PartOne}");        
        Console.WriteLine("*** Part Two ***");
        Console.WriteLine($"Answer: {results.PartTwo}");

        return Task.CompletedTask;
    }
}