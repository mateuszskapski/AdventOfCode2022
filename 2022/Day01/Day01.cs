public class Day01 : Problem
{
    public Day01(int year, int day, string fileName) : base(year, day, fileName) {}
    protected override object PartOne(List<string> input) => GetElfsCaloriesDescending(input).Max();

    protected override object PartTwo(List<string> input) => GetElfsCaloriesDescending(input).Take(3).Sum();

    IEnumerable<int> GetElfsCaloriesDescending(List<string> input) =>
        input.Select(x => x.Split("\n")
            .Select(x => int.Parse(x)))
            .Select(x => x.Sum())
            .OrderByDescending(x => x);
}