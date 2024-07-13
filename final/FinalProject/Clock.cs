class Clock
{
    private int _currentTime = 1;
    private int _currentRound = 1;

    public int GetTime()
    {
        return _currentTime;
    }
    public int GetRound()
    {
        return _currentRound;
    }

    public void AdvanceTime()
    {
        if (_currentTime < 12)
        {
            _currentTime++;
        }
        else
        {
            _currentTime = 1;
            _currentRound++;
        }
    }
}