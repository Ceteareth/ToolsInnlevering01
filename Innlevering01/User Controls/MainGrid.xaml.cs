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

        private void GridTileListener(object sender, MouseEventArgs e)
        {
            var element = (UIElement)e.Source;
            SetTileBackground(Grid.GetRow(element), Grid.GetColumn(element));
        }

        void mainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            AddRows(20, 20);
        }

        private void AddRows(int rows, int columns)
        {
            for (int i = 0; i < columns; i++)
            {
                ColumnDefinition colDef = new ColumnDefinition { Width = new GridLength(10) };
                UniGrid.ColumnDefinitions.Add(new ColumnDefinition());

            }

            for (int i = 0; i < rows; i++)
            {
                RowDefinition rowDef = new RowDefinition { Height = new GridLength(10) };
                UniGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Grid tile = new Grid { Background = new SolidColorBrush(Colors.DimGray), ShowGridLines = true };

                    Grid.SetColumn(tile, i);
                    Grid.SetRow(tile, j);
                    UniGrid.Children.Add(tile);
                }
            }
        }

        private void SetTileBackground(int row, int column)
        {
            Image image = LeftPanel.GetSelectedTileInfo();
            if (image == null) return;
            ImageBrush brush = new ImageBrush { ImageSource = image.Source };

            var tileToChange = UniGrid.Children.Cast<Grid>().First(ele => Grid.GetRow(ele) == row && Grid.GetColumn(ele) == column);
            tileToChange.Background = brush;
        }
    }
}
