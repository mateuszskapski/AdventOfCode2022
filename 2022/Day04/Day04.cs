public class Day04 : Problem<string>
{
    public Day04(int year, int day, string fileName) : base(year, day, fileName)
    {
    }

    protected override object PartOne(List<string> input)
    {
        var containerRangeCount = 0;
        foreach (var line in input)
        {
            var pairs = line.Split(",");
            var left = GetRange(pairs[0]);
            var right = GetRange(pairs[1]);

            if ((right.Start >= left.Start && right.End <= left.End) || 
                (left.Start >= right.Start && left.End <= right.End))
                containerRangeCount++;
        }
        
        return containerRangeCount;
    }

    protected override object PartTwo(List<string> input)
    {
        var doNotOverlap = 0;
        foreach (var line in input)
        {
            var pairs = line.Split(",");
            var left = GetRange(pairs[0]);
            var right = GetRange(pairs[1]);
            
            if ((left.End < right.Start || left.Start > right.End))
                doNotOverlap++;
        }
        
        return input.Count - doNotOverlap;
    }

    protected override string ProcessLine(string line) => line;

    (int Start, int End) GetRange(string pair)
    {
        var split = pair.Split("-").Select(x => int.Parse(x));

        return (split.ElementAt(0), split.ElementAt(1));
    }
}
