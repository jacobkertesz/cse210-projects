using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        Entry entry = new Entry();

        string selection;

        do 
        {
            journal.DisplayMenu();
            Console.Write("");
            selection = Console.ReadLine();

            if (selection == "0") //quit
            {
                Console.WriteLine("\nQuitting");
            } 
            else if (selection == "1") //load journal
            {
                Console.Write("\nEnter Filename ");
                journal._filename = Console.ReadLine(); // using the full filename
                journal._entriesArray = System.IO.File.ReadAllLines(journal._filename);
                journal._entries.Clear();

                foreach (string line in journal._entriesArray)
                {
                    journal._entries.Add($"{line}");
                }
                Console.WriteLine("\nJournal Loaded");
            }
            else if (selection == "2") //show entries
            {
                Console.WriteLine("\nShowing Entries");
                foreach (string i in journal._entries)
                {
                    string[] parts = i.Split("*");
                    Console.WriteLine($"\n{parts[0]}\n{parts[1]}\n{parts[2]}");
                }
            }
            else if (selection == "3") //new entry
            {
                Console.Write("\nDo you want a prompt? (y/n)");
                string usePrompt = Console.ReadLine();
                if (usePrompt == "y")
                {
                    entry._prompt = entry.PickPrompt();
                    entry._text = entry.WriteNewEntry(true);
                }
                else 
                {
                    entry._prompt = "";
                    entry._text = entry.WriteNewEntry(false);
                }
                
                journal._entries.Add($"{entry._date.ToShortDateString()}*{entry._prompt}*{entry._text}");
            }
            else if (selection == "4") //save journal
            {
                Console.Write("\nEnter Filename ");
                journal._filename = Console.ReadLine(); // using the full filename

                using (StreamWriter savedJournal = new StreamWriter(journal._filename))
                {
                    foreach (string i in journal._entries)
                    {
                        savedJournal.WriteLine($"{i}");
                    }
                }
                Console.WriteLine("\nSaved Journal");
            }
            else 
            {
                Console.WriteLine("\nNo Such Command\n");
            }
        }
        while (selection != "0");
    }
}