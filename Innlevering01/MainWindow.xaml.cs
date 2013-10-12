using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Innlevering01.User_Controls;

namespace Innlevering01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<GridTile> gridTileContainer;

        public MainWindow()
        {
            gridTileContainer = new List<GridTile>();
            InitializeComponent();
        }

        private void exit(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        /*private void GridTileListener(object sender, MouseEventArgs e)
        {
            var element = (UIElement)e.Source;
            SetTileBackground(Grid.GetRow(element), Grid.GetColumn(element));
        }*/

        /*private void SetTileBackground(int row, int column)
        {
            Image image = LeftPanel.GetSelectedTileInfo();
            if (image == null) return;
            ImageBrush brush = new ImageBrush {ImageSource = image.Source};

            var tileToChange = UniGrid.Children.Cast<Grid>().First(ele => Grid.GetRow(ele) == row && Grid.GetColumn(ele) == column);
            tileToChange.Background = brush;
        }*/

        /*void mainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            AddRows(20, 20);
        }

        private void AddRows(int rows, int columns)
        {
            for (int i = 0; i < columns; i++)
            {
                ColumnDefinition colDef = new ColumnDefinition {Width = new GridLength(10)};
                UniGrid.ColumnDefinitions.Add(new ColumnDefinition());
                
            }

            for (int i = 0; i < rows; i++)
            {
                RowDefinition rowDef = new RowDefinition {Height = new GridLength(10)};
                UniGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Grid tile = new Grid {Background = new SolidColorBrush(Colors.DimGray), ShowGridLines = true};

                    Grid.SetColumn(tile, i);
                    Grid.SetRow(tile, j);
                    UniGrid.Children.Add(tile);
                }
            }
        }*/
    }
}
