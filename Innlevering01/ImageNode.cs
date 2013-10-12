using System.Windows.Media;

namespace Innlevering01
{
    class ImageNode
    {
        public ImageSource ImageSource { get; set; }
        public string Filename { get; set; }
        public string Filepath { get; private set; }

        public ImageNode(ImageSource image, string filename, string filepath)
        {
            Filepath = filepath;
            filename.Replace(".png", "");
            ImageSource = image;
            Filename = filename;
        }
    }
}
