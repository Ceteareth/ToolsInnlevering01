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

    public partial class LeftPanel : UserControl
    {
        public ImageHandler _imgHandler;
        static ImageNode _selectedTile;

        public LeftPanel()
        {     
#if DEBUG
            System.Diagnostics.PresentationTraceSources.DataBindingSource.Switch.Level = System.Diagnostics.SourceLevels.Critical;
#endif
            InitializeComponent();
            _imgHandler = new ImageHandler();
            _imgHandler.LoadImages();
        }

        /*private void tileContainer_Loaded(object sender, RoutedEventArgs e)
        {
            ImageNode[] tiles = _imgHandler.getListBoxItemImages();

            for (int i = 0; i < tiles.Length; i += 2)
            {
                
                // Creates a new container so that it displays in twos 
                StackPanel horizontalContainer = new StackPanel { Orientation = Orientation.Horizontal };

                Image firstImage = new Image { Source = tiles[i].Image, Margin = new Thickness(5) };
                _imgHandler.StoreImage(tiles[i]);

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
                    Image secondImage = new Image { Source = tiles[i + 1].Image, Margin = new Thickness(5) };

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

                //TileMainWrap.Children.Add(horizontalContainer);
            }
        }*/
         

        public void tileContainer_Loaded(object sender, RoutedEventArgs e)
        {
            ImageNode[] tiles = _imgHandler.GetListBoxItemImages();

            foreach (ImageNode tile in tiles)
            {
                tile.PreviewMouseDown += TileSelectionListener;
                tile.Foreground = new SolidColorBrush(Color.FromArgb(255, 238, 238, 238));
                tile.Opacity = 100;
                TileMainWrap.Items.Add(tile);
            }
        }

        // Responds to clicks on tiles in left panel
        private void TileSelectionListener(object sender, MouseEventArgs e)
        {
            var element = (ImageNode)e.Source;

            if (element == null) return;
            SetSelectedTileImage(element);
        }

        // Sets the active tile to be used on the grid
        private void SetSelectedTileImage(ImageNode node)
        {
            if (node == null) return;

            ImageBrush brush = new ImageBrush { ImageSource = node.Image.Source };
            _selectedTile = node;
            _selectedTile.Image = node.Image;
            SelectedTileGrid.Background = brush;
            _selectedTile.CollisionMap = node.CollisionMap;
        }

        public static ImageNode GetSelectedTileInfo()
        {
            return _selectedTile;
        }
    }
}
