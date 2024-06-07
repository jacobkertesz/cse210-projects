public class Math : Assignment
{
    private string _textbookSection;
    private string _problems;

    public Math(string newN, string newTo, string newTe, string newP) : base(newN, newTo)
    {
        _textbookSection = newTe;
        _problems = newP;
    }
    public string GetHomeworkList()
    {
        return $"Section {_textbookSection} Problems {_problems}";
    }
}