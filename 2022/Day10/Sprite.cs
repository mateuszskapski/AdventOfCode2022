public class Sprite
{
    int[] CurrentPosition;

    public Sprite()
    {
        CurrentPosition = new int[3];
        CurrentPosition[0] = 0;
        CurrentPosition[1] = 1;
        CurrentPosition[2] = 2;
    }

    public void Move(int registerX)
    {
        CurrentPosition[0] = registerX - 1;
        CurrentPosition[1] = registerX;
        CurrentPosition[2] = registerX + 1;
    }

    public void DrawPixel(int cpuCycle)
    {
        if (CanDrawPixel(cpuCycle-1))
        {
            Console.Write("#");
        }
        else
        {
            Console.Write(".");
        }
    }

    bool CanDrawPixel(int cycle) => 
        cycle == CurrentPosition[1] || cycle == CurrentPosition[0] || cycle == CurrentPosition[2];  
}