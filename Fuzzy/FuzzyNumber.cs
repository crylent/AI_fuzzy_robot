namespace AI_robot.Fuzzy;

public class FuzzyNumber
{
    public float A, B, C, D;

    public FuzzyNumber(float a, float b, float c, float d)
    {
        A = a;
        B = b;
        C = c;
        D = d;
    }

    public float GetProbability(float value)
    {
        if (value < A || value > D) return 0;
        if (value > B && value < C) return 1;
        if (value < B) return (value - A) / (B - A);
        //return (value - D) / (D - C);
        return (C - value) / (D - C) + 1;
    }
}