using System.Runtime.InteropServices;

namespace ConsoleFrontEnd;
 
public class Mouse
{
    public static List<(int, int, int, int)> ToRemove = new();
    public enum MouseButton
    {
        LeftMouseButton = 0x01,
        RightMouseButton = 0x02,
        MiddleMouseButton = 0x04,
        Enter = 0x0D
    }
 
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int X, int Y);
 
    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out Point lpPoint);
 
    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool MoveWindow(nint hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
 
    public static int[] GetCur()
    {
        var a = new Point();
        GetCursorPos(out a);
        var sol = new int[2];
        sol[0] = a.X;
        sol[1] = a.Y;
        return sol;
    }
 
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern nint GetConsoleWindow();
 
    [DllImport("user32.dll")]
    public static extern bool GetAsyncKeyState(int button);
 
    public static bool IsMouseButtonPressed(MouseButton button)
    {
        return GetAsyncKeyState((int)button);
    }
 
    public static int[] WaitForInput()
    {
        while (true)
            if (IsMouseButtonPressed(MouseButton.LeftMouseButton))
            {
                Thread.Sleep(200);
                return GetCur();
            }
    }
    
    public static void WaitForInputAt<T>(int xchars, int ychars, int xWidth, int yWidth, 
        Action<T> action, T parameter, bool oneTimeUse, int delayMs)
    {
        while (!ToRemove.Contains((xchars, ychars, xWidth, yWidth)))
        {
            if (IsMouseButtonPressed(MouseButton.LeftMouseButton))
            {
                var pos = GetCoords();
                
                if (pos.left > CharToPixel(false, xchars) &&
                    pos.left < CharToPixel(false, xchars) + CharToPixel(false, xWidth + 4) &&
                    pos.top > CharToPixel(true, ychars) &&
                    pos.top < CharToPixel(true, ychars) + CharToPixel(true, yWidth + 1))
                {
                    action.Invoke(parameter);

                    if (oneTimeUse)
                    {
                        return;
                    }

                    Thread.Sleep(delayMs);
                }
            }
        }

        ToRemove.Remove((xchars, ychars, xWidth, yWidth));
    }

    private static double CharToPixel(bool isVertical, int amount)
    {
        return isVertical ? 20 * amount : 8 * amount;
    }
 
    public static (double left, double top) GetCoords()
    {
        var mousecoordds = WaitForInput();
        return (mousecoordds[0] - Program.XOffSet, mousecoordds[1] - Program.YOffSet);
    }
 
    public struct Point
    {
        public int X;
        public int Y;
    }
}