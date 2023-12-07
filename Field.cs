namespace AI_robot;

public class Field
{
    private readonly int _x;
    private readonly int _y;
    
    private readonly bool[][] _obstacles;
    private readonly Robot _robot;

    public Field(int x, int y)
    {
        _x = x;
        _y = y;
        _obstacles = new bool[y][];
        _robot = new Robot(this);
        for (var row = 0; row < _obstacles.Length; row++)
        {
            _obstacles[row] = new bool[x];
        }
    }

    public void SetObstacle(int x, int y, bool obstacle)
    {
        _obstacles[y][x] = obstacle;
    }

    public bool GetObstacle(int x, int y) => _obstacles[y][x];

    public void SetRobotPosition(int x, int y) => _robot.SetPosition(x, y);
}