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
        Boolean optrPressed = false, equalPressed = false, isDot = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonNumber_Click(object sender, EventArgs e)
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

        private double eval(Queue<string> inqueue)
        {
            Queue<string> outqueue = new Queue<string>();
            Stack<double> outstack = new Stack<double>();
            Stack<string> opstack = new Stack<string>();
            double n = 0;

            foreach (string s in inqueue)
            {
                if (Double.TryParse(s, out n))
                    outqueue.Enqueue(s);
                else
                {
                    if (opstack.Count == 0)
                        opstack.Push(s);
                    else
                    {
                        if (s == "+" || s == "-")
                        {
                            while (opstack.Count > 0)
                            {
                                outqueue.Enqueue(opstack.Peek());
                                opstack.Pop();
                            }
                            opstack.Push(s);
                        }

                        if (s == "*" || s == "/")
                        {
                            if (opstack.Peek() == "*" || opstack.Peek() == "/")
                            {
                                outqueue.Enqueue(opstack.Peek());
                                opstack.Pop();
                            }
                            opstack.Push(s);
                        }
                    }
                }
            }

            while (opstack.Count > 0)
            {
                outqueue.Enqueue(opstack.Peek());
                opstack.Pop();
            }

            //Console.Write("=> ");

            foreach (string s in outqueue)
            {
                //Console.Write(s + " ");

                if (Double.TryParse(s, out n))
                    outstack.Push(n);
                else
                {
                    double v2 = outstack.Peek();
                    outstack.Pop();
                    double v1 = outstack.Peek();
                    outstack.Pop();
                    if (s == "+")
                        outstack.Push(v1 + v2);
                    else if (s == "-")
                        outstack.Push(v1 - v2);
                    else if (s == "*")
                        outstack.Push(v1 * v2);
                    else if (s == "/")
                        outstack.Push(v1 / v2);
                }
            }
            return outstack.Peek();
        }

        private void buttonOperate_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (optrPressed)
                textBoxExpression.Text = textBoxExpression.Text.Substring
                    (0, textBoxExpression.Text.Length - 2) + button.Text + " ";
            else if (equalPressed)
            {
                textBoxExpression.Text = textBoxResult.Text + " " + button.Text + " ";
            }
            else
            {
                textBoxExpression.Text += textBoxResult.Text + " ";

                string[] expression = textBoxExpression.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                Queue<string> inqueue = new Queue<string>();

                foreach (string s in expression)
                {
                    inqueue.Enqueue(s);
                }

                textBoxResult.Text = eval(inqueue).ToString();
                textBoxExpression.Text += button.Text + " ";
            }

            equalPressed = false;
            optrPressed = true;
            isDot = false;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxResult.Text = "0";
            textBoxExpression.Text = "";
            optrPressed = false;
            equalPressed = false;
            isDot = false;
        }

        private void buttonEqual_Click(object sender, EventArgs e)
        {
            if (textBoxExpression.Text == "") textBoxExpression.Text = textBoxResult.Text + " " + "=" + " ";
            else
            {
                if (equalPressed)
                {
                    string[] expression = textBoxExpression.Text.Substring
                    (0, textBoxExpression.Text.Length - 2).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    Queue<string> inqueue = new Queue<string>();

                    foreach (string s in expression)
                    {
                        inqueue.Enqueue(s);
                    }

                    textBoxResult.Text = eval(inqueue).ToString();
                    textBoxExpression.Text = textBoxResult.Text + " " + expression[1] + " " + expression[2] + " " + "=" + " ";//1+5*2=會錯
                }
                else
                {
                    textBoxExpression.Text += textBoxResult.Text;

                    string[] expression = textBoxExpression.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    Queue<string> inqueue = new Queue<string>();

                    foreach (string s in expression)
                    {
                        inqueue.Enqueue(s);
                    }
                    textBoxResult.Text = eval(inqueue).ToString();
                    textBoxExpression.Text += " " + "=" + " ";
                }
            }
            equalPressed = true;
        }

        private void buttonBackspace_Click(object sender, EventArgs e)
        {
            if (textBoxResult.Text.Length == 1)
                textBoxResult.Text = "0";
            else
                textBoxResult.Text = textBoxResult.Text.Substring(0, textBoxResult.Text.Length - 1);

            optrPressed = false;
            equalPressed = false;
        }

        private void buttonDot_Click(object sender, EventArgs e)
        {
            if (isDot == false && equalPressed == false)
                textBoxResult.Text += '.';
            isDot = true;
        }

        private void buttonClearError_Click(object sender, EventArgs e)
        {
            while (textBoxResult.Text.Length != 1)
                textBoxResult.Text = textBoxResult.Text.Substring(0, textBoxResult.Text.Length - 1);
            textBoxResult.Text = "0";
            isDot = false;
        }

        private void buttonSign_Click(object sender, EventArgs e)
        {
            int sign = Convert.ToInt32(textBoxResult.Text);
            textBoxResult.Text = (-1 * sign).ToString();
        }
    }
}
