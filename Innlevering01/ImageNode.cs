using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Innlevering01
{
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
