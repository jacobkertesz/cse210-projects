class Region 
{
    private string _name;
    private List<Creature> _creatures = new List<Creature>();
    
    public string GetName()
    {
        return _name;
    }
    public List<Creature> GetCreatures()
    {
        return _creatures;
    }

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
    public bool CheckSense()
    {
        bool output = false;
        if (_creatures.Count > 1)
        {
            output = true;
        }
        return output;
    }
}