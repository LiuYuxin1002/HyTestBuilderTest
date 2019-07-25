using System;
using System.Windows.Forms;
using static LBSoft.IndustrialCtrls.Leds.LBLed;
using HyTestRTDataService.RunningMode;
using HyTestRTDataService;
using log4net;
using System.Diagnostics;
using System.Threading;

namespace HyTestBuilderTest
{
    public partial class FormTestRunning : Form
    {
        RunningServer server = RunningServer.getServer();

        ILog log = LogManager.GetLogger(typeof(FormTestRunning));

        bool[] state = new bool[8];

        public FormTestRunning()
        {
            InitializeComponent();
            server.Run();
        }

        #region DO
        private void button1_Click(object sender, EventArgs e)
        {
            state[0] = !state[0];
            server.InstantWrite("DO1", state[0]);
            log.Info("DO1已写入");
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            state[1] = !state[1];
            server.InstantWrite("DO2", state[1]);
            log.Info("DO2已写入");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            state[2] = !state[2];
            server.InstantWrite("DO3", state[2]);
            log.Info("DO3已写入");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            state[3] = !state[3];
            server.InstantWrite("DO4", state[3]);
            log.Info("DO4已写入");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            state[4] = !state[4];
            server.InstantWrite("DO5", state[4]);
            log.Info("DO5已写入");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            state[5] = !state[5];
            server.InstantWrite("DO6", state[5]);
            log.Info("DO6已写入");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            state[6] = !state[6];
            server.InstantWrite("DO7", state[6]);
            log.Info("DO7已写入");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            state[7] = !state[7];
            server.InstantWrite("DO8", state[7]);
            log.Info("DO8已写入");
        }

        #endregion

        #region AO

        //AO部分
        private void button9_Click(object sender, EventArgs e)
        {
            string valueStr = this.textBox1.Text.Trim();
            if(!string.IsNullOrEmpty(valueStr))
            {
                //还应该判断是不是非double
                server.InstantWrite("AO1", double.Parse(valueStr));
                log.Info("AO已写入");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string valueStr = this.textBox2.Text.Trim();
            server.InstantWrite("AO2", double.Parse(valueStr));
            log.Info("AO已写入");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string valueStr = this.textBox3.Text.Trim();
            if(!string.IsNullOrEmpty(valueStr))
            {
                server.InstantWrite("AO3", double.Parse(valueStr));
                log.Info("AO已写入");
            }
            else
            {
                log.Info("给定输入不符合规定");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string valueStr = this.textBox4.Text.Trim();
            server.InstantWrite("AO4", double.Parse(valueStr));
            log.Info("AO已写入");
        }

        #endregion
    }
}
