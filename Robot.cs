using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AI_robot.Fuzzy;

namespace AI_robot;

public class Robot
{
    private readonly Field _field;
    private readonly FuzzyRecent _recentCells = new();
    
    private int _x;
    private int _y;
    
    private enum Target
    {
        TopEdge, BottomEdge, LeftEdge, RightEdge
    }

    private Target _target = Target.RightEdge;
    
    public Robot(Field field)
    {
        _field = field;
    }

    public void SetPosition(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public delegate void OnMoveListener(int x, int y);
    private readonly List<OnMoveListener> _onMove = new();

    public void AddOnMoveListener(OnMoveListener listener)
    {
        _onMove.Add(listener);
    }

    private void OnMove(int x, int y)
    {
        foreach (var onMoveListener in _onMove)
        {
            onMoveListener(x, y);
        }
        _recentCells.AddToHistory(x, y);
    }
    
    public void MakeMove()
    {
        const float minObs = 1f;
        float downObstruction = minObs, upObstruction = minObs, leftObstruction = minObs, rightObstruction = minObs;
        float downRecency = _recentCells.GetRecency(_x, _y + 1),
            upRecency = _recentCells.GetRecency(_x, _y - 1),
            leftRecency = _recentCells.GetRecency(_x - 1, _y),
            rightRecency = _recentCells.GetRecency(_x + 1, _y);
        foreach (var (dist, dir) in GetObstaclesDistanceAndDirection())
        {
            var closeness = float.Pow(FuzzyDistance.GetCloseness(dist), 3);
            if (closeness > 0.999) closeness *= 10000; // never move right into the wall
            downObstruction += closeness * FuzzyDirection.GetDownProbability(dir);
            upObstruction += closeness * FuzzyDirection.GetUpProbability(dir);
            leftObstruction += closeness * FuzzyDirection.GetLeftProbability(dir);
            rightObstruction += closeness * FuzzyDirection.GetRightProbability(dir);
        }

        float downDesire = GetDesire(Target.BottomEdge, Target.TopEdge, downObstruction, downRecency),
            upDesire = GetDesire(Target.TopEdge, Target.BottomEdge, upObstruction, upRecency),
            leftDesire = GetDesire(Target.LeftEdge, Target.RightEdge, leftObstruction, leftRecency),
            rightDesire = GetDesire(Target.RightEdge, Target.LeftEdge, rightObstruction, rightRecency);
        Debug.Print($"Down: {downDesire}\tUp: {upDesire}\tLeft: {leftDesire}\tRight: {rightDesire}");
        var max = new[] { downDesire, upDesire, leftDesire, rightDesire }.Max();
        if (downDesire.Equals(max)) _y += 1;
        else if (leftDesire.Equals(max)) _x -= 1;
        else if (rightDesire.Equals(max)) _x += 1;
        else _y -= 1;
        OnMove(_x, _y);

        // check if reached the target
        switch (_target)
        {
            case Target.TopEdge:
                if (_y == 0) TossTarget();
                break;
            case Target.BottomEdge:
                if (_y == _field.Y - 1) TossTarget();
                break;
            case Target.LeftEdge:
                if (_x == 0) TossTarget();
                break;
            case Target.RightEdge:
                if (_x == _field.X - 1) TossTarget();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private float GetDesire(Target targetEdge, Target nonTargetEdge, float obstruction, float recency)
    {
        float k;
        if (_target == targetEdge)
        {
            k = 1.7f;
        }
        else if (_target == nonTargetEdge)
        {
            k = 0.8f;
        }
        else
        {
            k = 1f;
        }
        return k / obstruction / (0.1f + recency);
    }

    private void TossTarget()
    {
        _target = Random.Shared.Next(0, 4) switch
        {
            0 => Target.TopEdge,
            1 => Target.BottomEdge,
            2 => Target.LeftEdge,
            3 => Target.RightEdge,
            _ => throw new ArgumentOutOfRangeException()
        };
        _recentCells.ClearHistory();
        Debug.Print($"Target: {_target}");
    }

    private IEnumerable<(float dist, float dir)> GetObstaclesDistanceAndDirection()
    {
        foreach (var (x, y) in _field.GetObstacles())
        {
            var dist = float.Sqrt(float.Pow(x - _x, 2) + float.Pow(y - _y, 2));
            var dir = float.Atan2(x - _x, y - _y);
            yield return (dist, dir);
        }
    }
}