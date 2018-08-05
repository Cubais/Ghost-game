using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GhostGamePlayer
{
  
  public partial class Game : Form
  {
    Player player;

    public Game()
    {
      InitializeComponent();
      player = new Player(this, 0, 0);
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
      this.Close();
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      switch (keyData)
      {
        case Keys.Left:
          output.Text = "LEFT" + Environment.NewLine;
          player.PositionX -= 5;
          return true;
          break;
        case Keys.Up:
          output.Text = "UP" + Environment.NewLine;
          player.PositionY -= 5;
          return true;
          break;
        case Keys.Right:
          output.Text = "RIGHT" + Environment.NewLine;
          player.PositionX += 5;
          return true;
          break;
        case Keys.Down:
          output.Text = "DOWN" + Environment.NewLine;
          player.PositionY += 5;
          return true;
          break;
        default:
          output.Text = "ANOTHER" + Environment.NewLine;
          return base.ProcessCmdKey(ref msg, keyData);
          break;
      }
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
    
    Form form;    
    Graphics g;

    public Player(Form form,int posX,int posY)
    {     
      this.form = form;
      this.PositionX = posX;
      this.PositionY = posY;
      g = form.CreateGraphics();
    }

    public void Draw()
    {
      g.Clear(Color.White);
      g.DrawRectangle(new Pen(Color.Red),new Rectangle(PositionX,PositionY,50,50));
    }

    
  }
}
