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
        Tail tail = new Tail(InitialPositionX, InitialPositionY);
        
        head.Subscribe(tail);
        
        foreach (var move in input)
        {
            head.Move(move.Direction, move.Distance);
        }
        
        return tail.UniquePositions;
    }

    protected override object PartTwo(List<Move> input)
    {
        Head head = new Head(InitialPositionX, InitialPositionY);
        Tail tail9 = new Tail(InitialPositionX, InitialPositionY);
        Tail tail8 = new Tail(InitialPositionX, InitialPositionY);
        Tail tail7 = new Tail(InitialPositionX, InitialPositionY);
        Tail tail6 = new Tail(InitialPositionX, InitialPositionY);
        Tail tail5 = new Tail(InitialPositionX, InitialPositionY);
        Tail tail4 = new Tail(InitialPositionX, InitialPositionY);
        Tail tail3 = new Tail(InitialPositionX, InitialPositionY);
        Tail tail2 = new Tail(InitialPositionX, InitialPositionY);
        Tail tail1 = new Tail(InitialPositionX, InitialPositionY);
        
        head.Subscribe(tail1);
        tail1.Subscribe(tail2);
        tail2.Subscribe(tail3);
        tail3.Subscribe(tail4);
        tail4.Subscribe(tail5);
        tail5.Subscribe(tail6);
        tail6.Subscribe(tail7);
        tail7.Subscribe(tail8);
        tail8.Subscribe(tail9);
        
        foreach (var move in input)
        {
            head.Move(move.Direction, move.Distance);
        }
        
        return tail9.UniquePositions;
    }

    protected override Move? ProcessLine(string line)
    {
        var move = line.Split(" ");

        return new Move(move[0][0], int.Parse(move[1]));
    }
}
