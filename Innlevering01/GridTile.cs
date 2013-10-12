using System.Windows.Shapes;

namespace Innlevering01
{
    class GridTile
    {
        public int Rows { get; internal set; }
        public int Columns { get; internal set; }
        public Rectangle Content { get; internal set; }

        public GridTile(int rows, int columns, Rectangle content)
        {
            Rows = rows;
            Columns = columns;
            Content = content;
        }        
    }
}
