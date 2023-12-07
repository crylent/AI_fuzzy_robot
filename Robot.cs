namespace AI_robot;

public class Robot
{
    private Field _field;
    
    private int _x;
    private int _y;
    
    public Robot(Field field)
    {
        _field = field;
    }

    public void SetPosition(int x, int y)
    {
        _x = x;
        _y = y;
    }
}