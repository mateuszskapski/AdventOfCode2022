public class Head : IObservable<Head>
{
    public int X {get; protected set;}
    public int Y {get; protected set;}

    protected IObserver<Head> Observer { get; private set;}

    public Head(int x = 0, int y = 0)
    {
        X = x;
        Y = y;
    }

    public IDisposable Subscribe(IObserver<Head> observer)
    {
        if (Observer is null)
        {
            Observer = observer;
        }

        return new Unsubscriber();
    }  

    public void Move(char direction, int distance) 
    {
        switch(direction) 
        {
            case 'U': 
            {
                for (var i = 0; i < distance; i++)
                {
                    Y++;
                    Observer.OnNext(this);
                }
                break;
            }
            case 'D':
            {
                for (var i = 0; i < distance; i++)
                {
                    Y--;
                    Observer.OnNext(this);
                }
                break;
            }
            case 'L':
            {
                for (var i = 0; i < distance; i++)
                {
                    X--;
                    Observer.OnNext(this);
                }
                break;
            }
            case 'R':
            {
                for (var i = 0; i < distance; i++)
                {
                    X++;
                    Observer.OnNext(this);
                }
                break;
            }
            default:
                throw new Exception("Unknown move.");
        }
    }
}

public class Unsubscriber : IDisposable
{
    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
