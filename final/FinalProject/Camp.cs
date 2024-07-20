class Camp : Region
{
    public Camp(string n) : base(n)
    {
        _regionType = "camp";
    }
    public override bool RegionEffect(Creature c)
    {
        //Only creatures with the bypass trait can enter the camp
        bool output = false;
        List<string> traitType = c.GetTraits();

        if (traitType.Contains("bypass") && c.ValueTest(c, "bypass", 0))
        {
            output = true;
        }
        return output;
    }
}