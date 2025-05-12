public interface IMemory
{
    Span<byte> GlobalMemory { get; }
    ReadOnlySpan<byte> FontMemory { get; }
}

public class Memory : IMemory
{
    public const int MemorySize = 4096;
    public const int FontStartByteIndex = 0x050;
    public const int FontEndByteIndex = 0x09F;
    public const int FontAreaByteCount = FontEndByteIndex - FontStartByteIndex;

    private byte[] _globalMemory = new byte[MemorySize];
    public Span<byte> GlobalMemory => _globalMemory.AsSpan();
    public ReadOnlySpan<byte> FontMemory => _globalMemory.AsSpan(FontStartByteIndex, FontAreaByteCount); // 0x050 to 0x09F

    public Memory()
    {
        LoadSystemFont();
    }

    private void LoadSystemFont()
    {
        var fontChars = new[]
        {
            Font.Zero, Font.One, Font.Two, Font.Three, Font.Four,
            Font.Five, Font.Six, Font.Seven, Font.Eight, Font.Nine,
            Font.A, Font.B, Font.C, Font.D, Font.E, Font.F
        };

        for (int i = 0; i < fontChars.Length; i++)
        {
            var offset = i * Font.FontPixelHeight;
            fontChars[i].CopyTo(GlobalMemory.Slice(FontStartByteIndex + offset, Font.FontPixelHeight));
        }
    }
}

