using System.Text;

public abstract class Problem
{
    public abstract Task<(object PartOne, object PartTwo)> Run();
}

public abstract class Problem<T> : Problem
{
    private readonly int _year;
    private readonly int _day;
    private readonly string _fileName;
    private Lazy<Task<List<T>>> _input;

    protected Problem(int year, int day, string fileName)
    {
        _year = year;
        _day = day;
        _fileName = fileName;
        _input = new Lazy<Task<List<T>>>(() => GetInput(GetInputFilePath()));
    }

    public override async Task<(object PartOne, object PartTwo)> Run()
    { 
        var input = await _input.Value;
        return (PartOne(input), PartTwo(input));
    }


    protected abstract object PartOne(List<T> input); 
    protected abstract object PartTwo(List<T> input); 

    private string GetInputFilePath() =>
        Path.Combine(new string[] { Directory.GetCurrentDirectory(), _year.ToString(), $"Day{_day.ToString("00")}", _fileName});

    protected virtual async Task<List<T>> GetInput(string path, bool includeEmptyLine = false)
    {
        var inputLines = new List<T>();

        using var fileStream = File.OpenRead(path);
        using var reader = new StreamReader(fileStream, Encoding.UTF8, true, 512);
        string line = string.Empty;
        while ((line = await reader.ReadLineAsync()) is not null)
        {
            if (string.IsNullOrEmpty(line) && includeEmptyLine)
                continue;

            inputLines.Add(ProcessLine(line));    
        }

        return inputLines;
    }

    protected abstract T ProcessLine(string line);
}