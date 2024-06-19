using System;

class Program
{
    static void Main(string[] args)
    {
        List<Shape> shapes = new List<Shape>();

        shapes.Add(new Square("purple", 4));

        shapes.Add(new Rectangle("blue", 2, 3));

        shapes.Add(new Circle("green", 2));

        foreach (Shape shape in shapes)
        {
            Console.WriteLine(shape.GetArea());
        }
    }
}