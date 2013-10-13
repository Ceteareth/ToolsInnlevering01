using System.Windows.Controls;

namespace Innlevering01
{
    // Inherits from TextBox so we can populate a grid with them, but also contain some extra information we need.
    // Otherwise it's very much alike ImageNode.
    public class GridTile : TextBox
    {
        public int Rotation;
        public int Id { get; set; }
        public Image Image { get; set; }
        public int[][] CollisionMap { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }

        public GridTile(int rotation, int id, Image image, int[][] collisionMap)
        {
            Rotation = rotation;
            Id = id;
            Image = image;

            CollisionMap = new int[3][];

            for (int i = 0; i < this.CollisionMap.Length; i++)
            {
                CollisionMap[i] = new int[3];
            }

            CollisionMap = collisionMap;
        }      
    }
}
