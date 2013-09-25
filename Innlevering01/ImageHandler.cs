using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Reflection;

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
            
            FileInfo[] fileInfo = dirInfo.GetFiles("*.png");

            images = new ImageNode[fileInfo.Length];

            if(fileInfo.Length > 0)
            {
                // Loading images into container class
                for (int i = 0; i < fileInfo.Length; i++)
                    images[i] = new ImageNode(new BitmapImage(new Uri(fileInfo[i].FullName)), fileInfo[i].Name);
            }
             /// NOTE: Add exception or other type of handling in case of no images in project
        }

        public ImageNode[] getListBoxItemImages()
        {
            return images;
        }
    }
}
