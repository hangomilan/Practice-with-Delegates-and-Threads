using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticeWithDelegatesAndThreads
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        

        private void TimeConsumingMethod(object Time)
        {
            int seconds;
            seconds = (int)Time;

            for (int j = 1; j <= seconds; j++)
            {
                System.Threading.Thread.Sleep(1000);
                SetProgress((int)(j * 100) / seconds);
                if (Cancel)
                    break;

            }
            if (Cancel)
            {
                MessageBox.Show("Cancelled");
                Cancel = false;
            }
            else
            {
                MessageBox.Show("Complete");
            }

        }
        private delegate void TimeConsumingMethodDelegate(int seconds);

        public delegate void SetProgressDelegate(int val);

        public void SetProgress(int val)
        {
            if (progressBar1.InvokeRequired)
            {
                SetProgressDelegate del = new SetProgressDelegate(SetProgress);
                this.Invoke(del, new object[] { val });
            }
            else
            {
                progressBar1.Value = val;
            }
        }

        bool Cancel;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread aThread = new 
            System.Threading.Thread(TimeConsumingMethod);
            aThread.Start(int.Parse(textBox1.Text));

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cancel = true;
        }
    }
}
