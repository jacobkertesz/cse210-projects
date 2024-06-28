//Added an additional class in charge of converting csv data into Goals and vice versa.
//Added a starting goal.
//Added the date a goal was most recently added to.

using System;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        int UpdateScore(List<Goal> goals)
        {
            int score = 0;
            foreach (Goal goal in goals)
                {
                    score += goal.GivePoints();
                }
            return score;
        }

        int score;
        string userResponse1 = "";
        string userResponse2 = "";

        List<Goal> goals = new List<Goal>();
        DataConverter dc = new DataConverter();

        goals = dc.FileToGoal();
        score = UpdateScore(goals);

        while (userResponse1 != "4")
        {
            Console.Clear();
            Console.WriteLine($"Score: [{score}]");
            Console.WriteLine("MENU: \n1.Create Goal \n2.Record Progress \n3.Show Goals \n4.Quit \n");
            userResponse1 = Console.ReadLine();

            if (userResponse1 == "1")//create
            {
                Console.Clear();
                Console.WriteLine("1.Create Simple Goal \n2.Create Ongoing Goal \n3.Create Checklist Goal \n");
                userResponse2 = Console.ReadLine();

                if (userResponse2 == "1") //Simple
                {
                    Console.Write("Name: ");
                    string n = Console.ReadLine();
                    Simple simple = new Simple(n, 0, "");
                    goals.Add(simple);
                }
                else if (userResponse2 == "2") //Ongoing
                {
                    Console.Write("Name: ");
                    string n = Console.ReadLine();
                    Ongoing ongoing = new Ongoing(n, 0, "");
                    goals.Add(ongoing);
                }
                else if (userResponse2 == "3") //Checklist
                {
                    Console.Write("Name: ");
                    string n = Console.ReadLine();
                    Console.Write("Max Complete: ");
                    int m = Int32.Parse(Console.ReadLine());
                    Checklist checklist = new Checklist(n, 0, "", m);
                    goals.Add(checklist);
                }
            }
            else if (userResponse1 == "2")//record
            {
                int chosenI;
                int number = 1;
                Console.Clear();
                foreach (Goal goal in goals)
                {
                    Console.Write($"{number}. ");
                    goal.ShowGoal();
                    number++;
                }
                Console.WriteLine();
                chosenI = Int32.Parse(Console.ReadLine()) - 1;
                goals[chosenI].RecordComplete();

                score = UpdateScore(goals);
            }
            else if (userResponse1 == "3")// show goal
            {
                Console.Clear();
                foreach (Goal goal in goals)
                {
                    goal.ShowGoal();
                }
                Console.Write("\nBack");
                Console.ReadLine();
            }
            else if (userResponse1 == "4")//quit
            {
                dc.GoalToFile(goals);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("No Such Command");
                Thread.Sleep(1000);
            }
        }
    }
}