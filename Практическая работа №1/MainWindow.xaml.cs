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
using System.Threading;

namespace Практическая_работа__1
{
    /// <summary>
    /// Логика (её нет) взаимодействия для MainWindow.xaml
    /// </summary>

    #region Порядок нахождения жордановой формы матрицы
    /*
     1) Нахождение характеристического многочлена по формуле (только коэффициенты) в методе CharacterOfMatrix()
     2) Нахождение корней характеристического многочлена 
     3) Составление таблицы элементарных делителей по инвариантным множителям
     4) По элементарным делителям нахождение жордановой формы матрицы

    */
    #endregion

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        //Главная конпка РАССЧИТАТЬ
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!TryParseX())
            {
                MessageBox.Show("Некорректный ввод");
            }
            else
            {
                double[] result = new double[10];
                result = Calculation();
                PrintResult(result);
            }

        }

        //Вывод задания
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ПП1-3-6 Напишите программу нахождения\n      жордановой формы матрицы 3 × 3.");
        }

        private bool TryParseX()
        {
            bool res = true;
            double x1, x2, x3, x4, x5, x6, x7, x8, x9 = 0;
            bool x1b, x2b, x3b, x4b, x5b, x6b, x7b, x8b, x9b = false;
            x1b = double.TryParse(X1.Text, out x1); x2b = double.TryParse(X2.Text, out x2);
            x3b = double.TryParse(X3.Text, out x3); x4b = double.TryParse(X4.Text, out x4);
            x5b = double.TryParse(X5.Text, out x5); x6b = double.TryParse(X6.Text, out x6);
            x7b = double.TryParse(X7.Text, out x7); x8b = double.TryParse(X8.Text, out x8);
            x9b = double.TryParse(X9.Text, out x9);
            if (x1b && x2b && x3b && x4b && x5b && x6b && x7b && x8b && x9b) { res = true; } else { res = false; }
            return res;
        }

        private double[] Calculation()
        {
            double[] x = new double[9];
            x[0] = double.Parse(X1.Text);
            x[1] = double.Parse(X2.Text);
            x[2] = double.Parse(X3.Text);
            x[3] = double.Parse(X4.Text);
            x[4] = double.Parse(X5.Text);
            x[5] = double.Parse(X6.Text);
            x[6] = double.Parse(X7.Text);
            x[7] = double.Parse(X8.Text);
            x[8] = double.Parse(X9.Text);
            double[] inv = new double[9];
            double[] eigenvalue = new double[4];
            eigenvalue = CharacterOfMatrix(x);
            double[] res = new double[10];
            x = CharacterMatrix(x, eigenvalue);
            res = st(eigenvalue, x);
            return res;
        }

        //Нахождение характеристического многочлена матрицы и нахождение его корней 
        private double[] CharacterOfMatrix(double[] matrix)
        {
            double[] resCharacter = new double[4];
            //Коэффициенты для кубического уравнения
            double a, b, c;
            a = -1;
            b = matrix[0] + matrix[4] + matrix[8];
            c = -((matrix[4] * matrix[8]) + (matrix[0] * matrix[8]) + (matrix[0] * matrix[4]) - (matrix[5] * matrix[7]) - (matrix[1] * matrix[3]) - (matrix[2] * matrix[6]));
            double det = Determinant(matrix);
            resCharacter = CubicEquation(a, b, c, det);
            return resCharacter;
        }

        //Нахождение корней кубического уравнения
        private double[] CubicEquation(double a, double b, double c, double d)
        {
            double aa, bb, cc;
            aa = b / a; bb = c / a; cc = d / a;
            double[] res = new double[4];
            double Q, R, S, FI, X;
            Q = (aa * aa - 3 * bb) / 9;
            R = (2 * aa * aa * aa - 9 * aa * bb + 27 * cc) / 54;
            S = Q * Q * Q - R * R;
            if (S > 0)
            {
                FI = (Math.Acos(R / Math.Sqrt(Math.Pow(Q, 3)))) / 3;
                res[0] = -2 * Math.Sqrt(Q) * Math.Cos(FI) - (aa / 3);
                res[1] = -2 * Math.Sqrt(Q) * Math.Cos(FI + (2 * Math.PI / 3)) - (aa / 3);
                res[2] = -2 * Math.Sqrt(Q) * Math.Cos(FI - (2 * Math.PI / 3)) - (aa / 3);
            }
            else if (S < 0)
            {
                if (Q > 0)
                {
                    X = Math.Abs(R) / Math.Sqrt(Math.Pow(Q, 3));
                    FI = (Math.Log(X + Math.Sqrt(X * X - 1))) / 3;
                    res[0] = (-2 * Math.Sign(R) * Math.Sqrt(Q) * Math.Cosh(FI)) - aa / 3;
                    res[1] = Math.Sign(R) * Math.Sqrt(Q) * Math.Cosh(FI) - aa / 3;
                    res[2] = Math.Sqrt(3) * Math.Sqrt(Q) * Math.Sinh(FI);
                    res[3] = 1;
                }
                else if (Q < 0)
                {
                    X = Math.Abs(R) / Math.Sqrt(Math.Pow(Math.Abs(Q), 3));
                    FI = (Math.Log(X + Math.Sqrt(X * X + 1))) / 3;
                    res[0] = (-2 * Math.Sign(R) * Math.Sqrt(Math.Abs(Q)) * Math.Sinh(FI)) - aa / 3;
                    res[1] = Math.Sign(R) * Math.Sqrt(Math.Abs(Q)) * Math.Sinh(FI) - aa / 3;
                    res[2] = Math.Sqrt(3) * Math.Sqrt(Math.Abs(Q)) * Math.Cosh(FI);
                    res[3] = 1;
                }
                else
                {
                    res[0] = -Math.Sqrt(Math.Sqrt(c - (Math.Pow(aa, 3) / 27))) - aa / 3;
                    res[1] = -(a + res[0] / 2);
                    res[2] = Math.Sqrt(Math.Abs((aa - 3 * res[0]) * (aa + res[0]) - 4 * bb));
                    res[3] = 1;
                }
            }
            else // S==0
            {
                res[0] = -2 * Signum(R) * Math.Sqrt(Q) - (aa / 3);
                res[1] = Signum(R) * Math.Sqrt(Q) - (aa / 3);
                res[2] = res[1];
            }
            res[0] = Math.Round(res[0]);
            res[1] = Math.Round(res[1]);
            res[2] = Math.Round(res[2]);
            return res;
        }

        //Нахождение определителя матрицы
        private double Determinant(double[] mat)
        {
            double determinant;

            determinant = ((mat[0] * mat[4] * mat[8]) + (mat[6] * mat[1] * mat[5]) + (mat[2] * mat[3] * mat[7])
                - (mat[6] * mat[4] * mat[2]) - (mat[3] * mat[1] * mat[8]) - (mat[7] * mat[5] * mat[0]));

            return determinant;
        }

        //Вывод
        private void PrintResult(double[] res)
        {
            bool result = false;
            foreach (var r in res)
            {
                if (double.IsNaN(r) || double.IsInfinity(r))
                {
                    result = true;
                }
            }
            if (result == true) { MessageBox.Show("Переполнение"); }
            else
            {
                string s1 = Convert.ToString(res[4]) + " + i" + Convert.ToString(res[8]);
                string s2 = Convert.ToString(res[4]) + " - i" + Convert.ToString(res[8]);
                X1.Text = Convert.ToString(res[0]);
                X2.Text = Convert.ToString(res[1]);
                X3.Text = Convert.ToString(res[2]);
                X4.Text = Convert.ToString(res[3]);
                if (res[9] == 1) { X5.Text = s1; } else { X5.Text = Convert.ToString(res[4]); }
                X6.Text = Convert.ToString(res[5]);
                X7.Text = Convert.ToString(res[6]);
                X8.Text = Convert.ToString(res[7]);
                if (res[9] == 1) { X9.Text = s2; } else { X9.Text = Convert.ToString(res[8]); }
                Degree_Function(res);
            }
        }

        private double[] st(double[] eigen, double[] matrix)
        {
            double[] res = new double[10];

            if (eigen[0] != eigen[1] && eigen[1] != eigen[2] && eigen[0] != eigen[2])
            {
                res = Answer1(eigen);
            }
            else if (eigen[0] == eigen[1] && eigen[1] == eigen[2])
            {
                res = Answer2(eigen, matrix);
            }
            else
            {
                res = Answer3(eigen, matrix);
            }
            return res;
        }

        //Составление характеристической матрицы
        private double[] CharacterMatrix(double[] matrix, double[] eigenval)
        {
            double[] res = new double[10];
            res[0] = matrix[0] - (int)eigenval[0];
            res[1] = matrix[1];
            res[2] = matrix[2];
            res[3] = matrix[3];
            res[4] = matrix[4] - (int)eigenval[1];
            res[5] = matrix[5];
            res[6] = matrix[6];
            res[7] = matrix[7];
            res[8] = matrix[8] - (int)eigenval[2];
            res[9] = Convert.ToInt32(eigenval[3]);
            return res;
        }

        //Собственные значения разные
        private double[] Answer1(double[] x)
        {
            double[] answer = new double[10];
            answer[0] = x[0];
            answer[1] = 0;
            answer[2] = 0;
            answer[3] = 0;
            answer[4] = x[1];
            answer[5] = 0;
            answer[6] = 0;
            answer[7] = 0;
            answer[8] = x[2];
            answer[9] = x[3];
            return answer;
        }

        //Собственные значения равны
        private double[] Answer2(double[] x, double[] matrix)
        {
            double rank = Rank(matrix);
            double[] answer = new double[10];
            answer[0] = x[0];
            if (rank == 1) { answer[1] = 1; } else if (rank == 2 || rank == 3) { answer[1] = 1; } else { answer[1] = 0; }
            answer[2] = 0;
            answer[3] = 0;
            answer[4] = x[1];
            if (rank == 1) { answer[5] = 0; } else if (rank == 2 || rank == 3) { answer[5] = 1; } else { answer[5] = 0; }
            answer[6] = 0;
            answer[7] = 0;
            answer[8] = x[2];
            answer[9] = x[3];
            return answer;
        }

        //Два разных значения
        private double[] Answer3(double[] x, double[] matrix)
        {
            double s = 0;
            if (x[1] == x[2]) { s = x[0]; x[2] = s; x[0] = x[1]; }
            double[] answer = new double[10];
            double rank = Rank(matrix);
            answer[0] = x[0];
            if (rank != 2 && x[0] == x[1] && matrix[9] != 1) { if (rank == 0) { answer[1] = 0; } else { answer[1] = 1; } } else { answer[1] = 0; }
            answer[2] = 0;
            answer[3] = 0;
            answer[4] = x[1];
            answer[5] = 0;
            answer[6] = 0;
            answer[7] = 0;
            answer[8] = x[2];
            if (x[3] == 1) { answer[9] = 1; }
            return answer;
        }

        //Подсчет ранга матрицы
        private double Rank(double[] matrix)
        {
            double answer = 0;
            if (Determinant(matrix) == 0)
            {
                double[] m = new double[9]; m = Minors(matrix);
                if (m[0] == m[1] && m[1] == m[2] && m[2] == m[3] && m[3] == m[4] && m[4] == m[5] && m[5] == m[6] && m[6] == m[7] && m[7] == m[8])
                {
                    answer = 1;
                    if (m[0] == 0 && m[0] == m[1] && m[1] == m[2] && m[2] == m[3] && m[3] == m[4] && m[4] == m[5] && m[5] == m[6] && m[6] == m[7] && m[7] == m[8]) { answer = 0; }
                }
                else { answer = 2; }
            }
            else { answer = 3; }
            return answer;
        }

        //Подсчет миноров матрицы
        private double[] Minors(double[] matrix)
        {
            double[] res = new double[9];
            res[0] = (matrix[4] * matrix[8]) - (matrix[5] * matrix[7]);
            res[1] = (matrix[3] * matrix[8]) - (matrix[5] * matrix[6]);
            res[2] = (matrix[3] * matrix[7]) - (matrix[4] * matrix[6]);
            res[3] = (matrix[1] * matrix[8]) - (matrix[2] * matrix[7]);
            res[4] = (matrix[0] * matrix[8]) - (matrix[2] * matrix[6]);
            res[5] = (matrix[0] * matrix[7]) - (matrix[1] * matrix[6]);
            res[6] = (matrix[1] * matrix[5]) - (matrix[4] * matrix[2]);
            res[7] = (matrix[0] * matrix[5]) - (matrix[3] * matrix[2]);
            res[8] = (matrix[0] * matrix[4]) - (matrix[1] * matrix[3]);
            return res;
        }

        private double Signum(double a)
        {
            double result;
            if (a > 0) { result = 1; } else if (a < 0) { result = -1; } else { result = 0; }
            return result;
        }

        private void Degree_Function(double[] answer)
        {
            //получаем степень
            int degree;
            if (Int32.TryParse(Degree.Text, out degree) && degree > 0)
            {
                //Массив для промежуточных вычислений
                double[] computing = new double[9];
                for(int i = 0; i < 9; i++)
                {
                    //Копирование элемента
                    computing[i] = answer[i];
                }

                //Основной цикл возведения в степень
                for(int i = 1; i < degree; i++)
                {

                }

                //Выводим результат
                Answer_1.Text = Convert.ToString(answer[0]);
                Answer_2.Text = Convert.ToString(answer[1]);
                Answer_3.Text = Convert.ToString(answer[2]);
                Answer_4.Text = Convert.ToString(answer[3]);
                Answer_5.Text = Convert.ToString(answer[4]);
                Answer_6.Text = Convert.ToString(answer[5]);
                Answer_7.Text = Convert.ToString(answer[6]);
                Answer_8.Text = Convert.ToString(answer[7]);
                Answer_9.Text = Convert.ToString(answer[8]);
            }else { MessageBox.Show("Некорректная степень"); }
        }


    }
}