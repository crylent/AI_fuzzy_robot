namespace AI_robot.Fuzzy;

public static class FuzzyDistance
{
    private static readonly FuzzyNumber Close = new(0, 0, 1, 5);
    private static readonly FuzzyNumber Far = new(1, 5, float.PositiveInfinity, float.PositiveInfinity);

    public static float GetCloseness(float dist) => Close.GetProbability(dist);
}