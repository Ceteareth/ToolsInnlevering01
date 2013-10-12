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
        List<GridTile> gridTileContainer;
        Image selectedTile;

        public MainWindow()
        {
            imgHandler = new ImageHandler();
            gridTileContainer = new List<GridTile>();
            InitializeComponent();
        }

        private void tileContainer_Loaded(object sender, RoutedEventArgs e)
        {
            ImageNode[] tiles = new ImageNode[imgHandler.getListBoxItemImages().Length];
            tiles = imgHandler.getListBoxItemImages();

            for (int i = 0; i < tiles.Length; i += 2)
            {
                // Creates a new container so that it displays in twos 
                StackPanel horizontalContainer = new StackPanel();
                horizontalContainer.Orientation = Orientation.Horizontal;

                Image firstImage = new Image();
                firstImage.Source = tiles[i].ImageSource;
                firstImage.Margin = new Thickness(5);

                // Enable selection, and removes .png from the name
                ListBoxItem firstItem = new ListBoxItem();
                firstItem.Name = tiles[i].Filename.Replace(".png", "");
                firstItem.Content = firstImage;
                firstItem.PreviewMouseDown += tileSelectionListener;

                horizontalContainer.Children.Add(firstItem);

                if (i + 1 < tiles.Length)
                {
                    Image secondImage = new Image();
                    secondImage.Source = tiles[i + 1].ImageSource;
                    secondImage.Margin = new Thickness(5);

                    // Enable selection, and removes .png from the name
                    ListBoxItem secondItem = new ListBoxItem();
                    secondItem.Name = tiles[i + 1].Filename.Replace(".png", "");
                    secondItem.Content = secondImage;
                    secondItem.PreviewMouseDown += tileSelectionListener;

                    horizontalContainer.Children.Add(secondItem);
                }

                else
                    break;

                tileMainWrap.Children.Add(horizontalContainer);
            }
        }

        // Responds to clicks on tiles in left panel
        private void tileSelectionListener(object sender, MouseEventArgs e)
        {
            var element = (UIElement)e.Source;

            if (element != null)
            {
                Image source = element as Image;
                Console.WriteLine("Sender: " + source.Source);
                setSelectedTileImage(source);
            }
        }

        private void setSelectedTileImage(Image image)
        {
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = image.Source;
            selectedTile = image;
            selectedTileGrid.Width = 100;
            selectedTileGrid.Height = 100;
            selectedTileGrid.Background = brush;
        }

        private void exit(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void gridTileListener(object sender, MouseEventArgs e)
        {
            var element = (UIElement)e.Source;
            setTileBackground(Grid.GetRow(element), Grid.GetColumn(element));
        }

        private void setTileBackground(int row, int column)
        {
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = selectedTile.Source;

            var tileToChange = UniGrid.Children.Cast<Grid>().First(ele => Grid.GetRow(ele) == row && Grid.GetColumn(ele) == column);
            tileToChange.Background = brush;
        }

        void mainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            AddRows(20, 20);
        }

        private void AddRows(int rows, int columns)
        {
            for (int i = 0; i < columns; i++)
            {
                ColumnDefinition colDef = new ColumnDefinition();
                colDef.Width = new GridLength(10);
                UniGrid.ColumnDefinitions.Add(new ColumnDefinition());
                
            }

            for (int i = 0; i < rows; i++)
            {
                RowDefinition rowDef = new RowDefinition();
                rowDef.Height = new GridLength(10);
                UniGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Grid tile = new Grid();

                    tile.Background = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    tile.ShowGridLines = true;
                    Grid.SetColumn(tile, i);
                    Grid.SetRow(tile, j);
                    UniGrid.Children.Add(tile);
                }
            }
        }
    }
}
