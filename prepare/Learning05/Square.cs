public class Square : Shape
{
    private double _side;
    public Square(string c, double s) : base(c)
    {
        _side = s;
    }

    public override double GetArea()
    {
        return _side * _side;
    }
}