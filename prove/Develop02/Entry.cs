using Microsoft.VisualBasic;

public class Entry
    {
        //attributes
        public DateTime _date = DateTime.Now;
        public string _text;
        List<string> _prompts = new List<string> 
        {
            "What was the most impactful thing that happened today? ",
            "What lesson did you learn today? ",
            "What are you currently preparing for? ",
            "What was the greatest challenge you faced today? ",
            "What superpower would have helped you today? "
        };
        Random random = new Random();
        public string _prompt;

        //behaviors
        public string PickPrompt()
        {
            int i = random.Next(_prompts.Count);
            _prompt = _prompts[i];
            return _prompt;
        }
        public string WriteNewEntry(bool usePrompt)
        {
            if (usePrompt == true)
            {
                Console.Write($"{_prompt}");
            }
            else 
            {
                Console.Write("Entry: ");
            }
            _text = Console.ReadLine();
            return _text;
        }
    }