﻿using System;
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
    /// 
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

        private void X1_TextChanged(object sender, TextChangedEventArgs e)
        {

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
                double[] result = Calculation();
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

        private double[] Calculation()
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

            CharacterOfMatrix(x);

            double[] res = new double[9];

            return res;
        }

        //Нахождение характеристического многочлена матрицы и нахождение его корней 
        private double[] CharacterOfMatrix(int[] matrix)
        {
            double[] resCharacter = new double[3];

            //Коэффициенты для кубического уравнения
            int a, b, c;

            a = -1;

            b = matrix[0] + matrix[4] + matrix[8];

            c = -((matrix[4] * matrix[8]) + (matrix[0] * matrix[8]) + (matrix[0] * matrix[4]) - (matrix[5] * matrix[7]) - (matrix[1] * matrix[3]) - (matrix[2] * matrix[6]));

            int det = Determinant(matrix);

            resCharacter =  CubicEquation(a, b, c, det);

            return resCharacter;
        }

        //Нахождение корней кубического уравнения
        private double[] CubicEquation(int a, int b, int c, int d)
        {
            double[] res = new double[3];
            a = 2; b = -3; c = -3; d = 2;
            double Q, R, FI, S;
            Q = ((a * a) - 3 * b) / 9;
            R = ((2 * Math.Pow(a, 3)) - (9 * a * b) + (27 * c)) / 54;
            S = (Math.Pow(Q,3) - Math.Pow(R,2));
            if (S > 0) {
                FI = (1.0 / 3) * Math.Acos(R / Math.Sqrt(Math.Pow(Q, 3)));
                res[0] = -2 * Math.Sqrt(Q) * Math.Cos(FI) - (a / 3.0);
                res[1] = -2 * Math.Sqrt(Q) * Math.Cos(FI + ((2.0 / 3) * Math.PI)) - a / 3;
                res[2] = -2 * Math.Sqrt(Q) * Math.Cos(FI - ((2.0 / 3) * Math.PI)) - a / 3;
            } else if (S < 0) { FI = Math.Cosh(Math.Abs(R) / Math.Pow(Math.Abs(Q), 1.5)) / 3;
                res[0] = -2 * Math.Sign(R) * Math.Sqrt(Math.Abs(Q)) * Math.Sinh(FI) - a / 3;
                res[1] = res[0]; res[2] = res[0];
            } else { res[0] = -2 * Math.Pow(R, 1.5) - a / 3;
                res[1] = Math.Pow(R, 1.5) - a / 3;
                res[2] = res[1];
            }
            return res;
        }

        //Нахождение определителя матрицы
        private int Determinant(int[] mat)
        {
            int determinant;

            determinant = ((mat[0] * mat[4] * mat[8]) + (mat[6] * mat[1] * mat[5]) + (mat[2] * mat[3] * mat[7])
                - (mat[6] * mat[4] * mat[2]) - (mat[3] * mat[1] * mat[8]) - (mat[7] * mat[5] * mat[0]));

            return determinant;
        }

        //Вывод
        private void PrintResult(double[] res)
        {
            Thread.Sleep(500);
            X1.Text = Convert.ToString(res[0]);
            X2.Text = Convert.ToString(res[1]);
            X3.Text = Convert.ToString(res[2]);
            X4.Text = Convert.ToString(res[3]);
            X5.Text = Convert.ToString(res[4]);
            X6.Text = Convert.ToString(res[5]);
            X7.Text = Convert.ToString(res[6]);
            X8.Text = Convert.ToString(res[7]);
            X9.Text = Convert.ToString(res[8]);
        }
    }
}