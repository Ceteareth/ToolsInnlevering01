using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Innlevering01.User_Controls
{
    public partial class LeftPanel : UserControl
    {
        public ImageHandler _imgHandler;
        static ImageNode _selectedTile;

        public LeftPanel()
        {     
            // A warning was causing some delay, the warning was not important. Surpressing it.
#if DEBUG
            System.Diagnostics.PresentationTraceSources.DataBindingSource.Switch.Level = System.Diagnostics.SourceLevels.Critical;
#endif
            InitializeComponent();
            _imgHandler = new ImageHandler();
            _imgHandler.LoadImages();
        }      

        // When the tileContainer is loaded, populate the list with entries from database or GFX folder.
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

        // Gets the currently selected tile, used for creating levels.
        public static ImageNode GetSelectedTileInfo()
        {
            return _selectedTile;
        }
    }
}
