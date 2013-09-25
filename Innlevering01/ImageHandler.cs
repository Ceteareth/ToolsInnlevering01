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
        Dictionary<string, ImageNode> images;

        public ImageHandler()
        {
            images = new Dictionary<string, ImageNode>();
            string path = Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory());
            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(path),"GFX\\"));
            Console.WriteLine(dirInfo);
            FileInfo[] fileInfo = dirInfo.GetFiles("*.png");

            if(fileInfo.Length > 0)
            {
                foreach (FileInfo info in fileInfo)
                {
                    Console.WriteLine(info.FullName);
                    images.Add(info.FullName, new ImageNode(new BitmapImage(new Uri(info.FullName)), info.FullName));
                }
            }

             /// NOTE: Add exception or other type of handling in case of no images in project
        }
    }
}
