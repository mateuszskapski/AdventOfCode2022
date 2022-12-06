public record Instruction(int From, int To, int Count);

public class Day05 : Problem<Instruction>
{
    public Day05(int year, int day, string fileName) : base(year, day, fileName)
    {
    }

    protected override object PartOne(List<Instruction> input)
    {
        var stacks = CreateInitialStuck();

        foreach (var instruction in input)
        {
            var from = stacks[instruction.From-1];
            var to = stacks[instruction.To-1];
            for (var move = 0; move < instruction.Count; move++)
            {
                to.Push(from.Pop());
            }
        }

        var word = stacks.Select(x => x.Pop()).ToArray();

        return new string(word);
    }

    protected override object PartTwo(List<Instruction> input)
    {
        var stacks = CreateInitialStuck();

        foreach (var instruction in input)
        {
            var from = stacks[instruction.From-1];
            var to = stacks[instruction.To-1];
            var tempStack = new Stack<char>();
            for (var move = 0; move < instruction.Count; move++)
            {
                tempStack.Push(from.Pop());
            }

            while (tempStack.Count > 0)
            {
                to.Push(tempStack.Pop());
            }
        }

        var word = stacks.Select(x => x.Pop()).ToArray();

        return new string(word);
    }

    protected override Instruction? ProcessLine(string line)
    {
        if (line.StartsWith("move"))
        {
            var splitLine = line.Split(' ');
            return new Instruction(int.Parse(splitLine[3]), int.Parse(splitLine[5]), int.Parse(splitLine[1]));
        }
        else
        {
            if (line.Contains('['))
            {
                _inputLines.Add(line);
            }

            return null;
        }
    }

    List<string> _inputLines = new List<string>();
    List<Stack<char>> CreateInitialStuck()
    {
        var stacks = new List<Stack<char>>(); 
        var stackPosition = 0;
        for (var row = _inputLines.Count - 1; row >= 0; row--)
        {
            for (var position = 1; @position <= _inputLines[row].Length + 1; position = position + 4)
            {
                if (row == _inputLines.Count - 1)
                {
                    var stack = new Stack<char>();
                    stack.Push(_inputLines[row][position]);
                    stacks.Add(stack);
                }
                else
                {
                    if (_inputLines[row][position] != ' ')
                    {
                        stacks[stackPosition].Push(_inputLines[row][position]);
                    }
                }
                stackPosition++;
            }
            stackPosition = 0;
        }

        return stacks;
    }
}