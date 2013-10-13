using System;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Controls;
using System.IO;
using System.Windows.Media.Imaging;
using System.Data.Linq;

namespace Innlevering01
{
    class ImageHandler
    {
        ImageNode[] images;
        private DatabaseHandler dbh;
        // Gets images from the GFX folder for now
        // Should be replaced by a database, shouldn't be too hard
        public ImageHandler()
        {
            dbh = new DatabaseHandler();
        }

        public ImageNode[] GetListBoxItemImages()
        {
            return images;
        }

        public void LoadImages()
        {
            int count = dbh.TileTable.Count();

            if (count > 0)
            {
                images = new ImageNode[count];
                DataClassesDataContext db = new DataClassesDataContext();
                var getAllQuery = from img in db.tiles
                    select img;

                int counter = 0;
                foreach (tile t in getAllQuery)
                {
                    byte[] buffer = t.image.ToArray();
                    MemoryStream stream = new MemoryStream(buffer);
                    BitmapImage hng = new BitmapImage();
                    hng.BeginInit();
                    hng.StreamSource = stream;
                    hng.EndInit();

                    Image img = new Image {Source = hng};

                    images[counter] = new ImageNode(img, t.tileName);
                    counter++;
                }
            }

            else
            {
                // Get dynamic project path and enters it into the database for a "default" collection of tiles
                string path = Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory());
                DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(path), "GFX\\"));

                FileInfo[] fileInfo = dirInfo.GetFiles("*.png");

                images = new ImageNode[fileInfo.Length];

                if (fileInfo.Length <= 0) return;

                // Loading images into container class
                for (int i = 0; i < fileInfo.Length; i++)
                {
                    images[i] = new ImageNode(new Image { Source = new BitmapImage(new Uri(fileInfo[i].FullName))},
                        fileInfo[i].Name, fileInfo[i].FullName);

                    // Also saves the default tiles to the database
                    StoreImage(images[i]);
                }
            }
        }

        public void StoreImage( ImageNode image)
        {
            byte[] imageData;
            String filename = image.Filepath;

           // Read the file into a byte array
            using(FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                imageData = new Byte[fs.Length];
                fs.Read( imageData, 0, (int)fs.Length );
            }

            tile til = new tile
            {
                collisionMap = "000000000",
                image = imageData,
                tileName = image.Name
            };

            dbh.TileTable.InsertOnSubmit(til);

            try
            {
                dbh.DataContxt.SubmitChanges();
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
