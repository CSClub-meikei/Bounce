using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bounce.socket;
namespace Bounce
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            Client.tcp.received += Tcp_received;
        }

        private void Tcp_received(object sender, packetData e)
        {
           // MessageBox.Show(e.data);
            string[] sp = e.data.Split(',');
            if(sp[0] == "success")
            {
                userData.userName = sp[1];
                Client.tcp.send(new packetData(Client.tcp.id, userData.userid.ToString(), @const.request, "bounce,getSaveData"));


                Invoke((MethodInvoker)delegate
                {
                    Close();
                    Dispose();
                });
            }
            else
            {
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Client.tcp.send(new packetData(Client.tcp.id, textBox1.Text, @const.request, "login,"+textBox1.Text + "," + textBox2.Text +",bounce"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Client.tcp.disConnect();
            Client.tcp = null;
        }
    }
}
