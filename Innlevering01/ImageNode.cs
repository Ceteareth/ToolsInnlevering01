using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Innlevering01
{
    class ImageNode
    {
        private ImageSource imageSource;
        private string fileName;

        public ImageSource ImageSource
        {
            get { return imageSource; }
            set { imageSource = value; }
        }

        public string Filename
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public ImageNode(ImageSource image, string filename)
        {
            imageSource = image;
            fileName = filename;
        }
    }
}
