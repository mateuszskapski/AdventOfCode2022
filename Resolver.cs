public abstract class Resolver
{
    private readonly int _year;
    private readonly int _day;
    protected Lazy<string> _input;

    protected Resolver(int year, int day)
    {
        _year = year;
        _day = day;

        _input = new Lazy<string>(() => GetInput(GetInputFilePath()));
    }

    public (object PartOne, object PartTwo) Run() => new (PartOne(_input.Value), PartTwo(_input.Value));

    protected abstract object PartOne(string input); 
    protected abstract object PartTwo(string input); 

    private string GetInputFilePath() =>
        Path.Combine(new string[] { Directory.GetCurrentDirectory(), _year.ToString(), $"Day{_day.ToString("00")}", "input.txt"});

    string GetInput(string path)
    {
        Console.WriteLine();

        var input = File.ReadAllText(path);
        if (input[input.Length - 1] == '\n')
            input = input.Substring(0, input.Length - 1);

        return input;
    }
}