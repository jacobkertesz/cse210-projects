using System;
using System.Globalization;
using System.Security.Cryptography;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();

        Clock clock = new Clock();
        
        List<Region> regions = new List<Region>{};
        for (int i = 0; i < 4; i++)
        {
            Region newRegion = new Region();
            newRegion.SetName($"R{i}");
            regions.Add(newRegion);
        }

        List<Creature> creatures = new List<Creature>{};
        Console.Write("Number of creatures: ");
        string userString = Console.ReadLine();
        int userInt = Int32.Parse(userString);

        for (int i = 0; i < userInt; i++)
        {
            Creature newC = new Creature();
            creatures.Add(newC);
            newC.SetRegion(regions);
        }

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
                Console.WriteLine($"{id}[{status} {aggro}] {rName}");
            }
        }
        
        foreach (Creature c in creatures)
        {
            c.RandomCreature();
            List<string> traits = new List<string>();
            List<int> strength = new List<int>();

            traits = c.GetTraits();
            strength = c.GetStrength();
            int neutral = c.GetNeutral();
            string attack = c.GetAttack();
            string activity = c.GetActivity();
            int aggression = c.GetAggression();

            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine($"{traits[i]} {strength[i]}");
            }
            Console.WriteLine($"\nTrace {neutral} \nAttack {attack} \nActivity {activity} \nAggression {aggression}\n");
        }
        
        Console.Write("Continue");
        Console.ReadLine();

        string command = "";


        while (command != "X")
        {
            int time = clock.GetTime();

            UpdateVisuals();
            Thread.Sleep(1000);

            foreach (Creature c in creatures)
            {
                if (c.GetStatus() != "dead")
                {
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
                            targets = c.ChooseTarget(targets);
                            if (targets.Count == 1)
                            {
                                c.PrepareAttack(targets[0], clock);
                                Thread.Sleep(1000);
                            }  
                        } 
                        //leave trace or move
                        int randomValue = random.Next(6) + 1;

                        if (randomValue > c.GetNeutral())
                        {
                           c.ChangeRegion(regions);
                        }   
                    }
                }
            }
            UpdateVisuals();
            Thread.Sleep(1000);
            Console.Write("Continue");
            command = Console.ReadLine();

            clock.AdvanceTime();
        }
    }
}