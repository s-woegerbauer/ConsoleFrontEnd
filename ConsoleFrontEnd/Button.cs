namespace ConsoleFrontEnd;

public class Button<T>
{
    private string _text;
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public string Text
    {
        get
        {
            return _text;
        }
        set
        {
            Draw();
            _text = value;
        }
    }
    public ConsoleColor BackgroundColor { get; set; }
    public ConsoleColor ForegroundColor { get; set; }
    public Action<T> OnClick { get; set; }
    public T Parameter { get; set; }
    
    public bool OneTimeUse { get; set; }
    public int DelayMs { get; set; }

    public Button(int x, int y, int width, int height, Action<T> onClick, T parameter = default, string text = "",
        ConsoleColor foregroundColor = ConsoleColor.White, bool oneTimeUse = false,
        ConsoleColor backgroundColor = ConsoleColor.Black, int z = 100, int delayMs = 100)
    {
        X = x;
        Y = y;
        z = z;
        _text = text;
        ForegroundColor = foregroundColor;
        BackgroundColor = backgroundColor;
        OnClick = onClick;
        Parameter = parameter;
        Width = width;
        Height = height;
        OneTimeUse = oneTimeUse;
        DelayMs = delayMs;
    }

    public void Start()
    {
        Draw();

        Mouse.WaitForInputAt(X, Y, Width, Height, OnClick, Parameter, OneTimeUse, DelayMs);
    }

    public void Draw()
    {
        Console.SetCursorPosition(X, Y);
        Program.WriteLine(Width);
        for (int y = 0; y < Height; y++)
        {
            Console.SetCursorPosition(X, Y + 1 + y);
            Console.Write("|");
            for (int i = 0; i < Width; i++)
            {
                
                if (i < Text.Length)
                {
                    if (IsMid(Height, y))
                    {
                        Console.Write(Text[i]);
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.Write("|");
        }
        
        Console.SetCursorPosition(X, Y + 1 + Height);
        Program.WriteLine(Width);
    }

    private bool IsMid(int interval, int index)
    {
        if (interval % 2 == 0)
        {
            return interval / 2 == index; 
        }
        else
        {
            return index * 2 + 1 == interval;
        }
    }

    public void Stop()
    {
        Mouse.ToRemove.Add((X, Y, Width, Height));
    }
}