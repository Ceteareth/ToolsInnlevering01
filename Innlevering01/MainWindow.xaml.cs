using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Innlevering01.User_Controls;
using Image = System.Windows.Controls.Image;

namespace Innlevering01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
        }

        private void exit(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void ImportAsset_OnClick(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                FileName = "",
                DefaultExt = ".png",
                Filter = "Image file (.png)|*.png"
            };

            // Show open file dialog box 
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Open document 
                string filePath = dlg.FileName;
                Image i = new Image();

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(filePath);
                bitmapImage.DecodePixelHeight = 64;
                bitmapImage.DecodePixelWidth = 64;
                bitmapImage.EndInit();

                Console.WriteLine(Path.GetFileName(filePath));

                i.Source = bitmapImage;
                ImageNode image = new ImageNode(i, Path.GetFileName(filePath).ToLower(), filePath);

                LeftPanel._imgHandler.StoreImage(image);
                LeftPanel._imgHandler.LoadImages();

                // Hackish solution, but out of time. Forces a reload of the left panel
                LeftPanel.tileContainer_Loaded(new object(), new RoutedEventArgs());
            }
        }

        private void SaveGrid(object sender, MouseButtonEventArgs e)
        {
            MainGrid.SerializeGrid();
        }

        private void OpenGrid(object sender, MouseButtonEventArgs e)
        {
            MainGrid.DeserializeGrid();
        }
    }
}
