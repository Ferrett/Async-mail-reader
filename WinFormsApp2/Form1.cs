using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
//using System.Net.Mail;
using System.Globalization;
using System.Threading;
using MailKit.Net.Imap;
using MailKit;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        #region pass
        static string password = "Prikhod322";

        List<string> Bodies = new List<string>();
        List<string> Date = new List<string>();
        List<string> Author = new List<string>();
        IMailFolder inbox;
        #endregion
        public Form1()
        {
            InitializeComponent();
            //Get();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = Bodies.ElementAt(listBox1.SelectedIndex);
            textBox2.Text = Date.ElementAt(listBox1.SelectedIndex);
            textBox3.Text = Author.ElementAt(listBox1.SelectedIndex);
        }

        public void Get(int a)
        {
            using (ImapClient client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, true);
                client.Authenticate("vovkaprikhod@gmail.com", password);
                inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                tmpS=inbox.GetMessage(a).Subject;
                tmpT= inbox.GetMessage(a).TextBody;
                tmpF =inbox.GetMessage(a).From.ToString();
                tmpD= inbox.GetMessage(a).Date.ToString();
                i++;
            }
        }
        string tmpS;
        string tmpT;
        string tmpD;
        string tmpF;
        int i = 0;
        bool IsWorking = true;

        private async void button1_Click(object sender, EventArgs e)
        {
            Action action = () => {
                Bodies.Add(tmpT);
                Date.Add(tmpD);
                Author.Add(tmpF);
                listBox1.Items.Add(tmpS);
            };
            await Task.Run(() =>
            {
                while (true)
                {
                    Get(i);
                    Invoke(action);

                    if (IsWorking == false)
                    {
                        break;

                    }

                }
            });

            IsWorking = true;
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            IsWorking = false;
        }
    }
}
