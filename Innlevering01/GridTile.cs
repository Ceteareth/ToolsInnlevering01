using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Innlevering01
{
    class GridTile : Grid
    {
        public int Rows { get; internal set; }
        public int Columns { get; internal set; }
        public Brush Content { get; internal set; }

        // Can now add more stuff we need to, rotation and such.
        /*public GridTile(int row, int column, Brush content)
        {
            Rows = row;
            Columns = column;
            Content = content;
        }  */      
    }
}
