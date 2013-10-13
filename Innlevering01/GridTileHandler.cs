using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using Image = System.Windows.Controls.Image;

namespace Innlevering01
{
    class GridTileHandler
    {
        public GridTileHandler()
        {
            
        }

        public GridTileSerializable SerializeGridTile(GridTile tile)
        {
            byte[] imageData;

            if (tile.Name == "")
                return new GridTileSerializable(tile.Rotation, tile.Id, null, null);

            // Read the file into a byte array
            using (MemoryStream fs = new MemoryStream())
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)tile.image.Source));
                encoder.Save(fs);
                imageData = fs.GetBuffer();
            }

            return new GridTileSerializable(tile.Rotation, tile.Id, imageData, tile.collisionMap)
            {
                row = tile.row,
                column = tile.column,
                collisionMap = tile.collisionMap
            };
        }

        public GridTile DeserializeGridTile(GridTileSerializable tile)
        {
            if (tile.image == null) return null;

            try
            {
                Image myImage = new Image();
                using (MemoryStream stream = new MemoryStream(tile.image))
                {
                    PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                    BitmapSource bitmapSource = decoder.Frames[0];
                    myImage.Source = bitmapSource;
                }

                return new GridTile(tile.Rotation, tile.Id, myImage, tile.collisionMap)
                {
                    row = tile.row,
                    column = tile.column,
                    collisionMap = tile.collisionMap
                    
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
