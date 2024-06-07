using System;
using System.Formats.Asn1;

class Program
{
    static void Main(string[] args)
    {
        Assignment a1 = new Assignment("Samuel Bennett", "Multiplication");

        string summary = a1.GetSummary();

        Console.WriteLine(summary);


        Math m1 = new Math("Roberto Rodriguez", "Fractions", "7.3", "8-19");

        summary = m1.GetSummary();
        string homeworkList = m1.GetHomeworkList();

        Console.WriteLine($"{summary}\n{homeworkList}");

        Writing w1 = new Writing("Mary Waters", "European History", "The Causes of World War II");

        summary = w1.GetSummary();
        string writingInfo = w1.GetWritingInformation();

        Console.WriteLine($"{summary}\n{writingInfo}");
    }
}