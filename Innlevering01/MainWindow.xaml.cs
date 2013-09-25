using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Innlevering01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ImageHandler imgHandler;

        public MainWindow()
        {
            imgHandler = new ImageHandler();
            InitializeComponent();
        }

        private void fileOpen_Click(object sender, RoutedEventArgs e)
        {
            // Test
        }

        private void tileContainer_Loaded(object sender, RoutedEventArgs e)
        {
            ImageNode[] tiles = new ImageNode[imgHandler.getListBoxItemImages().Length];
            tiles = imgHandler.getListBoxItemImages();

            for (int i = 0; i < tiles.Length; i += 2)
            {
                StackPanel horizontalContainer = new StackPanel();
                horizontalContainer.Orientation = Orientation.Horizontal;

                Image firstImage = new Image();
                firstImage.Source = tiles[i].ImageSource;
                firstImage.Margin = new Thickness(5);

                ListBoxItem firstItem = new ListBoxItem();
                firstItem.Name = tiles[i].Filename.Replace(".png", "");
                firstItem.Content = firstImage;

                horizontalContainer.Children.Add(firstItem);

                if (i + 1 >= tiles.Length)
                    break;

                else
                {
                    Image secondImage = new Image();
                    secondImage.Source = tiles[i + 1].ImageSource;
                    secondImage.Margin = new Thickness(5);

                    ListBoxItem secondItem = new ListBoxItem();
                    secondItem.Name = tiles[i + 1].Filename.Replace(".png", "");
                    secondItem.Content = secondImage;

                    horizontalContainer.Children.Add(secondItem);
                }
                
                tileMainWrap.Children.Add(horizontalContainer);
            }
        }
    }
}
