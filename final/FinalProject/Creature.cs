using System.Diagnostics.Contracts;
using System.Xml;

class Creature
{
    Random random = new Random();
    private string _id;
    private int _hp = 6;
    private string _status = "A";
    private List<string> _traitType = new List<string>();
    private List<int> _traitStrength = new List<int>();
    private int _behaviorNeutral;
    private string _behaviorAttack;
    private string _activity;
    private int _aggression;
    private int _currentRegionIndex;
    private List<string> _allTraits = new List<string>{
        "sight", "smell", "hearing", "seismic", "thermal", "electro",
        "tough", "resistant", "spiked", "camo", "toxic", "shock",
        "mimicry", "traps", "tools", "sabotage", "tactical", "learning",
        "stealth", "speed", "flight", "swim", "tunnel", "climb",
        "venom", "grab", "rush", "reach", "range", "power",
        "mental", "regen", "bypass", "teleport", "nightmare", "shapeshift"
    };
    private List<string> _allAttacks = new List<string>{
        "ambush", "stalk", "pester", "charge", "defend", "evade"
    };
    private List<string> _allActivity = new List<string>{
        "diurnal", "nocturnal", "matutinal", "vespertine", "crepuscular", "cathemeral"
    };

    public void RandomCreature()
    {
        //creating _id
        _id = $"{random.Next(10)}{random.Next(10)}{random.Next(10)}";

        //creating _traitType
        List<string> copyTraits = new List<string>();
        copyTraits = _allTraits;

        string sense = copyTraits[random.Next(6)];
        _traitType.Add(sense);
        copyTraits.Remove(sense);

        for (int i = 0; i < 5; i++)
        {
            int count = copyTraits.Count;
            string trait = copyTraits[random.Next(count)];
            _traitType.Add(trait);
            copyTraits.Remove(trait);
        }

        //creating _traitStrength
        for (int i = 0; i < 6; i++)
        {
            int strength = random.Next(6) + 1;
            _traitStrength.Add(strength);
        }

        //creating _behaviorNeutral
        _behaviorNeutral = random.Next(6) + 1;

        //creating _behaviorAttack
        _behaviorAttack = _allAttacks[random.Next(6)];

        //creating _activity
        _activity = _allActivity[random.Next(6)];

        //creating _aggresion
        _aggression = random.Next(7);
    }

//Getters
    public string GetId()
    {
        return _id;
    }
    public string GetStatus()
    {
        return _status;
    }
    public List<string> GetTraits()
    {
        return _traitType;
    }
    public List<int> GetStrength()
    {
        return _traitStrength;
    }
    public int GetNeutral()
    {
        return _behaviorNeutral;
    }
    public string GetAttack()
    {
        return _behaviorAttack;
    }
    public string GetActivity()
    {
        return _activity;
    }
    public int GetAggression()
    {
        return _aggression;
    }
    public int GetRegionI()
    {
        return _currentRegionIndex;
    }

//Setters
    public void SetStatus(string s)
    {
        _status = s;
    }
    public void SetHp(int h)
    {
        _hp = h;
        if (_hp < 1)
        {
            SetStatus("dead");
        }
    }

//Methods
    public void SetRegion(List<Region> regions)
    {
        _currentRegionIndex = random.Next(regions.Count);
        regions[_currentRegionIndex].AddCreature(this);
    }
    public void ChangeRegion(List<Region> regions)
    {
        int direction = random.Next(2);
        int newRegionIndex = _currentRegionIndex;

        if (direction == 0 && _currentRegionIndex == 0)
        {
            newRegionIndex += 3;
        }
        else if (direction == 1 && _currentRegionIndex == 3)
        {
            newRegionIndex -= 3;
        }
        else if (direction == 0)
        {
            newRegionIndex -= 1;
        }
        else 
        {
            newRegionIndex += 1;
        }

        regions[_currentRegionIndex].RemoveCreature(this);
        regions[newRegionIndex].AddCreature(this);
        _currentRegionIndex = newRegionIndex;
    }
    public bool IsActive(int time)
    {
        bool output = false;

        if (_activity == "diurnal" && (time < 7))
        {
            output = true;
            _status = "A";
        }
        else if (_activity == "nocturnal" && (time > 6))
        {
            output = true;
            _status = "A";
        }
        else if (_activity == "matutinal" && (time < 4 | time > 9))
        {
            output = true;
            _status = "A";
        }
        else if (_activity == "vespertine" && (time > 3 && time < 10))
        {
            output = true;
            _status = "A";
        }
        else if (_activity == "crepuscular" && (time == 1 | (time > 5 && time < 9) | (time > 10)))
        {
            output = true;
            _status = "A";
        }
        else if (_activity == "cathemeral" && (time % 2 != 0))
        {
            output = true;
            _status = "A";
        }
        else
        {
            _status = "Z";
        }
        return output;
    }
    public bool SenseCreature(Creature target, int time, int type = 0)
    {
        bool output;

        int sensed = 0;
        int difficulty = 0;
        if (type == 1)
        {
            difficulty += 2;
        }
        for (int i = 0; i < 6; i++)
        {
            string trait = _traitType[i];
            if (trait == "sight")
            {
                if (time > 6)
                {
                    difficulty += 2;
                }
                if (target._traitType.Contains("camo"))
                {
                    int targetI = target._traitType.IndexOf("camo");
                    int targetValue = random.Next(6) + 1;
                    if (targetValue <=target._traitStrength[targetI])
                    {
                        difficulty += 2;
                    }
                }

                int value = random.Next(6) + 1;
                if (value <=_traitStrength[i] - difficulty)
                {
                    sensed++;
                }
            }
            else if (trait == "smell")
            {
                int value1 = random.Next(6) + 1;
                difficulty += random.Next(3);
                if (value1 <=_traitStrength[i] - difficulty) 
                {
                    sensed++;
                }
            }
            else if (trait == "hearing")
            {
                if (target.GetActivity() == "Z")
                {
                    difficulty += 2;
                }
                int value = random.Next(6) + 1;
                if (value <=_traitStrength[i] - difficulty)
                {
                    sensed++;
                }
            }
            else if (trait == "seismic")
            {
                if (target._traitType.Contains("flight"))
                {
                    int targetI = target._traitType.IndexOf("flight");
                    int targetValue = random.Next(6) + 1;
                    if (targetValue <=target._traitStrength[targetI])
                    {
                        difficulty += 2;
                    }
                }
                int value = random.Next(6) + 1;
                if (value <=_traitStrength[i] - difficulty)
                {
                    sensed++;
                }
            }
            else if (trait == "thermal")
            {
                if (time < 7)
                {
                    difficulty += 2;
                }
                int value = random.Next(6) + 1;
                if (value <=_traitStrength[i] - difficulty)
                {
                    sensed++;
                }
            }
            else if (trait == "electro")
            {
                difficulty += 2;
                if (target._traitType.Contains("shock"))
                {
                    int targetI = target._traitType.IndexOf("shock");
                    int targetValue = random.Next(6) + 1;
                    if (targetValue <=target._traitStrength[targetI])
                    {
                        difficulty -= 2;
                    }
                }
                int value = random.Next(6) + 1;
                if (value <=_traitStrength[i] - difficulty)
                {
                    sensed++;
                }
            }
        }

        if (sensed > 0)
        {
            output = true;
            _status = "?";
        }
        else
        {
            output = false;
        }
        return output;
    }
    public int UpdateAggression(int day)
    {
        int output;
        if (_behaviorAttack == "evade")
        {
            output = _aggression + day - _hp - 1;
        }
        else
        {
            output = _aggression + day + _hp - 7;
        }
        if (output > 6)
        {
            output = 6;
        }
        else if (output < 0)
        {
            output = 0;
        }
        return output;
    }
    public List<Creature> ChooseTarget(List<Creature> targets)
    {
        while (targets.Count > 1)
        {
            targets.Remove(targets[random.Next(targets.Count)]);
        }
        
        return targets;
    }
    public void PrepareAttack(Creature target, Clock clock)
    {
        if (random.Next(6) + 1 <= UpdateAggression(clock.GetRound()))
        {
            if (_behaviorAttack == "ambush")
            {
                int value = random.Next(6) + 1;
                if (value <= target._behaviorNeutral | target._status == "Z")
                {
                    Fight(target, clock);
                }
            }
            else if (_behaviorAttack == "stalk")
            {
                if (target._status != "?")
                {
                    Fight(target, clock);
                }
            }
            else if (_behaviorAttack == "pester")
            {
                Fight(target, clock, "pester");
            }
            else if (_behaviorAttack == "charge")
            {
                Fight(target, clock, "charge");
            }
        }
    }
    public void Fight(Creature target, Clock clock, string type = "normal")
    {
        int aggro;
        if (type == "charge")
        {
            aggro = 6;
        }
        else
        {
            aggro = UpdateAggression(clock.GetRound());
        }

        int value = random.Next(6) + 1;
        if (value <= aggro)
        {
            Console.WriteLine($"{_id} is attacking {target._id}");
            Thread.Sleep(1000);
            int attack = random.Next(6) + 1;
            if (attack <= _hp)
            {
                target.SetHp(target._hp - 1);
                if (type == "pester")
                {
                    Flee(target, clock);
                }
                else
                {
                    target.Fight(this, clock);
                }
            }
            else
            {
                target.Fight(this, clock);
            }
        }
        else
        {
            Flee(target, clock);
        }
    }
    public void Flee(Creature target, Clock clock)
    {
        int bonus = 1;
        int value1 = random.Next(6) + bonus;
        int value2 = random.Next(6) + bonus;
        if (value1 <= value2)
        {
            target.Fight(this, clock);
        }
        else
        {
            Console.WriteLine($"{_id} fled");
        }
        
    }
}