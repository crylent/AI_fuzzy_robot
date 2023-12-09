using System.Windows;
using System.Windows.Media;

namespace AI_robot.UI;

public partial class FieldCell
{
    public const float CellSize = 50;
    private readonly Brush _empty = Brushes.White;
    private readonly Brush _obstacle = Brushes.Black;

    private readonly Field _field;
    private readonly int _x;
    private readonly int _y;
    private bool _isObstacle;
    
    public enum OnClickAction
    {
        None, SetObstacle, PutRobot
    }

    private static OnClickAction _onClickAction = OnClickAction.None;
    private static FieldCell _currentCell = null!;

    public FieldCell(Field field, int x, int y)
    {
        _field = field;
        _x = x;
        _y = y;
        //Width = 50;
        //Height = 50;
        Background = _empty;
        
        InitializeComponent();

        if (_currentCell == null!)
        {
            PutRobotHere();
        }
    }

    protected override void OnClick()
    {
        base.OnClick();

        switch (_onClickAction)
        {
            case OnClickAction.SetObstacle:
                _isObstacle = !_isObstacle;
                _field.SetObstacle(_x, _y, _isObstacle);
                Background = _isObstacle switch
                {
                    true => _obstacle,
                    false => _empty
                };
                break;
            case OnClickAction.PutRobot:
                PutRobotHere();
                break;
            case OnClickAction.None:
            default:
                break;
        }
    }

    public void PutRobotHere()
    {
        if (_currentCell != null!) _currentCell.RobotImage.Visibility = Visibility.Hidden; // remove from the last cell
        _currentCell = this;
        _field.Robot.SetPosition(_x, _y);
        RobotImage.Visibility = Visibility.Visible;
    }

    public static void SetOnClickAction(OnClickAction onClickAction)
    {
        _onClickAction = onClickAction;
    }
}