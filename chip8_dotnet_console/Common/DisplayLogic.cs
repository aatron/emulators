public class DisplayLogic
{
    public const int Width = 64;
    public const int Height = 32;

    private IRegisters _registers;
    private IDisplay _display;

    public bool[,] Pixels = new bool[Width, Height];
    
    public DisplayLogic(IRegisters registers, IDisplay display)
    {
        _registers = registers;
        _display = display;
    }

    public void UpdatePixels(byte[] newSpritePixelRows)
    {
        // Wrap round the beginning of the sprite but if sprite is too big, it will be cut off
        var startX = _registers.VX % Width;
        var startY = _registers.VY % Height;
        
        // Reset the flag register
        _registers.VF = 0;

        // Start at the index of the sprite
        var startPixelIndex = _registers.I;
        
        // Iterate over the sprite rows
        for(int newSpriteRowIndex = startPixelIndex; newSpriteRowIndex < startPixelIndex + newSpritePixelRows.Length; newSpriteRowIndex++)
        {
            // If the sprite row is beyond the bottom edge of the display, break
            if (newSpriteRowIndex >= Height)
            {
                break;
            }

            // Get the byte at the current index
            var newSpriteRow = newSpritePixelRows[newSpriteRowIndex];

            // Iterate over the sprite columns
            for(int newSpriteColumnIndex = 0; newSpriteColumnIndex < 8; newSpriteColumnIndex++)
            {
                var x = startX + newSpriteColumnIndex;
                var y = startY + newSpriteRowIndex;

                // Check if the column is beyond the right edge of the display
                if (x >= Width)
                {
                    break;
                }

                var screenPixel = Pixels[x, y];
                var newPixel = (newSpriteRow & (1 << newSpriteColumnIndex)) != 0; // Bit value at the current column

                Pixels[x, y] = screenPixel ^ newPixel;  // XOR the current pixel with the new pixel

                if (screenPixel && newPixel)
                {
                    _registers.VF = 1;
                    _display.DrawPixel(x, y, false);
                }
                else if (!screenPixel && newPixel)
                {
                    _display.DrawPixel(x, y, true);
                }
            }
        }
    }
}
