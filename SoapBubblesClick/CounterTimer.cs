using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoapBubblesClick
{
    //CounterTimerのクラス
    internal class CounterTimer
    {
        private Timer timer;
        private Counter counter;
        private Label timerLabel;

        public CounterTimer(Label label, Timer timerInstance)
        {
            timer = timerInstance;
            timerLabel = label;

            timer.Interval = 1000;
            timer.Tick -= (sender, e) =>
            {
                timerLabel.Text = ToString();
            };
        }
    }
}
