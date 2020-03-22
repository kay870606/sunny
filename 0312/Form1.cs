using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0312
{
    public partial class Form1 : Form
    {

        double operand = 0, lastOperand = 0;
        Boolean optrPressed = false, equalPressed = false;
        int lastOperator = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (optrPressed || textBoxResult.Text == "0")
                textBoxResult.Text = button.Text;
            else if (equalPressed)
            {
                textBoxExpression.Text = "";
                textBoxResult.Text = button.Text;
            }
            else
                textBoxResult.Text += button.Text;

            equalPressed = false;
            optrPressed = false;
        }

        private void button_Operate_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (optrPressed)
                textBoxExpression.Text = textBoxExpression.Text.Substring
                    (0, textBoxExpression.Text.Length - 1) + button.Text;
            else if (equalPressed)
            {
                textBoxExpression.Text = textBoxResult.Text + button.Text;

            }
            else
            {
                textBoxExpression.Text += textBoxResult.Text + button.Text;

                if (lastOperator == 0)
                {
                    textBoxResult.Text = (operand + Convert.ToDouble(textBoxResult.Text)).ToString();
                }
                else if (lastOperator == 1)
                {
                    textBoxResult.Text = (operand - Convert.ToDouble(textBoxResult.Text)).ToString();
                }
                else if (lastOperator == 2)
                {
                    textBoxResult.Text = (operand * Convert.ToDouble(textBoxResult.Text)).ToString();
                }
            }

            if (button.Text == "+")
                lastOperator = 0;
            else if (button.Text == "-")
                lastOperator = 1;
            else if (button.Text == "*")
                lastOperator = 2;

            operand = Convert.ToDouble(textBoxResult.Text);

            equalPressed = false;
            optrPressed = true;
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            textBoxResult.Text = "0";
            textBoxExpression.Text = "";
            operand = 0;
            lastOperand = 0;
            lastOperator = 0;
            optrPressed = false;
            equalPressed = false;
        }

        private void button_Equal_Click(object sender, EventArgs e)
        {
            if (textBoxExpression.Text == "") textBoxExpression.Text = textBoxResult.Text + "=";
            else
            {
                if (equalPressed)
                {
                    operand = Convert.ToDouble(textBoxResult.Text);
                    if (lastOperator == 0)
                    {
                        textBoxExpression.Text = textBoxResult.Text + "+" + lastOperand + "=";
                        textBoxResult.Text = (lastOperand + Convert.ToDouble(textBoxResult.Text)).ToString();
                    }
                    else if (lastOperator == 1)
                    {
                        textBoxExpression.Text = textBoxResult.Text + "-" + lastOperand + "=";
                        textBoxResult.Text = (lastOperand - Convert.ToDouble(textBoxResult.Text)).ToString();
                    }
                    else if (lastOperator == 2)
                    {
                        textBoxExpression.Text = textBoxResult.Text + "*" + lastOperand + "=";
                        textBoxResult.Text = (lastOperand * Convert.ToDouble(textBoxResult.Text)).ToString();
                    }
                }
                else
                {
                    textBoxExpression.Text += textBoxResult.Text + "=";
                    lastOperand = Convert.ToDouble(textBoxResult.Text);

                    if (lastOperator == 0)
                    {
                        textBoxResult.Text = (operand + Convert.ToDouble(textBoxResult.Text)).ToString();
                    }
                    else if (lastOperator == 1)
                    {
                        textBoxResult.Text = (operand - Convert.ToDouble(textBoxResult.Text)).ToString();
                    }
                    else if (lastOperator == 2)
                    {
                        textBoxResult.Text = (operand * Convert.ToDouble(textBoxResult.Text)).ToString();
                    }
                }
            }
            equalPressed = true;
        }

        private void button_Backspace_Click(object sender, EventArgs e)
        {
            if (textBoxResult.Text.Length == 1)
                textBoxResult.Text = "0";
            else
                textBoxResult.Text = textBoxResult.Text.Substring(0, textBoxResult.Text.Length - 1);

            optrPressed = false;
            equalPressed = false;
        }

        private void button_ClearError_Click(object sender, EventArgs e)
        {
            while (textBoxResult.Text.Length != 1)
                textBoxResult.Text = textBoxResult.Text.Substring(0, textBoxResult.Text.Length - 1);
            textBoxResult.Text = "0";
        }

        private void button_Sign_Click(object sender, EventArgs e)
        {
            int sign = Convert.ToInt32(textBoxResult.Text);
            textBoxResult.Text = (-1 * sign).ToString();
        }
    }
}
