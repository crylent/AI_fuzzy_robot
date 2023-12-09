namespace AI_robot.Fuzzy;

public static class FuzzyDirection
{
    private const float Angle90 = float.Pi / 2;
    private static readonly FuzzyNumber Forward = new(-Angle90, 0, 0, Angle90);
    private static readonly FuzzyNumber Right = new(0, Angle90, Angle90, Angle90 * 2);
    private static readonly FuzzyNumber Backward = new(Angle90, Angle90 * 2, Angle90 * 2, Angle90 * 3);
    private static readonly FuzzyNumber Left = new(-Angle90 * 2, -Angle90, -Angle90, 0);

    public static float GetDownProbability(float angle) => Forward.GetProbability(angle);
    public static float GetRightProbability(float angle) => Right.GetProbability(angle);
    public static float GetUpProbability(float angle) => Backward.GetProbability(angle >= 0 ? angle : angle + Angle90 * 4);
    public static float GetLeftProbability(float angle) => Left.GetProbability(angle);
}