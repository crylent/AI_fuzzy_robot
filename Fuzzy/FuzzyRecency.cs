using System.Collections.Generic;

namespace AI_robot.Fuzzy;

public class FuzzyRecency
{
    private const int Length = 10;
    private readonly List<(int x, int y)> _history = new(Length);

    private static readonly FuzzyNumber LongAgo = new(float.NegativeInfinity, float.NegativeInfinity, 0, Length);
    private static readonly FuzzyNumber Recent = new(0, Length, float.PositiveInfinity, float.PositiveInfinity);

    public void AddToHistory(int x, int y)
    {
        if (_history.Count == Length) _history.RemoveAt(0);
        _history.Add((x, y));
    } 

    public float GetRecency(int x, int y)
    {
        var index = _history.IndexOf((x, y));
        return Recent.GetProbability(index + 1);
    }

    public void ClearHistory() => _history.Clear();
}