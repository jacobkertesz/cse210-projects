class River : Woodland
{
    public River(string n) : base(n)
    {
        _regionType = "river";
    }
    public bool RiverEffect(Creature c)
    {
        //Creatures can swim in the river
        bool output = false;
        List<string> traitType = c.GetTraits();

        if (traitType.Contains("swim") && c.ValueTest(c, "swim", 0))
        {
            output = true;
        }
        return output;
    }
}