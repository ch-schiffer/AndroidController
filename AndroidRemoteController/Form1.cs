using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndroidRemoteController
{
    public partial class Form1 : Form
    {
        private Dictionary<Keys, int> _keyDictionary = new Dictionary<Keys, int>()
        {
          {Keys.Tab, 61 },
          {Keys.Down, 20},
          {Keys.Up, 19},
          {Keys.Enter, 66},
          {Keys.Space, 3},
          {Keys.Back, 4},
          {Keys.Left, 21},
          {Keys.Right,22},
          {Keys.Escape, 26}
        };

        public Form1()
        {
            InitializeComponent();
            Screenshot();
        }

        public static int Counter
        {
            get { return _counter; }
            set { _counter = value; }
        }

        private void InputKeyEvent(int key)
        {
            string cmd = String.Format("shell input keyevent {0}", key);
            ExecuteCommand(cmd);
            Screenshot();
        }

        private void InputTap(int x, int y)
        {
            string cmd = String.Format("shell input tap {0} {1}", x, y);
            ExecuteCommand(cmd);
        }

        private void InputText(string text)
        {
            string cmd = String.Format("shell input text {0}", text);
            ExecuteCommand(cmd);
        }

        private void InputSwipe(int xBefore, int yBefore, int xAfter, int yAfter)
        {
            string cmd = String.Format("shell input swipe {0} {1} {2} {3}", xBefore, yBefore, xAfter, yAfter);
            ExecuteCommand(cmd);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = richTextBox1.Text;
            InputText(text);
            Screenshot();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            InputKeyEvent(26);
            InputSwipe(200, 900, 200, 300);
            string pin = textBox1.Text;
            InputText(pin);
            Screenshot();
        }

        private static int _counter = 0;
        private void Screenshot()
        {
            //if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
            //pictureBox1.Image = null;
            ////if (File.Exists("screen"+(_counter-1)+".png")) File.Delete("screen.png");
            //ExecuteCommand("shell screencap -p /sdcard/screen" + _counter + ".png");
            //ExecuteCommand("pull /sdcard/screen" + _counter + ".png");
            //ExecuteCommand("shell rm /sdcard/screen" + _counter + ".png");
            //if (File.Exists("screen" + _counter + ".png") && (new FileInfo("screen" + _counter + ".png").Length > 0))
            //{
            //    pictureBox1.WaitOnLoad = true;
            //    pictureBox1.LoadAsync("screen" + _counter + ".png");
            //}
            //_counter++;
        }

        private void ExecuteCommand(string cmd)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "adb.exe",
                Arguments = cmd
            };
            process.StartInfo = startInfo;
            Console.WriteLine("adb {0}", cmd);
            process.Start();
            process.WaitForExit();
        }

       
        private void button3_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (_keyDictionary.ContainsKey(e.KeyCode))

            {
                InputKeyEvent(_keyDictionary[e.KeyCode]);
                e.IsInputKey = true;
            }
                
        }
    }
}
