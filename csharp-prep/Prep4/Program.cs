using System;
using System.Diagnostics.CodeAnalysis;

class Program
{
    static void Main(string[] args)
    {
        List<double> numbers = new List<double>();
        double newNumber;
        Console.WriteLine("Enter a list of numbers; type 0 when done.");

        do
        {
            Console.Write("Enter number: ");
            string userInput = Console.ReadLine();
            newNumber = double.Parse(userInput);

            numbers.Add(newNumber);

        } while (newNumber != 0);

        double sum = 0;
        double totalInList = numbers.Count - 1;
        double max = 0;

        foreach (int number in numbers)
        {
            sum += number;

            if (number > max)
            {
                max = number;
            }
        }

        double average = sum / totalInList;

        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {max}");
    }
}