public abstract class Resolver
{
    private readonly int _year;
    private readonly int _day;
    private readonly string _fileName;
    protected Lazy<string> _input;

    protected Resolver(int year, int day, string fileName)
    {
        _year = year;
        _day = day;
        _fileName = fileName;
        _input = new Lazy<string>(() => GetInput(GetInputFilePath()));
    }

    public (object PartOne, object PartTwo) Run() => new (PartOne(_input.Value), PartTwo(_input.Value));

    protected abstract object PartOne(string input); 
    protected abstract object PartTwo(string input); 

    private string GetInputFilePath() =>
        Path.Combine(new string[] { Directory.GetCurrentDirectory(), _year.ToString(), $"Day{_day.ToString("00")}", _fileName});

    string GetInput(string path)
    {
        var input = File.ReadAllText(path);
        if (input[input.Length - 1] == '\n')
            input = input.Substring(0, input.Length - 1);

        return input;
    }
}