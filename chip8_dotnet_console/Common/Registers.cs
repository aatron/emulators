public interface IRegisters
{
    /// <summary>
    /// VF is the flag register.
    /// </summary>
    byte VF { get; set; }

    /// <summary>
    /// I is the index register.
    /// </summary>
    byte I { get; set; }

    /// <summary>
    /// VX is the X register.
    /// </summary>
    byte VX { get; set; }

    /// <summary>
    /// VY is the Y register.
    /// </summary>
    byte VY { get; set; }

    /// <summary>
    /// PC is the program counter.
    /// </summary>
    byte PC { get; set; }
}

public class Registers : IRegisters
{
    public byte VF { get; set; }

    public byte I { get; set; }

    public byte VX { get; set; }

    public byte VY { get; set; }

    public byte PC { get; set; }
}
