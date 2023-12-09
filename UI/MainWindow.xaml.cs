using System.Windows;

namespace AI_robot.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnClickObstacleButton(object sender, RoutedEventArgs e)
        {
            FieldCell.SetOnClickAction(FieldCell.OnClickAction.SetObstacle);
        }

        private void OnClickRobotButton(object sender, RoutedEventArgs e)
        {
            FieldCell.SetOnClickAction(FieldCell.OnClickAction.PutRobot);
        }

        private void OnClickPlayButton(object sender, RoutedEventArgs e)
        {
            FieldCell.SetOnClickAction(FieldCell.OnClickAction.None);
            Field.Field.Robot.MakeMove();
        }
    }
}