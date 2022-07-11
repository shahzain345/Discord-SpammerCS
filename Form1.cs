using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Leaf.xNet;
using System.Threading;
namespace Discord_Spammer
{
    public partial class Form1 : Form
    {
        public static List<string> WorkingTokens = new List<string>();
        public Form1()
        {
            InitializeComponent();
            try
            {
                string path = "tokens.txt";
                string[] tokens = File.ReadAllLines(path);
                label2.Text = $"Token Count: {tokens.Length}";
            }
            catch (Exception _)
            {
                MessageBox.Show("Tokens file not found");
            }
        }
        #region Check Token
        public static async Task<string> Check(string token)
        {
            try
            {
                var utility = new Utils();
                HttpRequest r = new HttpRequest();
                utility.headers(r, token); // add all headers
                r.Get("https://discord.com/api/v9/users/@me/guild-events");
                r.Dispose();
                return token;
            } catch(Exception x)
            {
                if (x.Message.Contains("403") || x.Message.Contains("401"))
                {
                    return null;
                    //return ;
                }
            }
            return null;
        }
        async void CheckAll()
        {
            try
            {
                WorkingTokens.Clear();
                string[] tokens = File.ReadAllLines("tokens.txt");
                foreach (string token in tokens)
                {
                    string hlo = await Check(token);
                    if (hlo == token)
                    {
                        WorkingTokens.Add(token);
                    }
                }
                string[] newTokens = WorkingTokens.ToArray();
                File.WriteAllLines("tokens.txt", newTokens);
                MessageBox.Show($"Token check complete! You have {newTokens.Length} working tokens out of {tokens.Length} total tokens!");
            }
            catch (Exception x)
            {
                MessageBox.Show("Error while checking tokens");
            }
        }
        #endregion

        private async void button4_Click(object sender, EventArgs e)
        {
            Checker_status.Text = "Checking...";
            Thread t = new Thread(new ThreadStart(CheckAll));
            t.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var spmr = new Spammer();
            spmr.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
