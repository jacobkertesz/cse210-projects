class Reflection : Activity
{
    private List<string> _messages2 = new List<string>();
    public Reflection(string n, string d) :base(n, d)
    {
        _messages = ["Think of a time when you stood up for someone else", "Think of a time when you did something really difficult", "Think of a time when you helped someone in need", "Think of a time when you did something truly selfless"];
        _messages2 = ["Why was this experience meaningful to you?", "Have you ever done anything like this before?", "How did you get started?", "How did you feel when it was complete?", "What made this time different than other times when you were not as successful?", "What is your favorite thing about this experience?", "What could you learn from this experience that applies to other situations?", "What did you learn about yourself through this experience?", "How can you keep this experience in mind in the future?"];
    }
    public void RunReflection()
    {
        Random r = new Random();
        Console.Clear();
        Console.WriteLine($"{_messages[r.Next(_messages.Count)]}\n");
        AnimateLoad(3);

        while (_countdown > 0)
        {
            if (_countdown < 8)
            {
                Console.WriteLine($"{_messages2[r.Next(_messages2.Count)]}\n");
                AnimateLoad(_countdown);
                _countdown = 0;
            }
            else
            {
                Console.WriteLine($"{_messages2[r.Next(_messages2.Count)]}\n");
                AnimateLoad(8);
                _countdown -= 8;
            }
        }
        Console.WriteLine($"You've completed {_time} seconds of the {_name}");
        Thread.Sleep(2000);
    }
}