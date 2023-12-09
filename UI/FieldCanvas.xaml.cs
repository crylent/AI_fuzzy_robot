using System.Windows.Controls;

namespace AI_robot.UI;

public partial class FieldCanvas
{
    private const int FieldWidth = 10;
    private const int FieldHeight = 6;

    public readonly Field Field = new(FieldWidth, FieldHeight);

    private readonly FieldCell[][] _cells = new FieldCell[FieldHeight][];

    public FieldCanvas()
    {
        InitializeComponent();
        
        // grid definitions
        for (var y = 0; y < FieldHeight; y++)
        {
            RowDefinitions.Add(new RowDefinition());
        }
        for (var x = 0; x < FieldWidth; x++)
        {
            ColumnDefinitions.Add(new ColumnDefinition());
        }

        for (var y = 0; y < FieldHeight; y++)
        {
            _cells[y] = new FieldCell[FieldWidth];
            for (var x = 0; x < FieldWidth; x++)
            {
                var cell = new FieldCell(Field, x, y);
                Children.Add(cell);
                SetRow(cell, y);
                SetColumn(cell, x);
                //SetLeft(cell, x * FieldCell.CellSize);
                //SetTop(cell, y * FieldCell.CellSize);
                _cells[y][x] = cell;
            }
        }
        
        Field.Robot.AddOnMoveListener((x, y) => _cells[y][x].PutRobotHere());
    }
}