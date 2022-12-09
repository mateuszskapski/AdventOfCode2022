public record Position(int x, int y);
public class Tail
{
    private int _x;
    private int _y;
    private readonly Head _head;
    HashSet<Position> _positions = new HashSet<Position>();
    public int UniquePositions => _positions.Count;

    public Tail(int x, int y, Head head)
    {
        
        head.PositionChanged += (s, e) => OnPositionChanged(s, e);
        _x = x;
        _y = y;
        _head = head;
        _positions.Add(new Position(x, y));
    }

    private void OnPositionChanged(object sender, HeadPositionChangedEventArgs e)
    {
        if (_head.X == _x && _head.Y == _y)
            return;
        if ((_head.X == _x - 1 || _head.X == _x + 1) && Math.Abs(_head.Y - _y) < 2)
            return;
        if ((_head.Y == _y - 1 || _head.Y == _y + 1) && Math.Abs(_head.X - _x) < 2)
            return;

        switch (e.Direction)
        {
            case 'U': 
            {
                if (Math.Abs(_head.Y - _y) == 2)
                {
                    _x = _head.X;
                    _y = _head.Y - 1;
                }
                else
                {
                    if (_x > _head.X)
                        _x--;
                    else if (_x < _head.X)
                        _x++;
                    else
                        _y++;
                }

                _positions.Add(new Position(_x, _y));
                break;
            }
            case 'D': 
            {
                if (Math.Abs(_head.Y - _y) == 2)
                {
                    _x = _head.X;
                    _y = _head.Y + 1;
                }
                else
                {
                    if (_x > _head.X)
                        _x--;
                    else if (_x < _head.X)
                        _x++;
                    else
                        _y--;
                }

                _positions.Add(new Position(_x, _y));
                break;
            }
            case 'R': 
            {
                if (Math.Abs(_head.X - _x) == 2)
                {
                    _x = _head.X - 1;
                    _y = _head.Y;
                }
                else
                {
                    if (_y > _head.Y)
                        _y--;
                    else if (_y < _head.Y)
                        _y++;
                    else
                        _x++; 
                }
                _positions.Add(new Position(_x, _y));
                break;
            }
            case 'L': 
            {
                if (Math.Abs(_head.X - _x) == 2)
                {
                    _x = _head.X + 1;
                    _y = _head.Y;
                }
                else
                {
                    if (_y > _head.Y)
                        _y--;
                    else if (_y < _head.Y)
                        _y++;
                    else
                        _x--; 
                }
                _positions.Add(new Position(_x, _y));
                break;
            }
            default: throw new Exception("Invalid move.");
        }
    }
}
