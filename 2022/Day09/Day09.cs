public record Move(char Direction, int Distance);
class Day09 : Problem<Move>
{
    private const int InitialPositionX = 0;
    private const int InitialPositionY = 0;

    public Day09(int year, int day, string fileName) : base(year, day, fileName)
    {
        
        
    }

    protected override object PartOne(List<Move> input)
    {
        Head head = new Head(InitialPositionX, InitialPositionY);
        HeadTrucker headTrucker = new HeadTrucker(InitialPositionX, InitialPositionY, head);
        foreach (var move in input)
        {
            head.Move(move.Direction, move.Distance);
        }
        return headTrucker.UniquePositions;
    }

    protected override object PartTwo(List<Move> input)
    {
        return 0;
    }

    protected override Move? ProcessLine(string line)
    {
        var move = line.Split(" ");

        return new Move(move[0][0], int.Parse(move[1]));
    }
}
