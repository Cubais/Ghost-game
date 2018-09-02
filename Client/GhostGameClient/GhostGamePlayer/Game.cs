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

  public partial class Game : Form
  {
    Player localPlayer;
    private static NetworkStream ServerStream;
    private Map map;
    string previousData = "";
    
    public Game()
    {
      InitializeComponent();
    }

    public Game(NetworkStream serverStream, int playerID)
    {
      InitializeComponent();
      localPlayer = new Player(this, 50, 50, playerID);
      this.map = new Map(this, localPlayer);
      ServerStream = serverStream;
      this.Text = playerID.ToString();      

    }

    public void ReceiveMessage(string data)
    {
      if (previousData == data)
        return;

      previousData = data;
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
      //this.TopMost = true;
      GameTimer.Enabled = true;
      //this.FormBorderStyle = FormBorderStyle.None;
      //this.WindowState = FormWindowState.Maximized;


    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData == Keys.Escape)
      {
        this.Close();
        SendMessage("{Off}");
      }
        

      return (!localPlayer.PlayerControl(keyData)) ? base.ProcessCmdKey(ref msg, keyData) : true;
    }

    private void GameTimer_Tick(object sender, EventArgs e)
    {      
      map.DrawMap();
    }

    private void Game_Resize(object sender, EventArgs e)
    {
     // map.OnResize(this.ClientSize.Width, this.ClientSize.Height);
    }
  }

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

    public abstract void Draw(Graphics g, Color color);
    public abstract void UpdatePosition(int X, int Y);

    public MapPosition GetPosition()
    {
      return new MapPosition(PositionX, PositionY);
    }
  }

  public class Player : Entity
  {
    Form form;
    delegate void StatusChanged();
    StatusChanged changedLocalPLayer;
    
    private bool changePosition = true;

    Bitmap icon = new Bitmap("ghost.png");


    public Player(Form form, int posX, int posY, int playerID)
    {
      this.form = form;
      this.PositionX = posX;
      this.PositionY = posY;
      EntityID = playerID;
    }

    public void RegisterOnLocalPlayerChange(Map map)
    {
      changedLocalPLayer += map.ChangeStatus;
    }

    public override void Draw(Graphics g, Color color)
    {           
      g.DrawRectangle(new Pen(color), new Rectangle(PositionX, PositionY, 50, 50));
    }

    public override void UpdatePosition(int X, int Y)
    {
      PositionX = X;
      PositionY = Y;
      changePosition = true;
    }

    public bool PlayerControl(Keys keyData)
    {
      switch (keyData)
      {
        case Keys.Left:
          if (PositionX > 50)
          {
            PositionX -= 10;
            changePosition = true;
          }
          else
          {
            changePosition = false;
          }
          break;

        case Keys.Up:
          if (PositionY > 45)
          {
            PositionY -= 10;
            changePosition = true;
          }
          else
          {
            changePosition = false;
          }
          break;

        case Keys.Right:
          if (PositionX + 50 < (form.ClientSize.Width - 50))
          {
            PositionX += 10;
            changePosition = true;
          }
          else
          {
            changePosition = false;
          }
          break;

        case Keys.Down:
          if (PositionY + 50 < (form.ClientSize.Height - 45))
          {
            PositionY += 10;
            changePosition = true;
          }
          else
          {
            changePosition = false;
          }
          break;
        case Keys.Escape:
                  
        default:
          changePosition = false;
          break;

      }
      if (changedLocalPLayer != null)
        changedLocalPLayer.Invoke();

      if (changePosition)
        SendChange();

      return changePosition;
    }

    private void SendChange()
    {
      Game.SendMessage(JsonConvert.SerializeObject(this, Formatting.Indented));
    }


  }

  public class Map
  {
    Hashtable Entities = new Hashtable();
    Graphics g;
    Player localPlayer;
    List<Bitmap> icons = new List<Bitmap>();
    Bitmap map = new Bitmap("map_final.png");

    bool stateChanged = true;

    int canvasWidth;
    int canvasHeight;

    public Map(Form form, Player localPlayer)
    {
      this.g = form.CreateGraphics();
      this.localPlayer = localPlayer;
      localPlayer.RegisterOnLocalPlayerChange(this);
      canvasHeight = form.ClientSize.Height;
      canvasWidth = form.ClientSize.Width;
      LoadMap();            
    }

    public void ChangeStatus()
    {
      stateChanged = true;
    }

    public void DrawMap()
    {
      if (stateChanged)
      {        
        stateChanged = false;
        g.DrawImage(map, new Rectangle(0, 0, canvasWidth, canvasHeight));  
        
      }    

      localPlayer.Draw(g, Color.Blue);

      foreach (Entity entity in Entities.Values)
      {
        entity.Draw(g, Color.Red);
      }      
    }

    public void AddEntity(Entity e)
    {
      Entities.Add(e.EntityID, e);
    }

    public void UpdateEntity(Entity e)
    {
      if (e.EntityID == localPlayer.EntityID)
        return;

      if (Entities.Contains(e.EntityID))
      {
        ((Entity)Entities[e.EntityID]).UpdatePosition(e.PositionX, e.PositionY);
        stateChanged = true;
      }
      else
      {
        Entities.Add(e.EntityID, e);
      }
    }

    private void LoadMap()
    {
      icons.Add(new Bitmap("grass.jpg"));
      icons.Add(new Bitmap("wall.png"));
            
    }

    public void OnResize(int newWidth, int newHeight)
    {
      canvasWidth = newWidth;
      canvasHeight = newHeight;
    }
  }
}
