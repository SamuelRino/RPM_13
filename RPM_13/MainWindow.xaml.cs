using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibMas;

namespace RPM_13
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[,] matr;
        public MainWindow()
        {
            InitializeComponent();
            Matrica matrica = new Matrica();
            Matrica.Init(ref matr, 3, 3, 10);
            dg.ItemsSource = VisualArray.ToDataTable(matr).DefaultView;
        }

        private void btn_SetSize_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_rows.Text) && !string.IsNullOrEmpty(tb_columns.Text))
            {
                int rows = Convert.ToInt32(tb_rows.Text);
                int columns = Convert.ToInt32(tb_columns.Text);
                Matrica.Init(ref matr, rows, columns, 10);
                dg.ItemsSource = null;
                dg.ItemsSource = VisualArray.ToDataTable(matr).DefaultView;
            }
        }

        private void tb_PreviewKeyDown_NumberOnly(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key == Key.Back || e.Key == Key.Tab)
            {
                return;
            }
            e.Handled = true;
        }

        private void btn_Calculate_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void dg_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key == Key.Back || e.Key == Key.Tab || e.Key == Key.OemMinus)
            {
                return;
            }
            e.Handled = true;
        }

        private void dg_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;

            var currentCell = dg.CurrentCell;
            var cellContent = currentCell.Column.GetCellContent(currentCell.Item);

            string currentText = "";
            int selectionStart = 0;

            if (cellContent is TextBox textBox)
            {
                currentText = textBox.Text ?? "";
                selectionStart = textBox.SelectionStart;
            }

            if (e.Text == "-")
            {
                if (selectionStart != 0 || currentText.Contains("-"))
                {
                    e.Handled = true;
                    return;
                }
            }
        }
    }
}