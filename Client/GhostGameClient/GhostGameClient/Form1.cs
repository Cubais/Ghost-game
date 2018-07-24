using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace GhostGameClient
{
  public partial class Form1 : Form
  {

    private string serverIP = "localhost";
    private int port = 8080;

    public Form1()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      TcpClient client = new TcpClient(serverIP, port);

      int byteCount = Encoding.ASCII.GetByteCount(nameBox.Text);

      byte[] sendData = new byte[byteCount];

      sendData = Encoding.ASCII.GetBytes(nameBox.Text);

      NetworkStream stream = client.GetStream();

      stream.Write(sendData, 0, sendData.Length);

      stream.Close();
      client.Close();



    }
  }
}
