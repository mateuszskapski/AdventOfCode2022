public class Sprite
{
    const int InitialPosition = 1; 
    readonly Cpu _cpu;
    readonly int[] _interruptCycles;
    int[] _currentPosition;

    public Sprite(Cpu cpu, int[] interruptCycles)
    {
        _cpu = cpu;
        _interruptCycles = interruptCycles;
        _currentPosition = new int[3];
        SetPosition(InitialPosition);

        var row = 0;
        cpu.DuringCycle += (s, e) => 
        {
            DrawPixel(e.CycleCounter - row);

            if (interruptCycles.Contains(e.CycleCounter))
            {
                row = e.CycleCounter;
                DrawNewLine();
            }
        };

        cpu.CycleFinished += (s, e) => 
        {
            if (interruptCycles.Contains(e.CycleCounter))
            {
                row = e.CycleCounter;
                SetPosition(InitialPosition);
            }
            SetPosition(e.RegisterX);
        };
    }

    void SetPosition(int registerX)
    {
        _currentPosition[0] = registerX - 1;
        _currentPosition[1] = registerX;
        _currentPosition[2] = registerX + 1;
    }

    public void Draw(List<Cpu.CpuInstruction> input)
    {
        foreach (var instruction in input)
        {
            _cpu.Push(instruction);
        }
    }

    void DrawPixel(int cpuCycle)
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

    void DrawNewLine()
    {
        Console.WriteLine();
        SetPosition(InitialPosition);
    }

    bool CanDrawPixel(int cycle) => 
        cycle == _currentPosition[1] || cycle == _currentPosition[0] || cycle == _currentPosition[2];  
}