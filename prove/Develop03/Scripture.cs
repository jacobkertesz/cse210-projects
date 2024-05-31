class Scripture
{
    //attributes
    private string _scripture;

    private List<int> visibleWords = new List<int>();

    Random random = new Random();

    //methods
    public string GetScripture()
    {
        return _scripture;
    }

    public void SetScripture(string newScripture)
    {
        _scripture = newScripture;
    }

    public List<int> GetVisibleWords()
    {
        return visibleWords;
    }

    public void CreateList(int count)
    {
        visibleWords.Clear();
        int number = 0;

        for (int i = 0; i < count; i++ )
        {
            visibleWords.Add(number);
            number += 1;
        }
    }

    public void RemoveVisibleWords(int searchIndex)
    {
        visibleWords.RemoveAt(searchIndex);
    }

    public int SelectRandomWord()
    {
        int index = random.Next(visibleWords.Count); 
        return index;
    }
}