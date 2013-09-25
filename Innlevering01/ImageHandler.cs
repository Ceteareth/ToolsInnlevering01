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

        public ImageHandler()
        {
            // Get dynamic project path
            string path = Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory());
            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(path),"GFX\\"));
            
            FileInfo[] fileInfo = dirInfo.GetFiles("*.png");

            images = new ImageNode[fileInfo.Length];

            if(fileInfo.Length > 0)
            {
                int i = 0;
                // Loading images into container class
                foreach (FileInfo info in fileInfo)
                {
                    images[i] = new ImageNode(new BitmapImage(new Uri(info.FullName)), info.Name);
                    Console.WriteLine(info.Name);
                    i++;
                }
            }

             /// NOTE: Add exception or other type of handling in case of no images in project
        }

        public ImageNode[] getListBoxItemImages()
        {
            return images;
        }
    }
}
