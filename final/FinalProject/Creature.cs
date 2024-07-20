using System.Diagnostics.Contracts;
using System.Xml;

class Creature
{
    protected Random random = new Random();
    protected string _id;
    protected int _hp = 6;
    protected string _status = "A";
    protected List<string> _traitType = new List<string>();
    protected List<int> _traitStrength = new List<int>();
    protected int _behaviorNeutral;
    protected string _behaviorAttack;
    protected string _activity;
    protected int _aggression;
    protected int _currentRegionIndex;
    protected string _currentRegionType;
    // 17/36 traits actually do somthing
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

    public Creature(){}

    public void RandomCreature()
    {
        //creating _id
        _id = $"{random.Next(10)}{random.Next(10)}{random.Next(10)}";

        //creating _traitType
        List<string> copyTraits = _allTraits;

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
    public int GetHp()
    {
        return _hp;
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
            _hp = 0;
            SetStatus("dead");
        }
    }

//Methods
    public virtual void SetRegion(List<Region> regions)
    //Determines where a creature starts
    {
        bool key = true;
        do
        {
            key = true;
            _currentRegionIndex = random.Next(regions.Count);
            //Only Hunters can start in "camps"
            if (regions[_currentRegionIndex].GetRegionType() == "camp")
            {
                key = false;
            }
            //Only Creatures that can swim can start in "lakes"
            else if (regions[_currentRegionIndex].GetRegionType() == "lake" && _traitType.Contains("swim"))
            {
                key = regions[_currentRegionIndex].RegionEffect(this);
            }
            if (key)
            {
                regions[_currentRegionIndex].AddCreature(this);
            }
        }
        while (key == false);
        _currentRegionType = regions[_currentRegionIndex].GetRegionType();
    }
    public bool ValueTest(Creature c, string trait, int difficulty)
    //Used to determine success or failure when a Creature uses a one of their traits
    {
        int r1 = random.Next(6) + 1;
        
        bool output = false;
        if (trait == "") //Non-specific test
        {
            if (r1 < 4)
            {
                output = true;
            }
        }
        else if (c._traitType.Contains(trait))
        {
            int strengthI = c._traitType.IndexOf(trait);
            if (r1 <=c._traitStrength[strengthI] - difficulty)
            {
                output = true;
            }
        }
        
        return output;
    }
    public virtual void ChangeRegion(List<Region> regions, Clock clock = null)
    //Changes a Creatures region if they move
    {
        int randomValue = random.Next(6) + 1;

        if (randomValue > _behaviorNeutral) //Checks if the Creature moves
        {
            int newRegionIndex;
            do
            {
                newRegionIndex = random.Next(regions.Count);
            }
            while (newRegionIndex == _currentRegionIndex);

            bool key = true;
            //Creatures can only enter camps and lakes if they succeed with the Bypass and Swim traits respectively
            if (regions[newRegionIndex].GetRegionType() == "camp" | regions[newRegionIndex].GetRegionType() == "lake")
            {
                key = regions[newRegionIndex].RegionEffect(this);
            }
            
            if (key)
            {
                //Removes Creature from previous region and adds it to the new one
                regions[_currentRegionIndex].RemoveCreature(this);
                regions[newRegionIndex].AddCreature(this);
                _currentRegionIndex = newRegionIndex;
                _currentRegionType = regions[_currentRegionIndex].GetRegionType();
            }
        } 
    }
    public bool IsActive(int time)
    //Checks the Creatures activity to determine if the Creature is currently Active (A) or Inactive (Z)
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
        else if (_activity == "vespertine" && time > 3 && time < 10)
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
    public bool SenseCreature(Creature target, int time)
    //Using the Creature specific senses, determines if they can sense nearby Creatures.
    {
        bool output;

        int sensed = 0;
        int difficulty = 0;

        for (int i = 0; i < 6; i++)
        {
            string trait = _traitType[i];
            if (trait == "sight")
            {
                //It is harder to see at night
                if (time > 6)
                {
                    difficulty += 2;
                }
                //It is harder to see Creatures with the "camo" trait
                if (target._traitType.Contains("camo"))
                {
                    if (ValueTest(target, "camo", 0))
                    {
                        difficulty += 2;
                    }
                }

                if (ValueTest(this, "sight", difficulty))
                {
                    sensed++;
                }
            }
            else if (trait == "smell")
            {
                //Random wind currents can make finding a creature through scent difficult
                difficulty += random.Next(3);
                if (ValueTest(this, "smell", difficulty)) 
                {
                    sensed++;
                }
            }
            else if (trait == "hearing")
            {
                //It is harder to hear Creatures that are Inactive
                if (target.GetActivity() == "Z")
                {
                    difficulty += 2;
                }

                if (ValueTest(this, "hearing", difficulty))
                {
                    sensed++;
                }
            }
            else if (trait == "seismic") 
            {
                //it is harder to sense vibrations if the target is flying
                if (target._traitType.Contains("flight"))
                {
                    if (ValueTest(target, "flight", 0))
                    {
                        difficulty += 4;
                    }
                }

                if (ValueTest(this, "seismic", difficulty))
                {
                    sensed++;
                }
            }
            else if (trait == "thermal")
            {
                //It is harder to use thermal during the day
                if (time < 7)
                {
                    difficulty += 2;
                }

                if (ValueTest(this, "thermal", difficulty))
                {
                    sensed++;
                }
            }
            else if (trait == "electro")
            {
                //This is a weak sense but finds Creatures with the shock trait better
                difficulty += 2;
                if (target._traitType.Contains("shock"))
                {
                    if (ValueTest(target, "shock", 0))
                    {
                        difficulty -= 4;
                    }
                }

                if (ValueTest(this, "electro", difficulty))
                {
                    sensed++;
                }
            }
        }

        if (sensed > 0) //If the creature finds another, their status changes to "?"
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
    //Aggression is used to determine if a Creature will fight
    //This takes into account how many days have passed, the creatures base aggression, and how hurt the creature is
    {
        int output;
        if (_behaviorAttack == "evade")
        {
            output = day - _hp + 5;
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
    //Narrows potential targets down to one; prevents a creture from targeting multiple creatures at the same time
    {
        while (targets.Count > 1)
        {
            targets.Remove(targets[random.Next(targets.Count)]);
        }
        
        return targets;
    }
    public void PrepareAttack(Creature target, Clock clock)
    //This determines if a creature will try to attack another creature
    {
        if (random.Next(6) + 1 <= UpdateAggression(clock.GetRound()))
        {
            int t = 1000;
            if (_behaviorAttack == "ambush")
            {
                int value = random.Next(6) + 1;
                if (value <= target._behaviorNeutral | target._status == "Z")
                {
                    Console.WriteLine($"{_id} is attacking {target._id}");
                    Thread.Sleep(t);
                    Fight(target, clock);
                }
            }
            else if (_behaviorAttack == "stalk")
            {
                if (target._status != "?")
                {
                    Console.WriteLine($"{_id} is attacking {target._id}");
                    Thread.Sleep(t);
                    Fight(target, clock);
                }
            }
            else if (_behaviorAttack == "pester")
            {
                Console.WriteLine($"{_id} is attacking {target._id}");
                Thread.Sleep(t);
                Fight(target, clock, "pester");
            }
            else if (_behaviorAttack == "charge")
            {
                Console.WriteLine($"{_id} is attacking {target._id}");
                Thread.Sleep(t);
                Fight(target, clock, "charge");
            }
        }
    }
    public void Damage(Creature target, string type = "contact")
    //There are a few different ways a creature can be hurt
    {
        int damage = 1;
        if (_traitType.Contains("power") && ValueTest(this, "power", 0) && type == "contact")
        {
            damage = 2;   
        }
        if (target._traitType.Contains("tough") && ValueTest(target, "tough", 0))
        {
            damage -= 1;
        }
        if (target._traitType.Contains("spiked") && ValueTest(target, "spiked", 0) && type == "contact")
        {
            Damage(this, "self");
        }
        target.SetHp(target._hp - damage);
    }
    public void Fight(Creature target, Clock clock, string type = "normal", string distance = "")
    {
        //When a creature chooses to fight, it will take turns with its target to try to attack or flee until one dies or successfully flees
        if (_status != "dead")
        {
            int aggro;
            if (type == "charge") //If a creature charges, they will not flee
            {
                aggro = 6;
            }
            else
            {
                aggro = UpdateAggression(clock.GetRound());
            }

            int value = random.Next(6) + 1;
            if (value <= aggro) //Determines if the creature will try to fight or flee
            {
                if (distance == "") //Determines how far apart the creatures are
                {
                    if (_traitType.Contains("range"))
                    {
                        distance = "distant";
                    }
                    else 
                    {
                        distance = "contact";
                    }
                }

                if (distance == "distant")
                {
                    //When a creature attacks at a distances they have to have the range trait or change the distance to "contact" using the "rush" trait
                    if (_traitType.Contains("range") && ValueTest(this, "range", 0))
                    {
                        Console.WriteLine($"{_id} attacks at range");
                        Thread.Sleep(1000);

                        Damage(target, distance);
                        if (type == "pester") //Pester means the make one attack then try to flee
                        {
                            Flee(target, clock);
                        }
                        else
                        {
                            target.Fight(this, clock, type, distance); //Switches to the targets turn
                        }
                    }
                    else if (_traitType.Contains("rush") && ValueTest(this, "rush", 0))
                    {
                        Console.WriteLine($"{_id} closed the distance");
                        Thread.Sleep(1000);

                        distance = "contact";

                        Damage(target, distance);
                        if (type == "pester")
                        {
                            Flee(target, clock);
                        }
                        else
                        {
                            target.Fight(this, clock, type, distance);
                        }
                    }
                    else
                    {
                        target.Fight(this, clock, type, distance);
                    }
                }
                else if (distance == "contact")
                {
                    //The range trait does nothing if the distance is "contact"
                    int attack = random.Next(6) + 1;
                    if (attack <= _hp)
                    {
                        Console.WriteLine($"{_id} attacks close-up");
                        Thread.Sleep(1000);

                        Damage(target);
                        if (type == "pester")
                        {
                            Flee(target, clock);
                        }
                        else
                        {
                            target.Fight(this, clock, type, distance);
                        }
                    }
                    else
                    {
                        target.Fight(this, clock, type, distance);
                    }
                }
            }
            else
            {
                Flee(target, clock);
            }
        }
        else
        {
            Console.WriteLine($"{_id} has been killed");
            Thread.Sleep(1000);
        }
    }
    public void Flee(Creature target, Clock clock)
    {   
        //When a creature tries to flee, it must escape its pursuer in order to succeed
        //This takees a random movement trait the fleeing creature has and tests to see if the fleeing creature succeeds and the pursuer fails
        string trait = "";
        List<string> potentialTraits = [];

        for (int i = 0; i < 6; i++)
        {
            string currentTrait = _traitType[i];

            if (currentTrait == "speed" | currentTrait == "flight" | currentTrait == "tunnel" | currentTrait == "flight")
            {
                potentialTraits.Add(currentTrait);
            }
            else if (currentTrait == "swim" && (_currentRegionType == "lake" | _currentRegionType == "river"))
            {
                potentialTraits.Add(currentTrait);
            }
            else if (currentTrait == "climb" && (_currentRegionType == "woodland" | _currentRegionType == "river"))
            {
                potentialTraits.Add(currentTrait);
            }
        }
        if (potentialTraits.Count > 0)
        {
            trait = potentialTraits[random.Next(potentialTraits.Count)];
        }

        if (ValueTest(this, trait, 0) && ValueTest(target, trait, 0) == false)
        {
            Console.WriteLine($"{_id} fled");
        }
        else
        {
            //the Creature faild to flee, and the fight continues
            Console.WriteLine($"{_id} tried to flee");
            Thread.Sleep(1000);

            target.Fight(this, clock);
        }
    }
    public void DailyHeal(Clock clock)
    {
        //This heals at the start of the day
        if (clock.GetTime() == 1 && _hp < 6)
        {
            _hp += 1;
        }
    }
}