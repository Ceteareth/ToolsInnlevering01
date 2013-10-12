using System;
using System.Linq;
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
            // Get dynamic project path
            string path = Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory());
            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(path),"GFX\\"));
            Console.WriteLine("Directory: " + dirInfo);
            
            FileInfo[] fileInfo = dirInfo.GetFiles("*.png");

            images = new ImageNode[fileInfo.Length];

            Console.WriteLine("Length: " + fileInfo.Length);

            if (fileInfo.Length <= 0) return;
            // Loading images into container class
            for (int i = 0; i < fileInfo.Length; i++)
                images[i] = new ImageNode(new BitmapImage(new Uri(fileInfo[i].FullName)), fileInfo[i].Name, fileInfo[i].FullName);

            dbh = new DatabaseHandler();

        }

        public ImageNode[] getListBoxItemImages()
        {
            return images;
        }

        public void StoreImage( ImageNode image )
        {
            byte[] imageData;
            String filename = images[0].Filepath;

           // Read the file into a byte array
            using(FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                imageData = new Byte[fs.Length];
                fs.Read( imageData, 0, (int)fs.Length );
            }

            int count = dbh.TileTable.Count();

            tile til = new tile
            {
                collisionMap = "000000000",
                image = imageData,
                tileName = "Test"
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
