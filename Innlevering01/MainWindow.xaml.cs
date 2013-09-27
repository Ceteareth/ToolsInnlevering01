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
                firstItem.PreviewMouseDown += tileListener;

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
                    secondItem.PreviewMouseDown += tileListener;

                    horizontalContainer.Children.Add(secondItem);
                }

                else
                    break;

                tileMainWrap.Children.Add(horizontalContainer);
            }
        }

        // Responds to clicks on tiles in left panel
        private void tileListener(object sender, EventArgs e)
        {
            ListBoxItem clickedTile = sender as ListBoxItem;
            Console.WriteLine("Sender: " + clickedTile.Name);
        }

        private void exit(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            var element = (UIElement)e.Source;

            int c = Grid.GetColumn(element);
            int r = Grid.GetRow(element);

            Console.WriteLine("Column: " + c + " Row: " + r);
        }

        void mainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            AddRows(new Size(64, 64));
        }

        // Might approach this differently, hard to get coordinates correctly.
        private void AddRows(Size recSize)
        {
            UniGrid.Columns = (int)(UniGrid.ActualWidth / recSize.Width);
            UniGrid.Rows = (int)(UniGrid.ActualHeight / recSize.Height);
            Console.WriteLine("Column: " + UniGrid.Columns + " Row: " + UniGrid.Rows);
            for (int i = 0; i < UniGrid.Columns * UniGrid.Rows; i++)
            {
                UniGrid.Children.Add(new Rectangle { Fill = new SolidColorBrush(Colors.Yellow), Margin = new Thickness(1) });
            }

            int columnCounter = 0;
            for (int i = 0; i < UniGrid.Columns * UniGrid.Rows; i++)
            {   
                Console.WriteLine(UniGrid.Children[i] + " on column " + columnCounter + " row " + (int)i / UniGrid.Columns);
                if (columnCounter != UniGrid.Columns - 1)
                    columnCounter++;
                else
                    columnCounter = 0;
            }
        }
    }
}
