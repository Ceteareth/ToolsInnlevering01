using System.Windows.Controls;
using System.Windows.Media;

namespace Innlevering01
{
    class ImageNode : TreeViewItem
    {
        public ImageSource ImageSource { get; set; }
        public string Filename { get; set; }
        public string Filepath { get; private set; }

        public ImageNode(ImageSource image, string filename, string filepath)
        {
            Filepath = filepath;
            ImageSource = image;
            Name = filename.Replace(".png", "");
            Header = filename.Replace(".png", "");
        }
    }
}
