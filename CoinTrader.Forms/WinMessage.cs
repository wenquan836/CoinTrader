using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinTrader.Forms
{
    public enum MessageType
    {
        Success = 0,
        Alert = 1,
        Error = 2
    }


   
  
    public partial class WinMessage : Form
    {

        Color[] stateColors = { Color.Green, Color.Yellow, Color.Red };
        string stateChars = "✔¡✖";
        private enum State
        {
            Openning = 0,
            Showing = 1,
            Closing = 2
        }

        private State state = State.Openning;

        int opacityTime = 250;
        int showTime = 3000;
        int showMs = 0;
        public WinMessage()
        {
            InitializeComponent();
        }

        private void ShowMessage(MessageType type, string message)
        {
            this.label1.Text = message;
            this.state = State.Openning;

            this.lblImage.Text = stateChars[(int)type].ToString();
            this.lblImage.ForeColor = stateColors[(int)type];

            this.label1.Left = this.lblImage.Right + 15;
        }


        private static WinMessage _instance = null;
        public static void Show(MessageType type, string message)
        {
            if(_instance == null)
            {
                var win = new WinMessage();
                _instance = win;
                win.Show();
            }

            _instance.ShowMessage(type, message);
        }

        private void WinMessage_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(_instance == this)
            {
                _instance = null;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(state ==  State.Openning)
            {
                if (Opacity < 1)
                {
                    Opacity += 1.0f / opacityTime * timer1.Interval;
                }
                else
                {
                    state = State.Showing;
                    showMs = 0;
                }
            }
            else if(state ==  State.Showing)
            {
                if(showMs >= showTime)
                {
                    state = State.Closing;
                }

                showMs += timer1.Interval;
            }
            else if(state == State.Closing)
            {

                if((Opacity > 0))
                {
                    Opacity  = Math.Max(0, Opacity - 1.0f / opacityTime * timer1.Interval);
                }
                else
                {
                    this.Close();
                }
            }
        }
    }

   
}
