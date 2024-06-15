class Activity
{
    protected string _name;
    private string _description;
    protected int _time;
    protected int _countdown;
    protected List<string> _messages = new List<string>();

    public Activity(string n, string d)
    {
        _name = n;
        _description = d;
    }

    public void AskForTime()
    {
        Console.Write("\n\nTime in seconds: ");
        string numberString = Console.ReadLine();
        int numberInt = Int32.Parse(numberString);
        _time = numberInt;
        _countdown = numberInt;
    }
    public int GetTime()
    {
        return _time;
    }
    public void AnimateLoad(int length)
    {
        int miliSec = 100;
        string clear = "\b\b\b\b\b\b\b";

        while (length > 0)
        {
            Console.Write("(O    )"); //1
            Thread.Sleep(miliSec);
            Console.Write(clear);

            Console.Write("(OO   )"); //2
            Thread.Sleep(miliSec);
            Console.Write(clear);

            Console.Write("(OOO  )"); //3
            Thread.Sleep(miliSec);
            Console.Write(clear);

            Console.Write("(OOOO )"); //4
            Thread.Sleep(miliSec);
            Console.Write(clear);

            Console.Write("(OOOOO)"); //5
            Thread.Sleep(miliSec);
            Console.Write(clear);

            Console.Write("( OOOO)"); //6
            Thread.Sleep(miliSec);
            Console.Write(clear);

            Console.Write("(  OOO)"); //7
            Thread.Sleep(miliSec);
            Console.Write(clear);

            Console.Write("(   OO)"); //8
            Thread.Sleep(miliSec);
            Console.Write(clear);

            Console.Write("(    O)"); //9
            Thread.Sleep(miliSec);
            Console.Write(clear);

            Console.Write("(     )"); //10
            Thread.Sleep(miliSec);
            Console.Write(clear);

            length --;            
        }
    }
    public void StartInfo()
    {
        Console.Clear();
        Console.WriteLine($"{_name} \n\n{_description}");
    }
}