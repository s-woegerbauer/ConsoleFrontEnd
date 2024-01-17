using Chess_Cabs.Utils;

namespace ConsoleFrontEnd;

public class Program
{
    public static double XOffSet = 8;
    public static double YOffSet = 42;
    
    public static void Main()
    {
        //MaximizeWindow.Go();
        DisableConsoleQuickEdit.Go();
        //CalibrateFrontEnd();
        Console.WriteLine(XOffSet);
        Console.WriteLine(YOffSet);
        CountButton.Start();
    }

    static int Count = 0;
    static Button<DateTime> CountButton = new Button<DateTime>(5, 5, 10, 3, CountOneUp, text:"0", delayMs:0);
    
    public static void CountOneUp(DateTime time)
    {
        Count++;
        CountButton.Text = Count.ToString();
        CountButton.Draw();
    }

    public static void CalibrateFrontEnd()
    {
        Console.Clear();
        Console.ReadKey();  
        Console.SetCursorPosition(0,0);
        Console.Write("*");
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Click on the point above to Calibrate!");
        int[] pos = Mouse.WaitForInput();
        XOffSet = pos[0];
        YOffSet = pos[1];
        Console.Clear();
    }

    public static void WriteLine(int width)
    {
        Console.Write("+");
        for (int i = 0; i < width; i++)
        {
            Console.Write("-");
        }
        Console.Write("+");
    }
}