using System;

class Program
{
    static void Main(string[] args)
    {
        List<RoundShape> myList = new List<RoundShape>();
        myList.Add(new Circle(1.0));
        myList.Add(new Cylinder(1.0, 2.0));
        myList.Add(new Sphere(1.0));

        foreach (RoundShape shape in myList)
        {
            Console.WriteLine(shape.Area());
        }
    }
}