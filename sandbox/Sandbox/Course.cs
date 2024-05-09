using System.ComponentModel.DataAnnotations;

class Course 
{
    public string _courseCode;
    public string _className;
    public int _numberOfCredits;
    public string _color;

    // method
    public void Display()
    {
        Console.WriteLine($"{_courseCode} {_className} {_numberOfCredits} {_color}");
    } 
}

