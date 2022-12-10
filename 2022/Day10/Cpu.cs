public class Cpu
{
    public record CpuInstruction(string Type, int Value);

    Queue<CpuInstruction> _instructions = new Queue<CpuInstruction>();
    
    int CycleCounter { get; set; } = 0;
    int RegisterX { get; set; } = 1;
    
    public bool Stop { get; set; } = false;
    public event EventHandler<CpuCycleFinishedEventArgs> CycleStarted;
    public event EventHandler<CpuCycleFinishedEventArgs> CycleFinished;

    public void Push(CpuInstruction instruction) => _instructions.Enqueue(instruction);

    public void Execute()
    {
        if (_instructions.TryDequeue(out var instruction))
        {
            Console.WriteLine($"Processing instruction [{instruction.Type} {instruction.Value}]");

            var instructionCycle = 0;
            ProcessInstruction(instruction, ref instructionCycle);
        }
    }

    public void ResetRegister() => RegisterX = 1;

    private void ProcessInstruction(CpuInstruction instruction, ref int instructionCycle)
    {
        CycleCounter++;
        instructionCycle++;
        
        CycleStarted?.Invoke(this, new CpuCycleFinishedEventArgs(CycleCounter, RegisterX));
        
        switch (instruction.Type)
        {
            case "addx": 
            {
                if (instructionCycle < 2)
                {
                    //CycleFinished?.Invoke(this, new CpuCycleFinishedEventArgs(CycleCounter, RegisterX));
                    ProcessInstruction(instruction, ref instructionCycle);
                }
                else
                {
                    //CycleFinished?.Invoke(this, new CpuCycleFinishedEventArgs(CycleCounter, RegisterX));
                    RegisterX += instruction.Value;
                }
            } 
            break;
            case "noop": break;
            default: break;
        }

        CycleFinished?.Invoke(this, new CpuCycleFinishedEventArgs(CycleCounter, RegisterX));
    }
}

public class CpuCycleFinishedEventArgs : EventArgs
{
    public CpuCycleFinishedEventArgs(int cycleCounter, int registerX)
    {
        CycleCounter = cycleCounter;
        RegisterX = registerX;
    }

    public int CycleCounter { get; set; } 
    public int RegisterX { get; set; } 
}