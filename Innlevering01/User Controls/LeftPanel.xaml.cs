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

namespace Innlevering01.User_Controls
{
    /// <summary>
    /// Interaction logic for LeftPanel.xaml
    /// </summary>
    public partial class LeftPanel : UserControl
    {
        readonly ImageHandler _imgHandler;
        static Image _selectedTile;

        public LeftPanel()
        {     
#if DEBUG
            System.Diagnostics.PresentationTraceSources.DataBindingSource.Switch.Level = System.Diagnostics.SourceLevels.Critical;
#endif
            InitializeComponent();
            _imgHandler = new ImageHandler();
        }

        private void tileContainer_Loaded(object sender, RoutedEventArgs e)
        {
            ImageNode[] tiles = _imgHandler.getListBoxItemImages();

            for (int i = 0; i < tiles.Length; i += 2)
            {
                
                // Creates a new container so that it displays in twos 
                StackPanel horizontalContainer = new StackPanel { Orientation = Orientation.Horizontal };

                Image firstImage = new Image { Source = tiles[i].ImageSource, Margin = new Thickness(5) };
                _imgHandler.StorePicture(tiles[i]);

                // Enable selection, and removes .png from the name
                ListBoxItem firstItem = new ListBoxItem
                {
                    Name = tiles[i].Filename.Replace(".png", ""),
                    Content = firstImage,
                };

                firstItem.PreviewMouseDown += TileSelectionListener;

                horizontalContainer.Children.Add(firstItem);

                if (i + 1 < tiles.Length)
                {
                    Image secondImage = new Image { Source = tiles[i + 1].ImageSource, Margin = new Thickness(5) };

                    // Enable selection, and removes .png from the name
                    ListBoxItem secondItem = new ListBoxItem
                    {
                        Name = tiles[i + 1].Filename.Replace(".png", ""),
                        Content = secondImage
                    };

                    secondItem.PreviewMouseDown += TileSelectionListener;

                    horizontalContainer.Children.Add(secondItem);
                }

                else
                    break;

                TileMainWrap.Children.Add(horizontalContainer);
            }
        }

        // Responds to clicks on tiles in left panel
        private void TileSelectionListener(object sender, MouseEventArgs e)
        {
            var element = (UIElement)e.Source;

            if (element == null) return;
            Image source = element as Image;
            SetSelectedTileImage(source);
        }

        // Sets the active tile to be used on the grid
        private void SetSelectedTileImage(Image image)
        {
            if (image == null) return;
            ImageBrush brush = new ImageBrush { ImageSource = image.Source };
            _selectedTile = image;
            SelectedTileGrid.Width = 100;
            SelectedTileGrid.Height = 100;
            SelectedTileGrid.Background = brush;
        }

        public static Image GetSelectedTileInfo()
        {
            return _selectedTile;
        }
    }
}
