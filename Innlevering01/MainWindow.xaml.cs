using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;

namespace Innlevering01
{
    // This is our main window.
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Automatically maximizes the window.
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
        }

        void Exit(object sender, EventArgs e)
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
                Image img = new Image();

                // Create image of selected file, sizing it down to minimize database and save file size.
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(filePath);
                bitmapImage.DecodePixelHeight = 64;
                bitmapImage.DecodePixelWidth = 64;
                bitmapImage.EndInit();

                Console.WriteLine(Path.GetFileName(filePath));

                img.Source = bitmapImage;
                ImageNode image = new ImageNode(img, Path.GetFileName(filePath).ToLower(), filePath);

                // Stores the image in the database, and loads the asset list again.
                LeftPanel._imgHandler.StoreImage(image);
                LeftPanel._imgHandler.LoadImages();

                // Hackish solution, but out of time. Forces a reload of the left panel to show the new asset.
                LeftPanel.tileContainer_Loaded(new object(), new RoutedEventArgs());
            }
        }

        // Serializes and saves the grid to C:/temp/saveFile.xml.
        // Unfortunately this only enables for one save file, but time is running out.
        private void SaveGrid(object sender, MouseButtonEventArgs e)
        {
            MainGrid.SerializeGrid();
        }

        // Deserializes and loads the grid from C:/temp/saveFile.xml. 
        private void OpenGrid(object sender, MouseButtonEventArgs e)
        {
            MainGrid.DeserializeGrid();
        }
    }
}
