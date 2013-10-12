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
        public MainWindow()
        {
            InitializeComponent();
            DatabaseHandler dbh = new DatabaseHandler();
        }

        private void exit(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void ImportAsset_OnClick(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = ""; // Default file name 
            dlg.DefaultExt = ".png"; // Default file extension 
            dlg.Filter = "Image file (.png)|*.png"; // Filter files by extension 

            // Show open file dialog box 
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
            }
        }
    }
}
