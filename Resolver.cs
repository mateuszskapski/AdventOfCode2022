public abstract class Resolver
{
    private readonly int _year;
    private readonly int _day;

    protected Resolver(int year, int day)
    {
        _year = year;
        _day = day;
    }

    public abstract object PartOne(string input); 
    public abstract object PartTwo(string input); 

    public string GetInputFilePath() =>
        Path.Combine(new string[] { Directory.GetCurrentDirectory(), _year.ToString(), $"Day{_day.ToString("00")}"});
}