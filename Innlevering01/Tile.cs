using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innlevering01
{
    class Tile
    {
        public string tileName
        {
            get { return tileName; }
            internal set { tileName = value; } 
        }

        public imagenode imagenode
        {
            get { return imagenode; }
            internal set { imagenode = value; }
        }

        private char[,] collisionMap = new char[3,3];

        private void setCollisionMap(char[,] value)
        {
            collisionMap = value;
        }

        public char[,] getCollisionMap()
        {
            return collisionMap;
        }

        public void Tile(ImageNode _imageNode,string _tileName, char[,] _collisionMap)
        {
            this.imageNode = _imageNode;
            this.tileName = _tileName;
            setCollisionMap(_collisionMap);
        }
        
    }
}
