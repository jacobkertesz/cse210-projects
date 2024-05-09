public class Resume
        {
            public string _name;
            public List<Job> _jobsList = new List<Job>();

            public void Display()
            {
                Console.WriteLine($"{_name}");
                foreach (Job i in _jobsList)
                {
                    i.Display();
                }
            }
        }