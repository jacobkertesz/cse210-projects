class Ongoing : Goal
{
    public Ongoing(string n, int c, string d) : base(n,c,d)
    {
        _type = "ongoing";
    }   
    public override int GivePoints()
    {
        return 100 * _complete;
    }
}