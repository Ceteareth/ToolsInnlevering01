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
            filename.Replace(".png", "");
            imageSource = image;
            fileName = filename;
        }
    }
}
