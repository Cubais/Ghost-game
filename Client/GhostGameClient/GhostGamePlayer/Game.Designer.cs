﻿namespace GhostGamePlayer
{
  partial class Game
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.GameTimer = new System.Windows.Forms.Timer(this.components);
      this.SuspendLayout();
      // 
      // GameTimer
      // 
      this.GameTimer.Interval = 50;
      this.GameTimer.Tick += new System.EventHandler(this.GameTimer_Tick);
      // 
      // Game
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.ClientSize = new System.Drawing.Size(1282, 718);
      this.ControlBox = false;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Game";
      this.Text = "Form1";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Load += new System.EventHandler(this.Game_Load);
      this.Resize += new System.EventHandler(this.Game_Resize);
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Timer GameTimer;
  }
}

