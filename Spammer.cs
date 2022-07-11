using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using Leaf.xNet;
namespace Discord_Spammer
{
    public partial class Spammer : Form
    {
        public static bool warmedup;
        public Spammer()
        {
            InitializeComponent();
            string[] tokens = File.ReadAllLines("tokens.txt");
            label2.Text += tokens.Length;
            new Thread(new ThreadStart(OnlineToken)).Start();
        }
        public static List<Thread> WorkingThreads = new List<Thread>();
        public static Random random = new Random(Guid.NewGuid().GetHashCode());
        void OnlineToken()
        {
            if (!warmedup)
            {
                var socket = new DiscordSocket();
                string[] tokens = File.ReadAllLines("tokens.txt");
                foreach (string token in tokens)
                {
                    socket.OnlineToken(token);
                    Console.WriteLine($"Online {token}");
                }
                warmedup = true;
            } else
            {
                Console.WriteLine("Already warmed up");
            }
        }
        void Spam()
        {
            
            string[] tokens = File.ReadAllLines("tokens.txt");
            if (checkBox1.Checked)
            {
                while (true)
                {
                    try
                    {
                        var utility = new Utils();
                        string token = tokens[random.Next(tokens.Length)];
                        string[] mbrs = File.ReadAllLines("members.txt");
                        var prms = new RequestParams();
                        prms["content"] = buildContent(textBox3.Text, mbrs, 30);
                        prms["tts"] = false;
                        prms["nonce"] = utility.RandomNonce(18);
                        HttpRequest r = new HttpRequest();
                        utility.headers(r, token);
                        r.Post($"https://discord.com/api/v9/channels/{textBox1.Text}/messages", prms);
                    } catch(Exception _)
                    {
                        Console.WriteLine(_);
                    }
                }
            } else
            {
                while (true)
                {
                    try
                    {
                        var utility = new Utils();
                        string token = tokens[random.Next(tokens.Length)];
                        string[] mbrs = File.ReadAllLines("members.txt");
                        var prms = new RequestParams();
                        prms["content"] = textBox3.Text;
                        prms["tts"] = false;
                        prms["nonce"] = utility.RandomNonce(18);
                        HttpRequest r = new HttpRequest();
                        utility.headers(r, token);
                        r.Post($"https://discord.com/api/v9/channels/{textBox1.Text}/messages", prms);
                    } catch (Exception _x)
                    {
                        Console.WriteLine(_x);
                    }
                }
            }
        }
        public string buildContent(string msg, string[] mbrs, int len)
        {
            msg += "\n";
            for(int i = 0; i<len; ++i)
            {
                string randmbr = mbrs[random.Next(mbrs.Length)];
                msg += $"<@{randmbr}>";
            }
            return msg;
        }
        private void strt_btn_Click(object sender, EventArgs e)
        {
            if (strt_btn.Text == "Stop spamming")
            {
                var thrds = WorkingThreads.ToArray();
                foreach (Thread thread in thrds)
                {
                    thread.Abort();
                    Console.WriteLine("Thread aborted");
                }
            }
            string[] mbrs = File.ReadAllLines("members.txt");
            if (checkBox1.Checked && mbrs.Length == 0)
            {
                MessageBox.Show("Please run the scrape.py file before spamming with mass mention on.");
            } else
            {
                WorkingThreads.Clear();
                strt_btn.Text = "Stop spamming";
                for (int i = 0; i<30; ++i)
                {
                    Thread t = new Thread(new ThreadStart(Spam));
                    t.Start();
                    WorkingThreads.Add(t);
                }
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
