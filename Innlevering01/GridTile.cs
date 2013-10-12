using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Innlevering01
{
    class GridTile : TextBox
    {
        public int Rotation;
        public int Id { get; set; }
        public Image image { get; set; }

        // Can now add more stuff we need to, rotation and such.
        public GridTile(int _rotation, int _id, Image _image)
        {
            Rotation = _rotation;
            Id = _id;
            image = _image;
        }      
    }
}
