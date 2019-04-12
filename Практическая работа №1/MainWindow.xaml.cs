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

namespace Практическая_работа__1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void X1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!TryParseX())
            {
                MessageBox.Show("Некорректный ввод");
            }
            else
            {
                Calculation();
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ПП1-3-6 Напишите программу нахождения\n      жордановой формы матрицы 3 × 3.");            
        }

        private bool TryParseX()
        {
            bool res = true;
            int x1, x2, x3, x4, x5, x6, x7, x8, x9 = 0;
            bool x1b, x2b, x3b, x4b, x5b, x6b, x7b, x8b, x9b = false;
            x1b = int.TryParse(X1.Text, out x1); x2b = int.TryParse(X2.Text, out x2);
            x3b = int.TryParse(X3.Text, out x3); x4b = int.TryParse(X4.Text, out x4);
            x5b = int.TryParse(X5.Text, out x5); x6b = int.TryParse(X6.Text, out x6);
            x7b = int.TryParse(X7.Text, out x7); x8b = int.TryParse(X8.Text, out x8);
            x9b = int.TryParse(X9.Text, out x9);
            if (x1b && x2b && x3b && x4b && x5b && x6b && x7b && x8b && x9b) { res = true; } else { res = false; }
            return res;
        }

        private int[] Calculation()
        {
            int[] x = new int[9];
            x[0] = int.Parse(X1.Text);
            x[1] = int.Parse(X2.Text);
            x[2] = int.Parse(X3.Text);
            x[3] = int.Parse(X4.Text);
            x[4] = int.Parse(X5.Text);
            x[5] = int.Parse(X6.Text);
            x[6] = int.Parse(X7.Text);
            x[7] = int.Parse(X8.Text);
            x[8] = int.Parse(X9.Text);

            int[] res = new int[9];


            return res;
        }
    }
}
