using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        private Double number1;
        private Double number2;
        private string operation;
        private int option = 1;
        private bool solved = false;
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.textBox1.MaxLength = 7;
            this.KeyPress += new KeyPressEventHandler(handler);
            button15.Click += buttonCustomClick;
            button9.Click += buttonCustomClick;
            button8.Click += buttonCustomClick;
            button7.Click += buttonCustomClick;
            button6.Click += buttonCustomClick;
            button5.Click += buttonCustomClick;
            button4.Click += buttonCustomClick;
            button3.Click += buttonCustomClick;
            button2.Click += buttonCustomClick;
            button1.Click += buttonCustomClick;
            button11.Click += buttonSaveTempValue;
            button12.Click += buttonSaveTempValue;
            button13.Click += buttonSaveTempValue;
            button14.Click += buttonSaveTempValue;
            button18.Click += buttonCommaButtonClick;
            button10.Click += Calc;
        }

        private void buttonCommaButtonClick(object sender, EventArgs args)
        {
            if (textBox1.Text.Contains('.')) return;
            if (textBox1.Text.Length == 0 ||(textBox1.Text.Length == 1 && textBox1.Text=="-"))
                textBox1.Text += "0.";
            else
                textBox1.Text += '.';
        }

        private void buttonCustomClick(object sender, EventArgs args)
        {
            if (sender != null)
            {
                if (solved)
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    solved = false;
                }
                var button = sender as Button;
                if (textBox1.Text == "0" && textBox1.Text != null)
                    textBox1.Text = button.Text;
                else
                    textBox1.Text = textBox1.Text + button.Text;
            }
        }

        private void buttonSaveTempValue(object sender, EventArgs args)
        {
            if (textBox1.Text.Length > 0 && this.option==1)
            {
                var button = sender as Button;
                number1 = Convert.ToDouble(textBox1.Text);
                textBox2.Text = textBox1.Text + button.Text;
                operation = button.Text;
                textBox1.Text = "";
                option = 2;
            }
            else if (option == 2 && !solved)
            {
                var button = sender as Button;
                number2 = Convert.ToDouble(textBox1.Text);
                double result = calcResult();
                textBox2.Text = result.ToString() + button.Text;
                number1 = result;
                operation = button.Text;
                textBox1.Text = "";
            }
        }


        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            solved = false;
            option = 1;
        }


        private double calcResult()
        {
            switch (operation)
            {
                case "+":
                    return number1 + number2;
                case "-":
                    return number1 - number2;
                case "*":
                    return number1 * number2;
                default:
                    return number1 / number2;
                   
            }
        }

        private void Calc(object sender, EventArgs e)
        {
            if (!solved)
            {
                number2 = Convert.ToDouble(textBox1.Text);
                textBox2.Text += textBox1.Text + "=";
                double result = calcResult();
                option = 2;
                solved = true;
                textBox1.Text = result.ToString();
            }
        }

        private void customHandler(object sender, KeyPressEventArgs e)
        {
        }

        private void handler(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && textBox1.Text.Length > 0)
            {
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
                return;
            }
            // MessageBox.Show(char.GetNumericValue(e.KeyChar).ToString());
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
            {
                textBox1.Text += e.KeyChar;
                return;
            }

            var ops = new List<char> {'+', '-', '*', '/'};

            if (ops.Contains(e.KeyChar) && textBox1.Text.Length>0)
            {
/*                var button = sender as Button;
                e.Handled = true;
                number1 = Convert.ToDouble(textBox1.Text);
                textBox2.Text = textBox1.Text + e.KeyChar.ToString();
                operation = e.KeyChar.ToString();
                textBox1.Text = "";

*/
                if (textBox1.Text.Length > 0 && this.option == 1)
                {
                    var button = sender as Button;
                    number1 = Convert.ToDouble(textBox1.Text);
                    textBox2.Text = textBox1.Text + e.KeyChar.ToString();
                    operation = e.KeyChar.ToString();
                    textBox1.Text = "";
                    option = 2;
                }
                else if (option == 2 && !solved)
                {
                    var button = sender as Button;
                    number2 = Convert.ToDouble(textBox1.Text);
                    double result = calcResult();
                    textBox2.Text = result.ToString() + e.KeyChar.ToString();
                    number1 = result;
                    operation = e.KeyChar.ToString();
                    textBox1.Text = "";
                }

            }
            

            if (e.KeyChar == 10)
            {
                if (!solved)
                {
                    number2 = Convert.ToDouble(textBox1.Text);
                    textBox2.Text += textBox1.Text + "=";
                    double result = calcResult();
                    option = 2;
                    solved = true;
                    textBox1.Text = result.ToString();
                }
            }

            if (e.KeyChar != '.') return;
            e.Handled = true;
            if (textBox1.Text.Contains('.')) return;
            if (textBox1.Text.Length == 0 || (textBox1.Text.Length == 1 && textBox1.Text == "-"))
                textBox1.Text += "0.";
            else
                textBox1.Text += '.';
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0 && textBox1.Text!="-")
            {
                if (Convert.ToDouble(textBox1.Text) > 0)
                    textBox1.Text = '-' + textBox1.Text;
                else if (Convert.ToDouble(textBox1.Text) < 0)
                    textBox1.Text = textBox1.Text.Substring(1);
            }else
            {
                if (textBox1.Text.Length == 0)
                    textBox1.Text = "-";
                else
                    textBox1.Text = "";
            }
        }
    }
}