public record Instruction(int Source, int Destination, int Count);

public class Day05 : Problem<Instruction>
{
    private List<Stack<char>> _stacks = new List<Stack<char>>();

    public Day05(int year, int day, string fileName) : base(year, day, fileName)
    {
    }

    protected override object PartOne(List<Instruction> input)
    {
        throw new NotImplementedException();
    }

    protected override object PartTwo(List<Instruction> input)
    {
        throw new NotImplementedException();
    }

    protected override Instruction ProcessLine(string line)
    {
        throw new NotImplementedException();
    }
}