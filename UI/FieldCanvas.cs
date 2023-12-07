using System.Windows.Controls;

namespace AI_robot.UI;

public class FieldCanvas: Canvas
{
    private const int FieldWidth = 10;
    private const int FieldHeight = 6;

    private readonly Field _field = new(FieldWidth, FieldHeight);

    public FieldCanvas()
    {
        //Background = Brushes.White;
        for (var y = 0; y < FieldHeight; y++)
        {
            for (var x = 0; x < FieldWidth; x++)
            {
                var cell = new FieldCell(_field, x, y);
                Children.Add(cell);
                SetLeft(cell, x * FieldCell.CellSize);
                SetTop(cell, y * FieldCell.CellSize);
            }
        }
    }
}