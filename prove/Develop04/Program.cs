// Features added:
// The program keeps track of how many activities are completed and the total time spent in activites.
using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        string userResponse;
        int activityCount = 0;
        int timeSpent = 0;
        do
        {
            Console.Clear();
            Console.WriteLine($"Activity Menu: \n1.Breathing \n2.Reflection \n3.Listing \n4.Quit \n\nActivities Completed: {activityCount}    Time Spent: {timeSpent}");
            userResponse = Console.ReadLine();

        if (userResponse == "1")
        {
            Breathing b1 = new Breathing("Breathing Activity", "This activity will help you relax by walking your through breathing in and out slowly. Clear your mind and focus on your breathing.");
            b1.StartInfo();
            b1.AskForTime();
            b1.RunBreathing();
            activityCount ++;
            timeSpent += b1.GetTime();
        }
        else if (userResponse == "2")
        {
            Reflection r1 = new Reflection("Reflection Activity", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.");
            r1.StartInfo();
            r1.AskForTime();
            r1.RunReflection();
            activityCount ++;
            timeSpent += r1.GetTime();
        }
        else if (userResponse == "3")
        {
            Listing l1 = new Listing("Listing Activity", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");
            l1.StartInfo();
            l1.AskForTime();
            l1.RunListing();
            activityCount ++;
            timeSpent += l1.GetTime();
        }
        else if (userResponse == "4")
        {
            Console.Clear();
            Console.WriteLine("Good Bye");
        }
        else 
        {
            Console.Clear();
            Console.WriteLine("No Such Command");
            Thread.Sleep(1000);
        }
        }
        while (userResponse != "4");
    }
}