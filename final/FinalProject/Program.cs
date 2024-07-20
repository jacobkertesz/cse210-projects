using System;
using System.Globalization;
using System.Security.Cryptography;

class Program
{
    static void Main(string[] args)
    {
        //Setup
        Random random = new Random();

        Clock clock = new Clock();
        
        List<Region> regions = new List<Region>{};

        //Creating all regions
        Region newMeadow = new Region("Meadow");
        regions.Add(newMeadow);
        Woodland newWoodland = new Woodland("Woodland");
        regions.Add(newWoodland);
        Lake newLake = new Lake("Lake");
        regions.Add(newLake);
        River newRiver = new River("River");
        regions.Add(newRiver);
        Camp newCamp = new Camp("Camp");
        regions.Add(newCamp);

        //Creating specified number of creatures 
        List<Creature> creatures = new List<Creature>{};
        Console.Write("Number of creatures: ");
        string userString = Console.ReadLine();
        int userInt = Int32.Parse(userString);

        for (int i = 0; i < userInt; i++)
        {
            Creature newC = new Creature();
            creatures.Add(newC);
        }
        //Creating one Hunter
        Hunter newH = new Hunter();
        creatures.Add(newH);

        //Method updates what the user can see
        void UpdateVisuals()
        {
            Console.Clear();
            Console.WriteLine($"Time:{clock.GetTime()} Day:{clock.GetRound()}");
            foreach (Creature creature in creatures)
            {
                string id = creature.GetId();
                string status = creature.GetStatus();
                int aggro = creature.UpdateAggression(clock.GetRound());
                Region region = regions[creature.GetRegionI()];
                string rName = region.GetName();
                int hp = creature.GetHp();
                Console.WriteLine($"{id}[HP({hp}) {status} {aggro}] {rName}");
            }
        }
        
        //Gives Creatures their traits and starting region and shows relays this to the user
        foreach (Creature c in creatures)
        {
            //Hunters have preset info, but Creatures do not
            if (c.GetId() != "Hunter")
            {
                c.RandomCreature();
            }
            c.SetRegion(regions);

            //Shows the initial information
            string id = c.GetId();
            List<string> traits = c.GetTraits();
            List<int> strength = c.GetStrength();
            int neutral = c.GetNeutral();
            string attack = c.GetAttack();
            string activity = c.GetActivity();
            int aggression = c.GetAggression();

            Console.WriteLine($"ID:{id}");
            Console.WriteLine("Traits and Levels:");
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine($"{traits[i]} {strength[i]}");
            }
            Console.WriteLine($"\nTrace: {neutral} \nAttack: {attack} \nActivity: {activity} \nAggression: {aggression}\n");
        }

        
        Console.Write("Continue (y/n)");
        Console.ReadLine();

        string command = "";

        //Runs the simulation for 1 round (day)
        while (command != "n")
        {
            //1 round is split into 12 turns
            for (int i = 0; i < 12; i++)
            {
                int time = clock.GetTime();

                UpdateVisuals();
                Thread.Sleep(1000);

                foreach (Creature c in creatures)
                {
                    //Creatures do nothing if "dead"
                    if (c.GetStatus() != "dead")
                    {
                        c.DailyHeal(clock);
                        if (c.IsActive(time) == true)
                        {
                            int neutral = c.GetNeutral();
                            int regionI = c.GetRegionI();

                            //sense other creatures
                            if (regions[regionI].CheckSense() == true)
                            {
                                List<Creature> targets = new List<Creature>();

                                foreach (Creature nearC in regions[regionI].GetCreatures())
                                {
                                    //Creatures ignore "dead" Creatures
                                    if (nearC.GetId() != c.GetId() && nearC.GetStatus() != "dead")
                                    {
                                        if (c.SenseCreature(nearC, time) == true)
                                        {
                                            targets.Add(nearC);
                                            UpdateVisuals();
                                            Console.WriteLine($"{c.GetId()} sensed {nearC.GetId()}");
                                            Thread.Sleep(1000);
                                        }
                                        UpdateVisuals();
                                    }
                                }
                                //The is a chance Creatures will fight if they sense another
                                targets = c.ChooseTarget(targets);
                                if (targets.Count == 1)
                                {
                                    c.PrepareAttack(targets[0], clock);
                                    Thread.Sleep(1000);
                                }  
                            } 
                            //leave trace or move
                            c.ChangeRegion(regions, clock);
                        }
                    }
                }
                UpdateVisuals();
                Thread.Sleep(1000);

                clock.AdvanceTime();
            }
            Console.Write("Continue (y/n)");
            command = Console.ReadLine();
        }
    }
}