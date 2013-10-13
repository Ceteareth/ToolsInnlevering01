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
using System.Windows.Shapes;

namespace Innlevering01
{
    // Used for changing the collision maps of tiles.
    public partial class CollisionMapDialogue : Window
    {
        public int[][] tempCollisionMap { get; private set; }
        public bool changed { get; private set; }

        private readonly GridTile tileToChange;
        private readonly Brush comparisonBrush;

        public CollisionMapDialogue()
        {
            InitializeComponent();
        }

        // Fetches the image we're going to be using as our background, to easier represent how the 
        // collision map is going to be.
        public CollisionMapDialogue(GridTile image)
        {
            InitializeComponent();

            if (image == null) return;

            tileToChange = image;

            ImageBrush brush = new ImageBrush(image.Image.Source);
            CollisionMapGrid.Background = brush;

            tempCollisionMap = new int[3][];

            for (int i = 0; i < tempCollisionMap.Length; i++)
            {
                tempCollisionMap[i] = new int[i+3];
            }

            comparisonBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            LoadCollisionMap();
        }

        // If there's anything in the collision map, we change the grid to represent that.
        private void LoadCollisionMap()
        {
            for (int column = 0; column < 3; column++)
            {
                for (int row = 0; row < 3; row++)
                {
                    if (tileToChange.CollisionMap == null) continue;

                    if (tileToChange.CollisionMap[column][row] == 1)
                    {
                        var pendingChange = CollisionMapGrid.Children.Cast<TextBox>().First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column);
                        pendingChange.Background = comparisonBrush;
                    }
                }
            }
        }

        // Responds to clicks made on the collision map editor grid.
        private void CollisionSelection(object sender, MouseButtonEventArgs e)
        {
            changed = true;
            var element = (TextBox)e.Source;

            // Removes coloring representing denial of movement in the collision map.
            if (comparisonBrush.ToString().Equals(element.Background.ToString()))
            {
                element.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                tempCollisionMap[Grid.GetColumn(element)][Grid.GetRow(element)] = 0;
            }
            
            // Sets a red colour on the collision map editor grid, and sets the collision map accordingly.
            else
            {
                element.Background = comparisonBrush;
                tempCollisionMap[Grid.GetColumn(element)][Grid.GetRow(element)] = 1;
            }
        }

        // If we don't want to do any changes, flip a bool so that no changes are done on the other side, and close the window.
        private void CancelButton(object sender, MouseButtonEventArgs e)
        {
            changed = false;
            Close();
        }

        // If the user's ok with the changes, close the window and changes are caught in the editor grid, and written.
        private void OkButton(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
