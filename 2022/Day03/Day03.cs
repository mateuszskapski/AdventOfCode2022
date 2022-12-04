public class Day03 : Problem
{
    public Day03(int year, int day, string fileName) : base(year, day, fileName)
    {
    }

    protected override object PartOne(List<string> input)
    {
        int prioritySum = 0;
        foreach (var line in input)
        {
            var halfLength = line.Length / 2;
            var left = line.AsSpan().Slice(0, halfLength);
            var right = line.AsSpan().Slice(halfLength, halfLength);
            
            var hashSet = new HashSet<char>();
            foreach (var @char in left)
            {
                hashSet.Add(@char);
            }

            CalculateTotalPriority(right, 0, (@char) => hashSet.TryGetValue(@char, out _), ref prioritySum);
        }

        return prioritySum;
    }

    protected override object PartTwo(List<string> input)
    {
        int prioritySum = 0;
        int index = 0;
        
        while (index < input.Count)
        {
            var third = input[index + 2];
            var firstHashSet = input[index].ToHashSet<char>();
            var secondHashSet = input[index + 1].ToHashSet<char>();

            CalculateTotalPriority(third, 0, (@char) => firstHashSet.TryGetValue(@char, out _) && secondHashSet.TryGetValue(@char, out _), ref prioritySum);

            index += 3;
        }
        
        return prioritySum;
    }

    void CalculateTotalPriority(ReadOnlySpan<char> items, int index, Func<char, bool> conditions, ref int prioritySum)
    {
        if (index >= items.Length)
            return;

        if (conditions.Invoke(items[index]))
        {
            prioritySum += GetItemPriority(items[index]);
            return;
        }

        CalculateTotalPriority(items, index + 1, conditions, ref prioritySum);

        return;
    }

    int GetItemPriority(char @char)
    {
        if (char.IsLower(@char))
            return @char-96;
        else if (char.IsUpper(@char))
            return @char-38;
        else
            throw new ArgumentException($"Character '{@char}' is not a letter.");
    }
}