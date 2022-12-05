public class Day01 : Problem<string>
{
    public Day01(int year, int day, string fileName) : base(year, day, fileName) {}
    protected override object PartOne(List<string> input) => Sums.Select(x => x.Value).Max();

    protected override object PartTwo(List<string> input) => Sums.Select(x => x.Value).OrderByDescending(x => x).Take(3).Sum();

    int currentElfId = 1;
    Dictionary<int,int> Sums = new Dictionary<int, int>
    {
         { 1, 0 } // Elf ID, Sum 
    };

    protected override string ProcessLine(string line)
    {
        if (string.IsNullOrEmpty(line))
        {
            currentElfId++;
            Sums.Add(currentElfId, 0);
        }
        else
        {
            Sums[currentElfId] += int.Parse(line);
        }

        return line;
    }
}