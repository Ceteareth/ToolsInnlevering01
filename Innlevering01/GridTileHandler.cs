using System;
using System.IO;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;

namespace Innlevering01
{
    class GridTileHandler
    {
        public GridTileHandler()
        {
            
        }

        // Serializes a grid tile and returns it as such.
        public GridTileSerializable SerializeGridTile(GridTile tile)
        {
            byte[] imageData;

            if (tile.Name == "")
                return new GridTileSerializable(tile.Rotation, tile.Id, null, null);

            // Read the file into a byte array, effectively converting the image in grid tile to a byte array, which we can 
            // save in a XML file.
            using (MemoryStream fs = new MemoryStream())
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)tile.Image.Source));
                encoder.Save(fs);
                imageData = fs.GetBuffer();
            }

            return new GridTileSerializable(tile.Rotation, tile.Id, imageData, tile.CollisionMap)
            {
                Row = tile.Row,
                Column = tile.Column,
                CollisionMap = tile.CollisionMap
            };
        }

        // Deserializes a serialized grid tile, making it fit for use in our editor grid.
        public GridTile DeserializeGridTile(GridTileSerializable tile)
        {
            if (tile.Image == null) return null;

            try
            {
                // Effectively creating an image out of the byte array in the serialized grid tile.
                Image myImage = new Image();
                using (MemoryStream stream = new MemoryStream(tile.Image))
                {
                    PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                    BitmapSource bitmapSource = decoder.Frames[0];
                    myImage.Source = bitmapSource;
                }

                return new GridTile(tile.Rotation, tile.Id, myImage, tile.CollisionMap)
                {
                    Row = tile.Row,
                    Column = tile.Column,
                    CollisionMap = tile.CollisionMap
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
