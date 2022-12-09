public class Tail : Head, IObserver<Head>
{
    private record Position(int x, int y);

    List<Position> _positions = new List<Position>();
    public int UniquePositions => _positions.Distinct().Count();
    public int VisitedPositions => _positions.Count;

    public Tail(int x = 0, int y = 0) : base(x, y)
    {
        _positions.Add(new Position(x, y));
    }

    public void OnNext(Head head)
    {
        if (head.X == X && head.Y == Y)
            return;
        if (head.X == X && Math.Abs(head.Y - Y) == 1)
            return;
        if (head.Y == Y && Math.Abs(head.X - X) == 1)
            return;
        if (Math.Abs(head.X - X) == 1 && Math.Abs(head.Y - Y) == 1)
            return;
        
        if (head.X != X && head.Y == Y)
        {
            // Move left or right
            X = head.X > X ? ++X : --X;

        }
        else if(head.Y != Y && head.X == X)
        {
             // Move up or down
             Y = head.Y > Y ? ++Y : --Y;
        }
        else
        {
            // Move diagonally
            X = head.X > X ? ++X : --X;
            Y = head.Y > Y ? ++Y : --Y;
        }

        _positions.Add(new Position(X, Y));
        if (Observer is not null)
        {
            X = X;
            Y = Y;
            Observer.OnNext(this);
        }
    }

    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }
}