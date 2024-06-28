class Checklist : Goal
{
    private int _maxComplete;
    public Checklist(string n, int c, string d, int m) : base(n,c,d)
    {
        _maxComplete = m;
        _type = "checklist";
    }
    public override int GivePoints()
    {
        int points = 0;
        
        if (_complete <= _maxComplete)
        {
            if (_complete == _maxComplete) 
            {
                points = 100 * _maxComplete;
            }
            else
            {
                points = 50 * _complete;
            }
        }
        else
        {
            Console.WriteLine("Checklist Goal Already Completed");
            Thread.Sleep(1000);
        }
        
        return points;
    }
    public override void ShowGoal()
    {
        Console.WriteLine($"{_name} | {_type} | [{_complete}/{_maxComplete}] | {_date}");
    }
    public override string CopyData()
    {
        return $"{_name}*{_type}*{_complete}*{_maxComplete}*{_date}";
    }
}