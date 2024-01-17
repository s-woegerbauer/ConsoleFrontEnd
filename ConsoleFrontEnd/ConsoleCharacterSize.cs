using System.Runtime.InteropServices;

namespace ConsoleFrontEnd;

class ConsoleCharacterSize
{
    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    static extern bool GetCurrentConsoleFont(IntPtr hConsoleOutput, bool bMaximumWindow, out CONSOLE_FONT_INFO lpConsoleCurrentFont);

    [StructLayout(LayoutKind.Sequential)]
    public struct COORD
    {
        public short X;
        public short Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CONSOLE_FONT_INFO
    {
        public int nFont;
        public COORD dwFontSize;
    }

    const int STD_OUTPUT_HANDLE = -11;

    public static (int X, int Y) GetConsoleFontSize()
    {
        IntPtr hConsoleOutput = GetStdHandle(STD_OUTPUT_HANDLE);

        CONSOLE_FONT_INFO consoleFontInfo;
        GetCurrentConsoleFont(hConsoleOutput, false, out consoleFontInfo);

        return (consoleFontInfo.dwFontSize.X, consoleFontInfo.dwFontSize.Y);
    }
}