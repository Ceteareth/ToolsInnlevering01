using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Innlevering01
{
    public class GridTileSerializable
    {
        public int Rotation;
        public int Id { get; set; }
        public byte[] image { get; set; }
        public int[][] collisionMap { get; set; }
        public int row { get; set; }
        public int column { get; set; }

        // Can now add more stuff we need to, rotation and such.
        public GridTileSerializable(int _rotation, int _id, byte[] _image, int[][] _collisionMap)
        {
            Rotation = _rotation;
            Id = _id;
            image = _image;

            collisionMap = new int[3][];

            for (int i = 0; i < collisionMap.Length; i++)
            {
                collisionMap[i] = new int[3];
            }

            collisionMap = _collisionMap;
        }

        public GridTileSerializable()
        {
            
        }
    }
}
