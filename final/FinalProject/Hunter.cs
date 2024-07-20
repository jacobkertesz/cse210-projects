class Hunter : Creature
{
    private int _homeRegionIndex;
    public Hunter() : base()
    {
        //All hunters use these traits
        _id = "Hunter";
        _traitType = new List<string>{"sight", "hearing", "swim", "tactical", "range", "bypass"};
        _traitStrength = new List<int>{4, 2, 2, 4, 6, 6};
        _behaviorNeutral = 2;
        _behaviorAttack = "stalk";
        _activity = "diurnal";
        _aggression = 5;
    }

    public override void SetRegion(List<Region> regions)
    {
        //Hunters always start at the camp
        foreach (Region region in regions)
        {
            string type = region.GetRegionType();
            if (type == "camp")
            {
                _currentRegionIndex = regions.IndexOf(region);
                _homeRegionIndex = _currentRegionIndex;
                _currentRegionType = type;
            }
        }
    }

    public override void ChangeRegion(List<Region> regions, Clock clock)
    {
        //The same as Creature.ChangeRegion except Hunters return to camp at the end of the day
        int randomValue = random.Next(6) + 1;

        if (randomValue > _behaviorNeutral | clock.GetTime() == 6)
        {
            int newRegionIndex;
            if (clock.GetTime() == 6)
            {
                newRegionIndex = _homeRegionIndex;
            }
            else
            {
                do
                {
                    newRegionIndex = random.Next(regions.Count);
                }
                while (newRegionIndex == _currentRegionIndex);
            }

            bool key = true;
            if (regions[newRegionIndex].GetRegionType() == "camp" | regions[newRegionIndex].GetRegionType() == "lake")
            {
                key = regions[newRegionIndex].RegionEffect(this);
            }
            
            if (key)
            {
                regions[_currentRegionIndex].RemoveCreature(this);
                regions[newRegionIndex].AddCreature(this);
                _currentRegionIndex = newRegionIndex;
                _currentRegionType = regions[_currentRegionIndex].GetRegionType();
            }
        }
    }
}
