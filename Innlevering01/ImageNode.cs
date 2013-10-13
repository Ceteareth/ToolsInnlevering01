using System.Windows.Controls;

namespace Innlevering01
{
    // ImageNode inherits from TreeViewItem so that we easily can show it in a treeview.
    // Otherwise it's very much alike GridTile.
    public class ImageNode : TreeViewItem
    {
        public Image Image { get; set; }
        public string Filename { get; set; }
        public string Filepath { get; private set; }
        public int[][] CollisionMap { get; set; }

        public ImageNode(Image image, string filename, string filepath)
        {
            Filepath = filepath;
            Image = image;
            Name = filename.Replace(".png", "");
            Header = filename.Replace(".png", "");
            CollisionMap = new int[3][];

            for (int i = 0; i < CollisionMap.Length; i++)
            {
                CollisionMap[i] = new int[3];
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    CollisionMap[i][j] = 0;
                }
            }
        }

        // A separate type of ImageNode not needing a file path, for images in database.
        public ImageNode(Image image, string filename)
        {
            CollisionMap = new int[3][];

            for (int i = 0; i < CollisionMap.Length; i++)
            {
                CollisionMap[i] = new int[3];
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    CollisionMap[i][j] = 0;
                }
            }

            Image = image;
            Name = filename.Replace(".png", "");
            Header = filename.Replace(".png", "");
            Filepath = "Located in database";
        }
    }
}
