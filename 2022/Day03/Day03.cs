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

            foreach (var @char in right)
            {
                if (hashSet.TryGetValue(@char, out _))
                {
                    prioritySum += GetPriority(@char);
                    break;
                }
            }
        }

        return prioritySum;
    }

    protected override object PartTwo(List<string> input)
    {
        int prioritySum = 0;
        int index = 0;
        
        while (index < input.Count)
        {
            var first = input[index];
            var second = input[index + 1];
            var third = input[index + 2];
            
            var firstHashSet = new HashSet<char>();
            foreach (var @char in first)
            {
                firstHashSet.Add(@char);
            }
            
            var secondHashSet = new HashSet<char>();
            foreach (var @char in second)
            {
                secondHashSet.Add(@char);
            }
            
            foreach (var @char in third)
            {
                if (firstHashSet.TryGetValue(@char, out _) && secondHashSet.TryGetValue(@char, out _))
                {
                    prioritySum += GetPriority(@char);
                    break;
                }
            }

            index += 3;
        }
        
        return prioritySum;
    }

    int GetPriority(char @char)
    {
        if (char.IsLower(@char))
            return @char-96;
        else if (char.IsUpper(@char))
            return @char-38;
        else
            throw new ArgumentException($"Character '{@char}' is not a letter.");
    }
}