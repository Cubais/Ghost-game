using System;
using System.IO;
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

using Newtonsoft.Json;

namespace GhostGamePlayer
{
  
  public partial class Game : Form
  {
    Player player;
    private static NetworkStream ServerStream;

    public Game()
    {
      InitializeComponent();
    }

    public Game(NetworkStream serverStream)
    {
      InitializeComponent();
      player = new Player(this, 0, 0);
      ServerStream = serverStream;
      
    }

    public static void SendMessage(string data)
    {
      byte[] outStream = System.Text.Encoding.ASCII.GetBytes(data);
      ServerStream.Write(outStream, 0, outStream.Length);
      ServerStream.Flush();
    }

    private void Game_Load(object sender, EventArgs e)
    {
      this.TopMost = true;
      GameTimer.Enabled = true;
      //this.FormBorderStyle = FormBorderStyle.None;
      //this.WindowState = FormWindowState.Maximized;
      
    }

    private void button1_Click(object sender, EventArgs e)
    {
      //File.Create("output.txt");
      //File.WriteAllText("output.txt", JsonConvert.SerializeObject(player, Formatting.Indented));
      this.Close();
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      return (!player.PlayerControl(keyData)) ? base.ProcessCmdKey(ref msg, keyData) : true;
    }

    private void GameTimer_Tick(object sender, EventArgs e)
    {
      player.Draw();    
      
    }    
  }

  public class Player
  {
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public int PlayerID { get; set; }
    
    Form form;    
    Graphics g;

    private bool changePosition = true;


    public Player(Form form,int posX,int posY)
    {     
      this.form = form;
      this.PositionX = posX;
      this.PositionY = posY;
      g = form.CreateGraphics();
    }

    public void Draw()
    {
      if (changePosition)    {
        g.Clear(Color.White);
        g.DrawRectangle(new Pen(Color.Red), new Rectangle(PositionX, PositionY, 50, 50));
        changePosition = false;

        Game.SendMessage(JsonConvert.SerializeObject(this, Formatting.Indented));      

      }
    }

    public bool PlayerControl(Keys keyData)
    {
      switch (keyData)
      {
        case Keys.Left:         
            PositionX -= 5;
            changePosition = true;
          return true;

        case Keys.Up:          
            PositionY -= 5;
            changePosition = true;
          return true;

        case Keys.Right:          
            PositionX += 5;
            changePosition = true;
          return true;

        case Keys.Down:          
            PositionY += 5;
            changePosition = true;
          return true;

        default:
          changePosition = false;
          return false;

      }
    }

    
  }
}
