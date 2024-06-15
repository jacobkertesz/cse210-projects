class Listing : Activity
{
    private List<string> _answers = new List<string>();
    private string _answer = "";
    public Listing(string n, string d) :base(n, d)
    {
        _messages = ["Who are people that you appreciate?", "What are personal strengths of yours?", "Who are people that you have helped this week?", "When have you felt the Holy Ghost this month?", "Who are some of your personal heroes?"];
    }
    public void RunListing()
    {
        Random r = new Random();
        
        Console.Clear();
        Console.WriteLine($"{_messages[r.Next(_messages.Count)]}\n");

        Console.WriteLine("Think about the prompt and begin answering in\n");
        for (int i = 5; i > 0; i--)
        {
            Console.Write($"{i}\b");
            Thread.Sleep(1000);
        }

        Console.WriteLine("Begin\n");
        DateTime nowTime = DateTime.Now;
        DateTime endTime = nowTime.AddSeconds(_countdown);
        while (nowTime < endTime)
        {
            nowTime = DateTime.Now;
            _answer = Console.ReadLine();

            if (_answer != "")
            {
                _answers.Add(_answer);
                _answer = "";
            }
        }
        Console.WriteLine($"You gave {_answers.Count} answers");
        Thread.Sleep(1000);

        Console.WriteLine($"You've completed {_time} seconds of the {_name}");
        Thread.Sleep(2000);
    }
}