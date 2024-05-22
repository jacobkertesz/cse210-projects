using System;

class Program
{
    static void Main(string[] args)
    {
        Fraction f1 = new Fraction();
        Fraction f2 = new Fraction(6);
        Fraction f3 = new Fraction(6, 7);

        f1.SetTop(1);
        f1.SetBottom(1);

        string fraction = f1.GetFractionString();
        double number = f1.GetDecimalValue();

        Console.WriteLine($"{fraction}");
        Console.WriteLine($"{number}");


        f1.SetTop(5);
        f1.SetBottom(1);

        fraction = f1.GetFractionString();
        number = f1.GetDecimalValue();

        Console.WriteLine($"{fraction}");
        Console.WriteLine($"{number}");


        f1.SetTop(3);
        f1.SetBottom(4);

        fraction = f1.GetFractionString();
        number = f1.GetDecimalValue();

        Console.WriteLine($"{fraction}");
        Console.WriteLine($"{number}");


        f1.SetTop(1);
        f1.SetBottom(3);

        fraction = f1.GetFractionString();
        number = f1.GetDecimalValue();

        Console.WriteLine($"{fraction}");
        Console.WriteLine($"{number}");
    }
}