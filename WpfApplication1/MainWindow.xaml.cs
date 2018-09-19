using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApplication1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        double First_number = 0.0; //переменная для запоминания результатов
        int oper = 0; //переменная для запоминания операций

        private void button_Click(object sender, RoutedEventArgs e) //запись в textbox цыфры с кнопки
        {
            textBox.Text += (sender as Button).Content;
        }

        private void button_division_Click(object sender, RoutedEventArgs e) //деление
        {
            try
            {
                if (oper != 0)
                    equall();
                oper = 4;
                textBox.Text = "";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка деления");
            }
        }

        private void button_multiply_Click(object sender, RoutedEventArgs e) //умножение
        {
            if (oper != 0)
                equall();
            oper = 3;
            textBox.Text = "";
        }

        private void button_minus_Click(object sender, RoutedEventArgs e) //вычиитание
        {
            if (oper != 0)
                equall();
            oper = 2;
            textBox.Text = "";
        }

        private void button_plus_Click(object sender, RoutedEventArgs e) //сложение
        {
            if (oper != 0)
                equall();
            oper = 1;
            textBox.Text = "";
        }

        private void button_insert_Click(object sender, RoutedEventArgs e) //равно
        {
            equall();
            oper = 0;
            textBox.Text = Convert.ToString(First_number);
        }

        private void button_Clear_Click(object sender, RoutedEventArgs e) //reset
        {
            textBox.Text = "";
            First_number = 0.0;
            oper = 0;
        }

        private bool checker() //проверка строки
        {
            string s = textBox.Text;
            s.Replace(' ', ',');
            s.Replace('.', ',');
            return String.IsNullOrWhiteSpace(s);
        }
        private void equall() //выполнение арефмитических операций
        {
            try
            {
                Console.WriteLine(First_number);
                double tmp_number = 0;
                if (checker() == false)
                    tmp_number = Convert.ToDouble(textBox.Text);
                switch(oper)
                {
                    case 1:
                        {
                            First_number += tmp_number;
                        }; break;
                    case 2:
                        {
                            First_number -= tmp_number;
                        }; break;
                    case 3:
                        {
                            First_number *= tmp_number;
                        }; break;
                    case 4:
                        {
                            First_number /= tmp_number;
                        }; break;
                    default:
                        {
                            MessageBox.Show("Ошибка");
                        }; break;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка ввода");
            }
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e) //вызов операций с клавиатуры
        {
            switch (e.Key)
            {
                case Key.Divide:
                    {
                        button_division.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    }; break;
                case Key.Return:
                    {
                        button_enter.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    }; break;
            }
        }

        private void button_enter_Click(object sender, RoutedEventArgs e)//калькулятор в строку
        {
            string str = textBox.Text;
            List<double> mas = new List<double>();
            List<char> znak = new List<char>();
            double answer = 0.0;
            int Index = 0;
            while(str.Contains("+") || str.Contains("-") || str.Contains("*") || str.Contains("/"))
            {
                if (str[Index] == '+' || str[Index] == '-' || str[Index] == '*' || str[Index] == '/')
                {
                    if (str[Index + 1] == '+' || str[Index + 1] == '-' || str[Index + 1] == '*' || str[Index + 1] == '/')//исключение повторений
                    {
                        MessageBox.Show("Ошибка формата");
                        break;
                    }
                    mas.Add(double.Parse(str.Substring(0, Index))); //запомнание числа
                    znak.Add(str[Index]); //запомнание знака
                    str = str.Remove(0, Index + 1);
                    Index = 0;
                }
                Index++;
            }
            mas.Add(double.Parse(str));
            //подсчет результата
            //!без учета порядка операций
            switch(znak[0])
            {
                case '+': answer = mas[0] + mas[1]; break;
                case '-': answer = mas[0] - mas[1]; break;
                case '*': answer = mas[0] * mas[1]; break;
                case '/': answer = mas[0] / mas[1]; break;
            }
            for (int i = 1; i < znak.Count; i++)
            {
                switch (znak[i])
                {
                    case '+': answer += mas[i+1]; break;
                    case '-': answer -= mas[i+1]; break;
                    case '*': answer *= mas[i+1]; break;
                    case '/': answer /= mas[i+1]; break;
                }
                Console.WriteLine("CH" + answer);
            }
            textBox.Text = answer.ToString();
        }
    }
}
//Никулин С. Малай Д. П2-15