using System;
using System.Net.WebSockets;

/*
Added functions:
    Scriptures and references are inputed by the user.
    When selecting words to hide, the program only selects words NOT already hidden.
*/

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();

        //setup scripture and reference
        Scripture scripture = new Scripture();
        Reference reference = new Reference();

        Console.Write("Enter Scripture: ");
        string userScri = Console.ReadLine();
        scripture.SetScripture(userScri);

        Console.Write("\nEnter Reference: ");
        string userRef = Console.ReadLine();
        reference.SetReference(userRef);

        //setup wordList
        List<Word> wordList = new List<Word>();

        string s = scripture.GetScripture();
        string[] sa = s.Split(" ");

        foreach (string wordString in sa)
        {
            Word newWord = new Word();
            newWord.SetWord(wordString);

            wordList.Add(newWord);
        } 

        //setup visibleWords
        int totalWordCount = wordList.Count;

        scripture.CreateList(totalWordCount);

        List<int> visibleIndexes = scripture.GetVisibleWords();
        int visibleIndexesCount = scripture.GetVisibleWords().Count;

        do
        {
            //display scripture and reference
            Console.Clear();

            string r = reference.GetReference();
            Console.WriteLine($"{r}:");

            foreach (Word wW in wordList)
            {
                string wS = wW.GetWord();

                Console.Write($"{wS} ");
            }
            Console.Write("\n");
            string command = Console.ReadLine();

            if (command == "quit")
            {
                visibleIndexesCount = 0;
            }
            else
            {
                //hide words
                visibleIndexesCount = scripture.GetVisibleWords().Count;

                if (visibleIndexesCount >= 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int randomIndex = scripture.SelectRandomWord();
                        wordList[visibleIndexes[randomIndex]].HideWord();
                        scripture.RemoveVisibleWords(randomIndex);
                    }
                }
                else if (visibleIndexesCount > 0)
                {
                    for (int i = 0; i < visibleIndexesCount; i++)
                    {
                        int randomIndex = scripture.SelectRandomWord();
                        wordList[visibleIndexes[randomIndex]].HideWord();
                        scripture.RemoveVisibleWords(randomIndex);
                    }
                }
            }
            }
        while (visibleIndexesCount > 0);
    }
}