using System.Collections.Generic;

namespace AI_robot.Fuzzy;

public class FuzzyRecent
{
    private const int Length = 10;
    private readonly List<(int x, int y)> _history = new(Length);

    public void AddToHistory(int x, int y)
    {
        if (_history.Count == Length) _history.RemoveAt(0);
        _history.Add((x, y));
    } 

    public float GetRecency(int x, int y)
    {
        var index = _history.IndexOf((x, y));
        if (index < 0) return 0;
        return (float) (index + 1) / Length;
    }

    public void ClearHistory() => _history.Clear();
}