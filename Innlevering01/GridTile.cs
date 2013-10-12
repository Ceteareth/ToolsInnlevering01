using System.Windows.Shapes;

namespace Innlevering01
{
    class GridTile
    {
        int rows;
        int columns;
        Rectangle content;


        public int Rows
        {
            get { return rows; }
            internal set { rows = value; }
        }
        public int Columns
        {
            get { return columns; }
            internal set { columns = value; }
        }

        public Rectangle Content
        {
            get { return content; }
            internal set { content = value; }
        }

        public GridTile(int rows, int columns, Rectangle content)
        {
            this.rows = rows;
            this.columns = columns;
            this.content = content;
        }        
    }
}
