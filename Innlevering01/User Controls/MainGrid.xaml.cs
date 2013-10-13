using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Linq;

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

        // Default grid size is 20x20, doesn't work well with uneven columns and rows. Runs when the GridContainer WPF component is loaded
        void mainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            ConstructGrid(20, 20);
        }

        // Adds all the rows and columns to the grid area.
        private void ConstructGrid(int rows, int columns)
        {
            // Adds all the columns first.
            for (int i = 0; i < columns; i++)
            {
                ColumnDefinition colDef = new ColumnDefinition { Width = new GridLength(10) };
                GridTileContainer.ColumnDefinitions.Add(new ColumnDefinition());

            }

            // Adds all the rows.
            for (int i = 0; i < rows; i++)
            {
                RowDefinition rowDef = new RowDefinition { Height = new GridLength(10) };
                GridTileContainer.RowDefinitions.Add(new RowDefinition());
            }

            // Populates the grid with default start color, and adds the grid tile to a GridTileContainer's children collection.
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    GridTile tile = new GridTile(0,0,null, null)
                    {
                        row = j,
                        column = i,
                        Background = new SolidColorBrush(Colors.DimGray), 
                        BorderBrush = new SolidColorBrush(Colors.Black), 
                        BorderThickness = new Thickness(0.5, 0.5, 0.0, 0.0),
                        IsReadOnly = true,
                        Cursor = Cursors.Arrow
                    };

                    Grid.SetColumn(tile, i);
                    Grid.SetRow(tile, j);
                    GridTileContainer.Children.Add(tile);
                }
            }
        }

        // Sets the background of or rotates a specific tile
        private void SetTileBackground(int row, int column)
        {
            ImageNode image = LeftPanel.GetSelectedTileInfo();
            if (image == null) return;
            ImageBrush brush = new ImageBrush { ImageSource = image.Image.Source };

            // Starts at the beginning of Unigrid.Children, iterates through until it finds an element that matches specified row and column, then saves it as tileToChange.
            var tileToChange = GridTileContainer.Children.Cast<GridTile>().First(ele => Grid.GetRow(ele) == row && Grid.GetColumn(ele) == column);

            if (tileToChange.image != null)
            {
                if (tileToChange.image == image.Image)
                {
                    if (tileToChange.Rotation < 3)
                    {
                        tileToChange.Rotation++;
                    }
                    else { tileToChange.Rotation = 0; }
                    //Rotates the tile 90 degrees clockwise
                    RotateTransform rotateTransform = new RotateTransform
                    {
                        CenterX = 0.5,
                        CenterY = 0.5,
                        Angle = tileToChange.Rotation*90
                    };
                    brush.RelativeTransform = rotateTransform;
                }
            }
            //Used to check if we click the same tile
            tileToChange.image = image.Image;
            tileToChange.Background = brush;
            tileToChange.Name = image.Name;
            tileToChange.collisionMap = image.CollisionMap;
        }

        private void OpenCollisionDialogue(object sender, MouseEventArgs e)
        {
            var element = (UIElement)e.Source;
            
            var tileToChange = GridTileContainer.Children.Cast<GridTile>().First(ele => Grid.GetRow(ele) == Grid.GetRow(element) && Grid.GetColumn(ele) == Grid.GetColumn(element));
            
            if (tileToChange.image == null) return;
            CollisionMapDialogue cmd = new CollisionMapDialogue(tileToChange);

            cmd.ShowDialog();

            if (cmd.changed)
            {
                tileToChange.collisionMap = cmd.tempCollisionMap;
            }
        }

        public void SerializeGrid()
        {
            GridTileSerializable[] serializableList = new GridTileSerializable[GridTileContainer.Children.Count];
            GridTileHandler gth = new GridTileHandler();;

            int counter = 0;
            foreach (GridTile child in GridTileContainer.Children.Cast<GridTile>())
            {
                serializableList[counter] = gth.SerializeGridTile(child);
                counter++;
            }

            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(serializableList.GetType());
            StreamWriter file = new StreamWriter(@"c:\temp\SerializationOverview.xml");
            x.Serialize(file, serializableList);
        }

        public void DeserializeGrid()
        {
            XDocument xdoc = XDocument.Load(@"c:\temp\SerializationOverview.xml");
            GridTileHandler gth = new GridTileHandler();

            var allValidData =
                from element in xdoc.Descendants("GridTileSerializable")
                where element.Elements("image").Any() 
                select element;

            foreach (var entry in allValidData)
            {
                byte[] temp = new Byte[entry.Element("image").Value.Length * sizeof(char)];

                GridTileSerializable gts = new GridTileSerializable
                {
                    Rotation = int.Parse(entry.Element("Rotation").Value),
                    Id = int.Parse(entry.Element("Rotation").Value),
                    row = int.Parse(entry.Element("row").Value),
                    column = int.Parse(entry.Element("column").Value),
                    image = new Byte[entry.Element("image").Value.Length*sizeof (char)],
                };

                gts.image = Convert.FromBase64String(entry.Element("image").Value);

                gts.collisionMap = new int[3][];
                for (int i = 0; i < gts.collisionMap.Length; i++)
                {
                    gts.collisionMap[i] = new int[3];
                }

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (entry.Element("collisionMap").IsEmpty) 
                            gts.collisionMap[i][j] = 0;
                        else
                            gts.collisionMap[i][j] = (int)Char.GetNumericValue(entry.Element("collisionMap").Value.ElementAt(i));
                    }
                }

                GridTile gridInfo = gth.DeserializeGridTile(gts);
                ImageBrush brush = new ImageBrush { ImageSource = gridInfo.image.Source };

                var tileToChange = GridTileContainer.Children.Cast<GridTile>().First(ele => Grid.GetRow(ele) == gridInfo.row && Grid.GetColumn(ele) == gridInfo.column);
                tileToChange.Background = brush;
                tileToChange.image = gridInfo.image;
                tileToChange.Name = gridInfo.Name;
                tileToChange.collisionMap = gridInfo.collisionMap;
            }
        }
    }
}
