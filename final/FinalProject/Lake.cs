class Lake : Region
{
    public Lake(string n) : base(n)
    {
        _regionType = "lake";
    }
    public override bool RegionEffect(Creature c)
    {
        //Creature must be able to swim to enter the lake
        bool output = false;
        List<string> traitType = c.GetTraits();

        if (traitType.Contains("swim") && c.ValueTest(c, "swim", 0))
        {
            output = true;
        }
        return output;
    }
}