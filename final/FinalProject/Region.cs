class Region 
{
    protected string _name;
    protected List<Creature> _creatures = new List<Creature>();
    protected string _regionType = "";
    
    public Region(string n)
    {
        _name = n;
    }

// Getters
    public string GetName()
    {
        return _name;
    }
    public string GetRegionType()
    {
        return _regionType;
    }
    public List<Creature> GetCreatures()
    {
        return _creatures;
    }

// Setters
    public void SetName(string n)
    {
        _name = n;
    }
    public void AddCreature(Creature creature)
    {
        _creatures.Add(creature);
    }
    public void RemoveCreature(Creature creature)
    {
        _creatures.Remove(creature);
    }

// Methods
    public bool CheckSense()
    {
        bool output = false;
        if (_creatures.Count > 1)
        {
            output = true;
        }
        return output;
    }
    public virtual bool RegionEffect(Creature c)
    {
        return true;
    }
}