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

        public CollisionMapDialogue(GridTile image)
        {
            InitializeComponent();

            if (image == null) return;

            tileToChange = image;

            ImageBrush brush = new ImageBrush(image.image.Source);
            CollisionMapGrid.Background = brush;

            tempCollisionMap = new int[3][];

            for (int i = 0; i < tempCollisionMap.Length; i++)
            {
                tempCollisionMap[i] = new int[i+3];
            }

            comparisonBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            LoadCollisionMap();
        }

        private void LoadCollisionMap()
        {
            for (int column = 0; column < 3; column++)
            {
                for (int row = 0; row < 3; row++)
                {
                    if (tileToChange.collisionMap == null) continue;

                    if (tileToChange.collisionMap[column][row] == 1)
                    {
                        var pendingChange = CollisionMapGrid.Children.Cast<TextBox>().First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column);
                        pendingChange.Background = comparisonBrush;
                    }
                }
            }
        }

        private void CollisionSelection(object sender, MouseButtonEventArgs e)
        {
            changed = true;
            var element = (TextBox)e.Source;

            if (comparisonBrush.ToString().Equals(element.Background.ToString()))
            {
                element.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                tempCollisionMap[Grid.GetColumn(element)][Grid.GetRow(element)] = 0;
            }
            else
            {
                element.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                tempCollisionMap[Grid.GetColumn(element)][Grid.GetRow(element)] = 1;
            }
        }

        private void CancelButton(object sender, MouseButtonEventArgs e)
        {
            changed = false;
            Close();
        }

        private void OkButton(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
