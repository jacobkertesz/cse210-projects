public class Circle : Shape
{
    private double _radius;
    public Circle(string c, double r) : base(c)
    {
        _radius = r;
    }
    public override double GetArea()
    {
        return Math.PI * _radius * _radius;
    }
}