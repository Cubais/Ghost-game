using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Collections;
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
  public struct MapPosition
  {
    int X { get; set; }
    int Y { get; set; }

    public MapPosition(int X, int Y)
    {
      this.X = X;
      this.Y = Y;
    }
  }

  public abstract class Entity
  {
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public int EntityID { get; set; }

    public abstract void Draw(Graphics g);

    public MapPosition GetPosition()
    {
      return new MapPosition(PositionX, PositionY);
    }
  }

  public partial class Game : Form
  {
    Player localPlayer;
    private static NetworkStream ServerStream;
    private Map map;

    public Game()
    {
      InitializeComponent();
    }

    public Game(NetworkStream serverStream, int playerID)
    {
      InitializeComponent();
      localPlayer = new Player(this, 0, 0, playerID);

      this.map = new Map(this);
      map.AddEntity(localPlayer);
      ServerStream = serverStream;

    }

    public void ReceiveMessage(string data)
    {
      var player = JsonConvert.DeserializeObject<Player>(data);

      map.UpdateEntity(player);

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
      SendMessage("{Off}");
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      return (!localPlayer.PlayerControl(keyData)) ? base.ProcessCmdKey(ref msg, keyData) : true;
    }

    private void GameTimer_Tick(object sender, EventArgs e)
    {
      map.DrawMap();
    }
  }

  public class Player : Entity
  {
    public int PlayerID { get; set; }

    Form form;
    
    private bool changePosition = true;


    public Player(Form form, int posX, int posY, int playerID)
    {
      this.form = form;
      this.PositionX = posX;
      this.PositionY = posY;
      PlayerID = playerID;
    }

    public override void Draw(Graphics g)
    {
      // Draw only if position has changed
      if (changePosition)
      {
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

  public class Map
  {
    Hashtable Entities = new Hashtable();
    Graphics g;

    public Map(Form form)
    {
      this.g = form.CreateGraphics();
    }

    public void DrawMap()
    {
      foreach (Entity entity in Entities.Values)
      {
        entity.Draw(g);
      }
    }

    public void AddEntity(Entity e)
    {
      Entities.Add(e.EntityID, e);
    }

    public void UpdateEntity(Entity e)
    {
      if (Entities.Contains(e.EntityID))
        Entities.Remove(e.EntityID);

      Entities.Add(e.EntityID, e);
    }

  }
}
