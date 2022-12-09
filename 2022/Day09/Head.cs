public class HeadPositionChangedEventArgs : EventArgs
{
    public char Direction {get; set;}
    public HeadPositionChangedEventArgs(char direction)
    {
        Direction = direction;
    }
}

public class Head
{
    public int X {get; private set;}
    public int Y {get; private set;}
    public event EventHandler<HeadPositionChangedEventArgs> PositionChanged;

    public Head(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Move(char direction, int distance) 
    {
        var args = new HeadPositionChangedEventArgs(direction);
        switch(direction) 
        {
            case 'U': 
            {
                for (var i = 0; i < distance; i++)
                {
                    Y++;
                    PositionChanged.Invoke(this, args);
                }
                break;
            }
            case 'D':
            {
                for (var i = 0; i < distance; i++)
                {
                    Y--;
                    PositionChanged.Invoke(this, args);
                }
                break;
            }
            case 'L':
            {
                for (var i = 0; i < distance; i++)
                {
                    X--;
                    PositionChanged.Invoke(this, args);
                }
                break;
            }
            case 'R':
            {
                for (var i = 0; i < distance; i++)
                {
                    X++;
                    PositionChanged.Invoke(this, args);
                }
                break;
            }
            default:
                throw new Exception("Unknown move.");
        }
        
    }
}

