using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
                        Row = j,
                        Column = i,
                        Background = new SolidColorBrush(Colors.DimGray), 
                        BorderBrush = new SolidColorBrush(Colors.Black), 
                        BorderThickness = new Thickness(0.5, 0.5, 0.0, 0.0),
                        IsReadOnly = true,
                        Cursor = Cursors.Arrow
                    };

                    // Places the grid tile's grid position and adds it to GridTileContainers children collection.
                    Grid.SetColumn(tile, i);
                    Grid.SetRow(tile, j);
                    GridTileContainer.Children.Add(tile);
                }
            }
        }

        // Sets the background of or rotates a specific tile
        private void SetTileBackground(int row, int column)
        {
            // Fetches the current tile it's supposed to place.
            ImageNode image = LeftPanel.GetSelectedTileInfo();

            // If nothing is selected, don't do anything.
            if (image == null) return;

            // If not, set a brush so we can change the background of the tile.
            ImageBrush brush = new ImageBrush { ImageSource = image.Image.Source };

            // Starts at the beginning of Unigrid.Children, iterates through until it finds an element that matches specified row and column, then saves it as tileToChange.
            var tileToChange = GridTileContainer.Children.Cast<GridTile>().First(ele => Grid.GetRow(ele) == row && Grid.GetColumn(ele) == column);

            if (tileToChange.Image != null)
            {
                if (tileToChange.Image == image.Image)
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

            // Replaces all old info with the new.
            tileToChange.Image = image.Image;
            tileToChange.Background = brush;
            tileToChange.Name = image.Name;
            tileToChange.CollisionMap = image.CollisionMap;
        }

        // Opens a new window where one can change the collision map visually.
        private void OpenCollisionDialogue(object sender, MouseEventArgs e)
        {
            // Gets the source, and finds the specific tile the user wants to edit the collision map of.
            var element = (UIElement)e.Source;
            var tileToChange = GridTileContainer.Children.Cast<GridTile>().First(ele => Grid.GetRow(ele) == Grid.GetRow(element) && Grid.GetColumn(ele) == Grid.GetColumn(element));
            
            // If the user has clicked a non-set tile, return.
            if (tileToChange.Image == null) return;

            // Creates a new dialogue for collision map editing, shows it, records feedback and saves it in the collision
            // map of the original tile.
            CollisionMapDialogue cmd = new CollisionMapDialogue(tileToChange);
            cmd.ShowDialog();

            if (cmd.changed)
            {
                tileToChange.CollisionMap = cmd.tempCollisionMap;
            }
        }

        public void SerializeGrid()
        {
            // Creates a list of serializable gridtiles, as normal gridtiles contain a lot of extra information.
            // We're sure it's a sexier way of doing this, but we're out of time.
            GridTileSerializable[] serializableList = new GridTileSerializable[GridTileContainer.Children.Count];
            GridTileHandler gth = new GridTileHandler();;

            // Goes through the whole grid and converts all grid tiles to serializable grid tiles.
            int counter = 0;
            foreach (GridTile child in GridTileContainer.Children.Cast<GridTile>())
            {
                serializableList[counter] = gth.SerializeGridTile(child);
                counter++;
            }

            // Creates a serializer and a stream, and fills the stream with the serializable list, and writes it to a XML file.
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(serializableList.GetType());
            StreamWriter file = new StreamWriter(@"c:\temp\saveFile.xml");
            x.Serialize(file, serializableList);
        }

        public void DeserializeGrid()
        {
            // Finds the XML document, and creates a handler for converting them back to normal grid tiles.
            XDocument xdoc = XDocument.Load(@"c:\temp\saveFile.xml");
            GridTileHandler gth = new GridTileHandler();

            // Queries that only gets entries with an image tag present, as all other is superflous.
            // Could have just avoided writing them to the file in the first place, but oh well.
            var allValidData =
                from element in xdoc.Descendants("GridTileSerializable")
                where element.Elements("Image").Any() 
                select element;

            Console.WriteLine(allValidData.Count());

            // Goes through each entry it finds in the XML file and converts it back to a grid tile.
            foreach (var entry in allValidData)
            {
                byte[] temp = new Byte[entry.Element("Image").Value.Length * sizeof(char)];

                // Firstly, recreate the serializable grid tile.
                GridTileSerializable gts = new GridTileSerializable
                {
                    Rotation = int.Parse(entry.Element("Rotation").Value),
                    Id = int.Parse(entry.Element("Rotation").Value),
                    Row = int.Parse(entry.Element("Row").Value),
                    Column = int.Parse(entry.Element("Column").Value),
                };

                Console.WriteLine(entry.Element("Column").Value);
                gts.Image = new Byte[entry.Element("Image").Value.Length*sizeof (char)];
                // Apparently byte arrays are saved as a base 64 string, so we convert that this way.
                gts.Image = Convert.FromBase64String(entry.Element("Image").Value);

                // Copies the collision map size.
                gts.CollisionMap = new int[3][];
                for (int i = 0; i < gts.CollisionMap.Length; i++)
                {
                    gts.CollisionMap[i] = new int[3];
                }

                // Copies the content of the collision map.
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (entry.Element("CollisionMap").IsEmpty) 
                            gts.CollisionMap[i][j] = 0;
                        else
                            gts.CollisionMap[i][j] = (int)Char.GetNumericValue(entry.Element("CollisionMap").Value.ElementAt(i));
                    }
                }

                // Converts all info of the serializable grid tile back to a normal grid tile.
                GridTile gridInfo = gth.DeserializeGridTile(gts);
                // Creates a brush based on the image in the grid tile.
                ImageBrush brush = new ImageBrush { ImageSource = gridInfo.Image.Source };

                // Finds the grid tile in the specific spot in the grid it was before, and sets all appropriate information.
                var tileToChange = GridTileContainer.Children.Cast<GridTile>().First(ele => Grid.GetRow(ele) == gridInfo.Row && Grid.GetColumn(ele) == gridInfo.Column);
                tileToChange.Background = brush;
                tileToChange.Image = gridInfo.Image;
                tileToChange.Name = gridInfo.Name;
                tileToChange.CollisionMap = gridInfo.CollisionMap;
            }
        }
    }
}
