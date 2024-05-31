class Word
{
    //atributes
    private string _word;

    //methods
    public string GetWord()
    {
        return _word;
    }

    public void SetWord(string newWord)
    {
        _word = newWord;
    }

    public void HideWord()
    {
        string hiddenWord = "";
        foreach (char c in _word)
        {
            hiddenWord += "_";
        }

        SetWord(hiddenWord);
    }
}