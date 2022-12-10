public class Day10 : Problem<Cpu.CpuInstruction>
{
    public Day10(int year, int day, string fileName) : base(year, day, fileName)
    {
    }

    protected override object PartOne(List<Cpu.CpuInstruction> input)
    {
        var strengths = new List<int>();
        var interruptCycles = new int[] {20, 60, 100, 140, 180, 220};
        Cpu cpu = new Cpu();
        cpu.DuringCycle += (s, e) => 
        {
            if (interruptCycles.Contains(e.CycleCounter))
            {
                strengths.Add(e.CycleCounter * e.RegisterX);
            }
            
            cpu.Stop = e.CycleCounter == 220 ? true : false;
        };

        foreach (var instruction in input)
        {
            if (!cpu.Stop)
            {
                cpu.Push(instruction);    
                cpu.Execute();
            }
        }

        return strengths.Sum();
    }

    protected override object PartTwo(List<Cpu.CpuInstruction> input)
    {
        var interruptCycles = new int[] {40, 80, 120, 160, 200, 240};
        Cpu cpu = new Cpu();
        Sprite sprite = new Sprite();
        int row = 0;

        cpu.DuringCycle += (s, e) => 
        {
            sprite.DrawPixel(e.CycleCounter - row);

            if (interruptCycles.Contains(e.CycleCounter))
            {
                row = e.CycleCounter;
                Console.WriteLine();
                sprite.Move(1);
            }
        };

        cpu.CycleFinished += (s, e) => 
        {
            if (interruptCycles.Contains(e.CycleCounter))
            {
                row = e.CycleCounter;
                sprite.Move(1);
            }
            sprite.Move(e.RegisterX);
        };

        foreach (var instruction in input)
        {
            if (!cpu.Stop)
            {
                cpu.Push(instruction);    
                cpu.Execute();
            }
        }

        return 0;
    }

    protected override Cpu.CpuInstruction? ProcessLine(string line)
    {
        var instruction = line.Split(" ");
        return new Cpu.CpuInstruction(instruction[0], instruction.Count() == 1 ? 0 : int.Parse(instruction[1]));
    }
}
