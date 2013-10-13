using System;
using System.Linq;
using System.Windows.Controls;
using System.IO;
using System.Windows.Media.Imaging;

namespace Innlevering01
{
    public class ImageHandler
    {
        ImageNode[] images;
        private readonly DatabaseHandler dbh;

        public ImageHandler()
        {
            dbh = new DatabaseHandler();
        }

        public ImageNode[] GetListBoxItemImages()
        {
            return images;
        }

        // Checks if there's any database present with images, if not, it loads up some default images and saves
        // those to the database.
        public void LoadImages()
        {
            // Checks if there's anything in the tile database.
            int count = dbh.TileTable.Count();

            // If there is, get all of it and populate the list with it.
            if (count > 0)
            {
                images = new ImageNode[count];
                DataClassesDataContext db = new DataClassesDataContext();

                var getAllQuery = from img in db.tiles
                    select img;

                // Runs through all queries, creates new ImageNodes and adds them to the internal array.
                int counter = 0;
                foreach (tile t in getAllQuery)
                {
                    byte[] buffer = t.image.ToArray();
                    MemoryStream stream = new MemoryStream(buffer);
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();

                    Image img = new Image {Source = bitmapImage};

                    Console.WriteLine(t.tileName);
                    images[counter] = new ImageNode(img, t.tileName);
                    counter++;
                }
            }

                // if not, gets all the images from the included GFX folder and enters those into the database.
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

        // Saves an image to the database.
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

            // Creates a new database entry.
            tile til = new tile
            {
                collisionMap = image.CollisionMap.ToString(),
                image = imageData,
                tileName = image.Name
            };

            dbh.TileTable.InsertOnSubmit(til);

            // Tries to submit, if it fails, throws an exception.
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
