using BespokeFusion;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace Converter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            TxtBxImportPath.Text = Properties.Settings.Default.ImportPath;
            TxtBxSteamPath.Text = Properties.Settings.Default.SteamPath;
            TglBtnRestartSteam.IsChecked = Properties.Settings.Default.RestartSteam;
            TglBtnDelteFiles.IsChecked = Properties.Settings.Default.DeleteFiles;
        }

        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnPowerOff_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnImportPath_Click(object sender, RoutedEventArgs e)
        {
            TxtBxImportPath.Text = GetPath();
        }

        private void BtnSteamPath_Click(object sender, RoutedEventArgs e)
        {
            TxtBxSteamPath.Text = GetPath();
        }

        private string GetPath()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = dialog.SelectedPath;

                return path;
            }
            return "";
        }

        private void RestartSteam()
        {
            var process = Process.GetProcessesByName("Steam")[0];
            process.Kill();

            string path = TxtBxSteamPath.Text;
            string[] extract = Regex.Split(path, "userdata");
            string main = extract[0].TrimEnd('\\');

            string steam = Path.Combine(main + "\\Steam.exe");
            Process.Start(steam);
        }

        private void BtnConvert_Click(object sender, RoutedEventArgs e)
        {
            string ImpPath = TxtBxImportPath.Text;
            string SteamPath = TxtBxSteamPath.Text;
            bool deleteFiles = Convert.ToBoolean(TglBtnDelteFiles.IsChecked);

            if (ImpPath != "" && SteamPath != "")
            {
                Functions.SortScreenshots(ImpPath);
                Functions.ConvertAndExport(SteamPath, deleteFiles);
            }
            else if(ImpPath == "" && SteamPath != "")
                MaterialMessageBox.Show("You need to Fill out import Field", "Empty Field");
            else if (SteamPath == "" && ImpPath != "")
                MaterialMessageBox.Show("You need to Fill out steam Field", "Empty Field");
            else if (ImpPath == "" && SteamPath == "")
                MaterialMessageBox.Show("Both Fields must be filled ", "Empty Fields");

            if (TglBtnRestartSteam.IsChecked == true)
                RestartSteam();
            else
                MaterialMessageBox.Show("You need to restart your Steam client to upload screenshots", "Info");
        }

        private void BtnSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ImportPath = TxtBxImportPath.Text;
            Properties.Settings.Default.SteamPath = TxtBxSteamPath.Text;
            Properties.Settings.Default.RestartSteam = Convert.ToBoolean(TglBtnRestartSteam.IsChecked);
            Properties.Settings.Default.DeleteFiles = Convert.ToBoolean(TglBtnDelteFiles.IsChecked);
        }
    }
}
