public class MainLoop
{
    private const int InstructionsPerFrame = 15;  // 900 instructions per second / 60 frames per second
    private const int FrameTimeMs = 16;  // ~60Hz (1000ms / 60 ≈ 16.67ms)

    private IRegisters _registers;
    private IDisplay _display;
    private IMemory _memory;
    private bool _isRunning = true;

    public MainLoop(IRegisters registers, IDisplay display, IMemory memory)
    {
        _registers = registers;
        _display = display;
        _memory = memory;
    }

    public void Run()
    {
        var stopwatch = new System.Diagnostics.Stopwatch();
        
        while (_isRunning)
        {
            stopwatch.Restart();

            // Execute instructions for this frame
            for (int i = 0; i < InstructionsPerFrame; i++)
            {
                var instruction = (ushort)((_memory.GlobalMemory[_registers.PC] << 8) | _memory.GlobalMemory[_registers.PC + 1]); // two-byte instruction
                DecodeAndExecuteInstruction(instruction);
                _registers.PC += 2;  // CHIP-8 instructions are 2 bytes long
            }

            // Calculate how long to sleep to maintain 60Hz
            var elapsedMs = stopwatch.ElapsedMilliseconds;
            var sleepTime = Math.Max(0, FrameTimeMs - elapsedMs);
            Thread.Sleep((int)sleepTime);
        }
    }

    private void DecodeAndExecuteInstruction(ushort instruction)
    {
        var instructionType = (byte)(instruction >> 12);
        var x = (byte)((instruction >> 8) & 0xF);       // X address in memory
        var y = (byte)((instruction >> 4) & 0xF);       // Y address in memory
        var n = (byte)(instruction & 0xF);              // 4-bit immediate value
        var nn = (byte)(instruction & 0xFF);            // 8-bit immediate value
        var nnn = (ushort)(instruction & 0xFFF);        // 12-bit immediate memory address

        // switch (instructionType)
        // {
        //     case 0x0:
        //         // TODO: Handle CLS instruction
        //         break;
        // }
    }

    public void Stop()
    {
        _isRunning = false;
    }
}
