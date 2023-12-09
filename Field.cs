using System.Collections.Generic;

namespace AI_robot;

public class Field
{
    public readonly int X;
    public readonly int Y;
    
    private readonly bool[][] _obstacles;
    public readonly Robot Robot;

    public Field(int x, int y)
    {
        X = x;
        Y = y;
        _obstacles = new bool[y][];
        Robot = new Robot(this);
        for (var row = 0; row < _obstacles.Length; row++)
        {
            _obstacles[row] = new bool[x];
        }
    }

    public void SetObstacle(int x, int y, bool obstacle)
    {
        _obstacles[y][x] = obstacle;
    }

    public IEnumerable<(int x, int y)> GetObstacles()
    {
        for (var y = 0; y < Y; y++)
        {
            for (var x = 0; x < X; x++)
            {
                if (_obstacles[y][x]) yield return (x, y);
            }
        }
        
        // add borders as walls
        for (var x = 0; x < X; x++)
        {
            yield return (x, -1);
            yield return (x, Y);
        }
        for (var y = 0; y < Y; y++)
        {
            yield return (-1, y);
            yield return (X, y);
        }
    }
}