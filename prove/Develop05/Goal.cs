class Goal{
    protected string _name;
    protected int _complete;
    protected string _date;
    protected string _type;

    public Goal(string n, int c, string d)
    {
        _name = n;
        _complete = c;
        _date = d;
    }

    public void RecordComplete()
    {
        _complete++;
        _date = DateTime.Now.ToShortDateString();
    }
    public virtual int GivePoints()
    {
        return 0;
    }
    public void SaveGoal(List<Goal> g, string fileName)
    {
        //save a List<Goal> info to another file
    }
    public virtual void ShowGoal()
    {
        Console.WriteLine($"{_name} | {_type} | [{_complete}] | {_date}");
    }
    public virtual string CopyData()
    {
        return $"{_name}*{_type}*{_complete}*{_date}";
    }
}