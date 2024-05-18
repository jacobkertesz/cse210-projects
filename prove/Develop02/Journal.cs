public class Journal
{
    //attributes
    public string _filename;
    public string[] _entriesArray;
    public List<string> _entries= new List<string> {};
    //behaviors
    public void DisplayMenu()
    {
        Console.WriteLine("\n0. Quit");
        Console.WriteLine("1. Load Journal");
        Console.WriteLine("2. Show Entries");
        Console.WriteLine("3. New Entry");
        Console.WriteLine("4. Save Journal");
    }
}