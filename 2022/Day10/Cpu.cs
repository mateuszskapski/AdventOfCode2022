public class Cpu
{
    public record CpuInstruction(string Type, int Value);

    Queue<CpuInstruction> _instructions = new Queue<CpuInstruction>();
    
    int CycleCounter { get; set; } = 0;
    int RegisterX { get; set; } = 1;
    
    public bool Stop { get; set; } = false;
    public event EventHandler<CpuCycleEventArgs> CycleStarted;
    public event EventHandler<CpuCycleEventArgs> CycleFinished;
    public event EventHandler<CpuCycleEventArgs> DuringCycle;

    public void Push(CpuInstruction instruction) => _instructions.Enqueue(instruction);

    public void Execute()
    {
        if (_instructions.TryDequeue(out var instruction))
        {
            //Console.WriteLine($"Processing instruction [{instruction.Type} {instruction.Value}]");

            CycleCounter++;
            CycleStarted?.Invoke(this, new CpuCycleEventArgs(CycleCounter, RegisterX));
            ProcessInstruction(instruction);
            CycleFinished?.Invoke(this, new CpuCycleEventArgs(CycleCounter, RegisterX));
        }
    }

    public void ResetRegister() => RegisterX = 1;

    private void ProcessInstruction(CpuInstruction instruction)
    {   
        DuringCycle?.Invoke(this, new CpuCycleEventArgs(CycleCounter, RegisterX));

        switch (instruction.Type)
        {
            case "addx": 
            {
                CycleCounter++;
                DuringCycle?.Invoke(this, new CpuCycleEventArgs(CycleCounter, RegisterX));
                RegisterX += instruction.Value;
            } 
            break;
            case "noop":  break;
            default: break;
        }
    }
}

public class CpuCycleEventArgs : EventArgs
{
    public CpuCycleEventArgs(int cycleCounter, int registerX)
    {
        CycleCounter = cycleCounter;
        RegisterX = registerX;
    }

    public int CycleCounter { get; set; } 
    public int RegisterX { get; set; } 
}