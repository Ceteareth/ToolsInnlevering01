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
    /// Interaction logic for MainGrid.xaml
    /// </summary>
    public partial class MainGrid : UserControl
    {
        public MainGrid()
        {
            InitializeComponent();
        }

        // Listens for mouse click activity on the grid, and responds accordingly.
        private void GridTileListener(object sender, MouseEventArgs e)
        {
            var element = (UIElement)e.Source;
            SetTileBackground(Grid.GetRow(element), Grid.GetColumn(element));
        }

        // Default grid size is 20x20, doesn't work well with uneven columns and rows. Runs when the mainGrid WPF component is loaded
        void mainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            AddRows(20, 20);
        }

        // Adds all the rows and columns to the grid area.
        private void AddRows(int rows, int columns)
        {
            // Adds all the columns first.
            for (int i = 0; i < columns; i++)
            {
                ColumnDefinition colDef = new ColumnDefinition { Width = new GridLength(10) };
                UniGrid.ColumnDefinitions.Add(new ColumnDefinition());

            }

            // Adds all the rows.
            for (int i = 0; i < rows; i++)
            {
                RowDefinition rowDef = new RowDefinition { Height = new GridLength(10) };
                UniGrid.RowDefinitions.Add(new RowDefinition());
            }

            // Populates the grid with default start color, and adds the grid tile to a UniGrid's children collection.
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    GridTile tile = new GridTile { Background = new SolidColorBrush(Colors.DimGray), ShowGridLines = true };

                    Grid.SetColumn(tile, i);
                    Grid.SetRow(tile, j);
                    UniGrid.Children.Add(tile);
                    //UniGrid.Children.Add(new GridTile(j, i, new SolidColorBrush(Colors.DimGray)));
                }
            }
        }

        // Sets the background of a specific tile
        private void SetTileBackground(int row, int column)
        {
            Image image = LeftPanel.GetSelectedTileInfo();
            if (image == null) return;
            ImageBrush brush = new ImageBrush { ImageSource = image.Source };

            // Starts at the beginning of Unigrid.Children, iterates through until it finds an element that matches specified row and column, then saves it as tileToChange.
            var tileToChange = UniGrid.Children.Cast<GridTile>().First(ele => Grid.GetRow(ele) == row && Grid.GetColumn(ele) == column);
            tileToChange.Background = brush;
        }
    }
}
