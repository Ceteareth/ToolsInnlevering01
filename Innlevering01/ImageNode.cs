using System.Windows.Controls;
using System.Windows.Media;

namespace Innlevering01
{
    class ImageNode : TreeViewItem
    {
        public Image Image { get; set; }
        public string Filename { get; set; }
        public string Filepath { get; private set; }
        public int[] CollisionMap { get; set; }

        public ImageNode(Image image, string filename, string filepath)
        {
            Filepath = filepath;
            Image = image;
            Name = filename.Replace(".png", "");
            Header = filename.Replace(".png", "");
            CollisionMap = new int[]
            {
                0,0,0,
                0,0,0,
                0,0,0
            };
        }

        public ImageNode(Image image, string filename)
        {
            Image = image;
            Name = filename.Replace(".png", "");
            Header = filename.Replace(".png", "");
            Filepath = "Located in database";
        }
    }
}
