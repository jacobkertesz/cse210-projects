class DataConverter
{
    private string _filename = "C:\\Users\\jacob\\OneDrive\\Documents\\cse210\\cse210-projects-1\\prove\\Develop05\\goals.csv";
    private string[] _fileArray;
    
    public List<Goal> FileToGoal()
    {
        List<Goal> goals = new List<Goal>();
        _fileArray = System.IO.File.ReadAllLines(_filename);

        foreach (string goalF in _fileArray)
        {
            string[] parts = goalF.Split("*");

            string gName = parts[0];
            string gType = parts[1];
            int gComplete = Int32.Parse(parts[2]);

            if (gType == "simple")
            {
                string gDate = parts[3];
                Simple goalSimple = new Simple(gName, gComplete, gDate);
                goals.Add(goalSimple);
            }
            else if (gType == "ongoing")
            {
                string gDate = parts[3];
                Ongoing goalOngoing = new Ongoing(gName, gComplete, gDate);
                goals.Add(goalOngoing);
            }
            else if (gType == "checklist")
            {
                int gMax = Int32.Parse(parts[3]);
                string gDate = parts[4];
                Checklist goalChecklist = new Checklist(gName, gComplete, gDate, gMax);
                goals.Add(goalChecklist);
            }
        }
        return goals;
    }
    public void GoalToFile(List<Goal> goals)
    {
        using (StreamWriter savedGoal = new StreamWriter(_filename))
        {
            foreach (Goal goal in goals)
            {
                string line = goal.CopyData();
                savedGoal.WriteLine($"{line}");
            }
        }
    }
}