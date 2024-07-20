class Woodland : Region
{
    public Woodland(string n) : base(n)
    {
        _regionType = "woodland";
    }
    public override bool RegionEffect(Creature c)
    {
        //Creatures can climb trees in the woodland
        bool output = false;
        List<string> traitType = c.GetTraits();

        if (traitType.Contains("climb") && c.ValueTest(c, "climb", 0))
        {
            output = true;
        }
        return output;
    }
}