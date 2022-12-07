public class Day06 : Problem<string>
{
    public Day06(int year, int day, string fileName) : base(year, day, fileName)
    {
    }

    protected override object PartOne(List<string> input) => FindMarkerPosition(input.First(), 4);

    protected override object PartTwo(List<string> input) => FindMarkerPosition(input.First(), 14);

    protected override string? ProcessLine(string line) => line;

    int FindMarkerPosition(string signal, int distinctChars)
    {
        for (var i = 0; i < signal.Length; i++)
        {
            var chars = new HashSet<char>();
            var j = i;
            for (; j < i+distinctChars; j++)
            {
                if (!chars.Add(signal[j]))
                {
                    break;
                }
            }

            if (j - i == distinctChars)
                return j;
        }

        throw new Exception("Marker position not found."); 
    }
}
