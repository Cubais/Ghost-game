﻿using System;
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
using System.Threading;

namespace GhostGameClient
{
  public partial class Form1 : Form
  {

    private string serverIP = "localhost";
    private int port = 8080;
    private NetworkStream serverStream = default(NetworkStream);
    private TcpClient client = new TcpClient();
    private string returnData;

    public Form1()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      client = new TcpClient(serverIP, port);

      int byteCount = Encoding.ASCII.GetByteCount(nameBox.Text);

      byte[] sendData = new byte[byteCount];

      sendData = Encoding.ASCII.GetBytes(nameBox.Text);

      serverStream = client.GetStream();

      serverStream.Write(sendData, 0, sendData.Length);

      serverStream.Flush();

      Thread clientThread = new Thread(getMessage);
      clientThread.Start();



    }
    private void getMessage()
    {
      while (true)

      {

        serverStream = client.GetStream();

        int buffSize = 0;

        byte[] inStream = new byte[10025];

        buffSize = client.ReceiveBufferSize;

        serverStream.Read(inStream, 0, 255);

        returnData = System.Text.Encoding.ASCII.GetString(inStream);

        messageWrite();
      }
      
    }
    private void messageWrite()
    {

      if (this.InvokeRequired)
        this.Invoke(new MethodInvoker(messageWrite));
      else
        output.Text = output.Text + Environment.NewLine + " >> " + returnData;

    }
  }
}
