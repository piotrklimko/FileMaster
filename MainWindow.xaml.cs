using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace FileMaster
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadFilesAndDirectories("C:\\", LeftFileList);
            LoadFilesAndDirectories("C:\\", RightFileList);
        }

        private void LoadFilesAndDirectories(string path, ListBox listBox)
        {
            try
            {
                var directories = Directory.GetDirectories(path);
                var files = Directory.GetFiles(path);

                listBox.Items.Clear();

                foreach (var directory in directories)
                {
                    listBox.Items.Add("[DIR] " + directory);
                }

                foreach (var file in files)
                {
                    listBox.Items.Add(file);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading files and directories: " + ex.Message);
            }
        }

        private void OnGoButtonClick(object sender, RoutedEventArgs e)
        {
            var path = PathTextBox.Text;

            if (Directory.Exists(path))
            {
                LoadFilesAndDirectories(path, LeftFileList);
                LoadFilesAndDirectories(path, RightFileList);
            }
            else
            {
                MessageBox.Show("Invalid path");
            }
        }

        private void OnLeftFileListDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (LeftFileList.SelectedItem != null)
            {
                var selectedItem = LeftFileList.SelectedItem.ToString();
                if (selectedItem.StartsWith("[DIR] "))
                {
                    var newPath = selectedItem.Substring(6);
                    PathTextBox.Text = newPath;
                    LoadFilesAndDirectories(newPath, LeftFileList);
                }
            }
        }

        private void OnRightFileListDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (RightFileList.SelectedItem != null)
            {
                var selectedItem = RightFileList.SelectedItem.ToString();
                if (selectedItem.StartsWith("[DIR] "))
                {
                    var newPath = selectedItem.Substring(6);
                    PathTextBox.Text = newPath;
                    LoadFilesAndDirectories(newPath, RightFileList);
                }
            }
        }
    }
}
