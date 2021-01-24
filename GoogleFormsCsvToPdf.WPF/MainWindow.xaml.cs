using Microsoft.Win32;
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

namespace GoogleFormsCsvToPdf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string fileName = "";
        private FormData formData;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            ToggleWindowEnabled();
            var fileDialog = new OpenFileDialog();

            if (fileDialog.ShowDialog() ?? false)
            {
                try
                {
                    formData = CsvImporter.ReadCsv(fileDialog.FileName);

                    OpenLabel.Content = fileDialog.SafeFileName;
                    fileName = RemoveExtensionFromFileName(fileDialog.SafeFileName);
                    ConvertButton.IsEnabled = true;
                }
                catch (Exception exc)
                {
                    DisplayError("Failed to open file", exc.Message);
                }
            }

            new FieldSelectionDialog(formData).ShowDialog();

            ToggleWindowEnabled();
        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            ToggleWindowEnabled();
            var fileDialog = new SaveFileDialog()
            {
                Filter = "PDF file (*.pdf)|*.pdf",
                FileName = fileName,
                OverwritePrompt = false,
                AddExtension = true
            };

            if (fileDialog.ShowDialog() ?? false)
            {
                try
                {
                    PdfExporter.WritePdf(fileDialog.FileName, formData);

                    OpenLabel.Content = "";
                    fileName = "";
                    ConvertButton.IsEnabled = false;
                }
                catch (Exception exc)
                {
                    DisplayError("Failed to convert and save file", exc.Message);
                }
            }
            ToggleWindowEnabled();
        }

        private void DisplayError(string title, string error)
        {
            MessageBox.Show(error, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private string RemoveExtensionFromFileName(string file)
        {
            if (file.Contains("."))
                return file.Substring(0, file.LastIndexOf("."));
            else
                return file;
        }

        private void ToggleWindowEnabled()
        {
            this.IsEnabled = !this.IsEnabled;
        }
    }
}
