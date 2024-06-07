public class Writing : Assignment
{
    private string _title;

    public Writing(string newN, string newTo, string newT) : base(newN, newTo)
    {
        _title = newT;
    }

    public string GetWritingInformation()
    {
        return $"{_title} by {_studentName}";
    }
}