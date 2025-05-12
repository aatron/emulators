public class DisplayConsole : IDisplay
{
    public void DrawPixel(int x, int y, bool pixelValue)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(pixelValue ? "█" : " ");
    }
}
