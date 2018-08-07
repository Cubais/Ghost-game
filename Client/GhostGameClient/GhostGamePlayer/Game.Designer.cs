namespace GhostGamePlayer
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
      this.Exit = new System.Windows.Forms.Button();
      this.GameTimer = new System.Windows.Forms.Timer(this.components);
      this.SuspendLayout();
      // 
      // Exit
      // 
      this.Exit.Location = new System.Drawing.Point(658, 12);
      this.Exit.Name = "Exit";
      this.Exit.Size = new System.Drawing.Size(130, 40);
      this.Exit.TabIndex = 1;
      this.Exit.Text = "EXIT";
      this.Exit.UseVisualStyleBackColor = true;
      this.Exit.Click += new System.EventHandler(this.button1_Click);
      // 
      // GameTimer
      // 
      this.GameTimer.Interval = 20;
      this.GameTimer.Tick += new System.EventHandler(this.GameTimer_Tick);
      // 
      // Game
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.Exit);
      this.Name = "Game";
      this.Text = "Form1";
      this.Load += new System.EventHandler(this.Game_Load);
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Button Exit;
    private System.Windows.Forms.Timer GameTimer;
  }
}

