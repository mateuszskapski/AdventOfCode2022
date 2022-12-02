public class Day01 : Resolver
{
    public Day01(int year, int day) : base(year, day) {}
    public override object PartOne(string input) =>
        GetElfsCaloriesDescending(input).Max();

    public override object PartTwo(string input) =>
        GetElfsCaloriesDescending(input).Take(3).Sum();

    IEnumerable<int> GetElfsCaloriesDescending(string input) =>
        input.Split("\n\n").Select(x => x.Split("\n")
            .Select(x => int.Parse(x)))
            .Select(x => x.Sum())
            .OrderByDescending(x => x);
}