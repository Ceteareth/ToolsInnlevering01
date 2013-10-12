using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace Innlevering01
{
    class ImageHandler
    {
        ImageNode[] images;

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
                images[i] = new ImageNode(new BitmapImage(new Uri(fileInfo[i].FullName)), fileInfo[i].Name);

        }

        public ImageNode[] getListBoxItemImages()
        {
            return images;
        }
    }
}
