class Simple : Goal
{
    public Simple(string n, int c, string d) : base(n,c,d)
    {
        _type = "simple";
    }

    public override int GivePoints()
    {
        int points = 0;
        if (_complete > 0)
        {
           _complete = 1;
            points = 1000;
        }

        return points;
    }
}