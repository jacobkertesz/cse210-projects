using System;

class Program
{
    static void Main(string[] args)
    {
        Random randomGenerator = new Random();
        int magicNumber = randomGenerator.Next(1, 100);
        int guess = 0;

        do 
        {
            Console.Write("Guess the number. ");
            string userGuess = Console.ReadLine();
            guess = int.Parse(userGuess);

            if (guess > magicNumber)
            {
                Console.WriteLine("Lower");
            }
            else if (guess < magicNumber)
            {
                Console.WriteLine("Higher");
            }
            else 
            {
                Console.WriteLine($"{magicNumber} is correct.");
            }
        } while (guess != magicNumber);
    }
}