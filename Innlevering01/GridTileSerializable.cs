namespace Innlevering01
{
    // This class saves all relevant information of a grid tile for XML serialization.
    public class GridTileSerializable
    {
        public int Rotation;
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public int[][] CollisionMap { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public GridTileSerializable(int rotation, int id, byte[] image, int[][] collisionMap)
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

        // Must have a parameter free constructor to be able to serialize.
        public GridTileSerializable()
        {
            
        }
    }
}
