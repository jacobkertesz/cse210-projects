class Breathing : Activity
{
    public Breathing(string n, string d) :base(n, d)
    {
        _messages = ["Breathe In", "Breathe Out"];
    }

    public void RunBreathing()
    {
        int index= 1;

        while (_countdown > 0)
        {
            if (index== 1)
            {
                index= 0;
            }
            else 
            {
                index= 1;
            }

            Console.Clear();

            if (_countdown < 5)
            {
                Console.WriteLine($"{_messages[index]}");
                AnimateLoad(_countdown);
                _countdown = 0;
            }
            else 
            {
                Console.WriteLine($"{_messages[index]}");
                AnimateLoad(5);
                _countdown -= 5;
            }
        }
        Console.WriteLine($"You've completed {_time} seconds of the {_name}");
        Thread.Sleep(2000);
    }
}