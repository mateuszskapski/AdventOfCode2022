using System.Text;

public abstract class Problem
{
    private readonly int _year;
    private readonly int _day;
    private readonly string _fileName;
    private Lazy<Task<List<string>>> _input;

    protected Problem(int year, int day, string fileName)
    {
        _year = year;
        _day = day;
        _fileName = fileName;
        _input = new Lazy<Task<List<string>>>(() => GetInput(GetInputFilePath()));
    }

    public async Task<(object PartOne, object PartTwo)> Run()
    { 
        var input = await _input.Value;
        return (PartOne(input), PartTwo(input));
    }


    protected abstract object PartOne(List<string> input); 
    protected abstract object PartTwo(List<string> input); 

    private string GetInputFilePath() =>
        Path.Combine(new string[] { Directory.GetCurrentDirectory(), _year.ToString(), $"Day{_day.ToString("00")}", _fileName});

    protected virtual async Task<List<string>> GetInput(string path, bool includeEmptyLine = false)
    {
        var inputLines = new List<string>();

        using var fileStream = File.OpenRead(path);
        using var reader = new StreamReader(fileStream, Encoding.UTF8, true, 512);
        string line = string.Empty;
        while ((line = await reader.ReadLineAsync()) is not null)
        {
            if (string.IsNullOrEmpty(line) && includeEmptyLine)
                continue;

            inputLines.Add(ProcessLine(line));    
        }

        if (string.IsNullOrEmpty(inputLines.Last()))
            inputLines.RemoveAt(inputLines.Count - 1);

        return inputLines;
    }

    protected virtual string ProcessLine(string line) => line;
}