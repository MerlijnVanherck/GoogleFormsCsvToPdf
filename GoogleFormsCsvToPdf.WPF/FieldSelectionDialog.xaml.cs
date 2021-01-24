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

namespace GoogleFormsCsvToPdf
{
    /// <summary>
    /// Interaction logic for FieldSelectionDialog.xaml
    /// </summary>
    public partial class FieldSelectionDialog : Window
    {
        public FieldSelectionDialog(FormData formData)
        {
            InitializeComponent();
            DrawGrid(formData);
        }

        private void DrawGrid(FormData data)
        {
            int row = 1;
            foreach (var f in data.Headers)
            {
                grid.RowDefinitions.Add(new RowDefinition());
                var margin = new Thickness(5);

                var lbl = new Label();
                lbl.Content = f.Text;
                lbl.Margin = margin;
                lbl.VerticalContentAlignment = VerticalAlignment.Center;
                Grid.SetRow(lbl, row);
                Grid.SetColumn(lbl, 0);
                grid.Children.Add(lbl);

                var box = new ComboBox();
                box.ItemsSource = Enum.GetValues(typeof(ExportMode));
                box.Margin = margin;
                box.Height = 25;
                Grid.SetRow(box, row);
                Grid.SetColumn(box, 1);
                grid.Children.Add(box);

                var binding = new Binding("Export");
                binding.Source = f;
                box.SetBinding(ComboBox.SelectedValueProperty, binding);

                row++;
            }
        }
    }
}
