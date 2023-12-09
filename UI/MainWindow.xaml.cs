using System.Windows;
using System.Windows.Media;

namespace AI_robot.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly Brush _pressedBackground = Brushes.Goldenrod;
        private readonly Brush _defaultBackground;
        
        public MainWindow()
        {
            InitializeComponent();
            _defaultBackground = RobotButton.Background;
        }

        private void ResetButtons()
        {
            ObstacleButton.Background = _defaultBackground;
            RobotButton.Background = _defaultBackground;
        }

        private void OnClickObstacleButton(object sender, RoutedEventArgs e)
        {
            ResetButtons();
            ObstacleButton.Background = _pressedBackground;
            FieldCell.SetOnClickAction(FieldCell.OnClickAction.SetObstacle);
        }

        private void OnClickRobotButton(object sender, RoutedEventArgs e)
        {
            ResetButtons();
            RobotButton.Background = _pressedBackground;
            FieldCell.SetOnClickAction(FieldCell.OnClickAction.PutRobot);
        }

        private void OnClickPlayButton(object sender, RoutedEventArgs e)
        {
            ResetButtons();
            FieldCell.SetOnClickAction(FieldCell.OnClickAction.None);
            Field.Field.Robot.MakeMove();
        }
    }
}