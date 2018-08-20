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
using System.Threading;

using GhostGamePlayer;

namespace GhostGameClient
{
  public partial class Form1 : Form
  {

    private string serverIP = "localhost";
    private int port = 8080;
    private NetworkStream serverStream = default(NetworkStream);
    private TcpClient client = new TcpClient();
    private string returnData;
    private int playerID;

    private bool gameStarted = false;
    Game newGame;

    public Form1()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e) //connect to server
    {
      client = new TcpClient(serverIP, port);

      int byteCount = Encoding.ASCII.GetByteCount(nameBox.Text);

      byte[] sendData = new byte[byteCount];

      sendData = Encoding.ASCII.GetBytes(nameBox.Text + '$');

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
        try
        {
          serverStream = client.GetStream();

          int buffSize = 0;

          byte[] inStream = new byte[10025];

          buffSize = client.ReceiveBufferSize;

          serverStream.Read(inStream, 0, 255);

          returnData = System.Text.Encoding.ASCII.GetString(inStream);

          if (returnData.IndexOf("@") != -1)
          {
            playerID = Int32.Parse(returnData.Substring(0, returnData.IndexOf('@')));
            continue;
          }

          MessageWrite();
        }
        catch
        {
          output.Text = "Server is not responding" + Environment.NewLine;
        }
      }
      
    }
    private void MessageWrite()
    {
      if (this.InvokeRequired)
        this.Invoke(new MethodInvoker(MessageWrite));
      else if(gameStarted)
      {
        newGame.ReceiveMessage(returnData);
      }
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void StartGame_Click(object sender, EventArgs e)
    {
      newGame = new GhostGamePlayer.Game(this.serverStream, playerID);
      gameStarted = true;
      newGame.Show();

    }

    private void Form1_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (serverStream != null)
        serverStream.Close();

      Application.Exit();
    }
  }
}
