using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, MyEventArgs e)
        {
            textBox1.Text = "Hello";
            MyEventArgs.Method();
            e.func();
        }


    }

    public class MyEventArgs : EventArgs
    {
        public int num { get; set; }

        public static void Method()
        {

        }
        public void func()
        {

        }
    }

}
