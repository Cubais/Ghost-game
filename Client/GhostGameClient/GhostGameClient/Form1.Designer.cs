namespace GhostGameClient
{
  partial class Form1
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.nameBox = new System.Windows.Forms.TextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.output = new System.Windows.Forms.TextBox();
      this.StartGame = new System.Windows.Forms.Button();
      this.NameLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // nameBox
      // 
      this.nameBox.Location = new System.Drawing.Point(63, 11);
      this.nameBox.Name = "nameBox";
      this.nameBox.Size = new System.Drawing.Size(246, 22);
      this.nameBox.TabIndex = 0;
      // 
      // button1
      // 
      this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
      this.button1.Location = new System.Drawing.Point(63, 39);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(246, 47);
      this.button1.TabIndex = 1;
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // output
      // 
      this.output.Location = new System.Drawing.Point(12, 92);
      this.output.Multiline = true;
      this.output.Name = "output";
      this.output.Size = new System.Drawing.Size(297, 361);
      this.output.TabIndex = 2;
      // 
      // StartGame
      // 
      this.StartGame.Image = ((System.Drawing.Image)(resources.GetObject("StartGame.Image")));
      this.StartGame.Location = new System.Drawing.Point(15, 459);
      this.StartGame.Name = "StartGame";
      this.StartGame.Size = new System.Drawing.Size(294, 145);
      this.StartGame.TabIndex = 3;
      this.StartGame.UseVisualStyleBackColor = true;
      this.StartGame.Click += new System.EventHandler(this.StartGame_Click);
      // 
      // NameLabel
      // 
      this.NameLabel.AutoSize = true;
      this.NameLabel.Location = new System.Drawing.Point(12, 11);
      this.NameLabel.Name = "NameLabel";
      this.NameLabel.Size = new System.Drawing.Size(45, 17);
      this.NameLabel.TabIndex = 4;
      this.NameLabel.Text = "Name";
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(321, 616);
      this.Controls.Add(this.NameLabel);
      this.Controls.Add(this.StartGame);
      this.Controls.Add(this.output);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.nameBox);
      this.Name = "Form1";
      this.Text = "Ghost";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
      this.Load += new System.EventHandler(this.Form1_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox nameBox;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.TextBox output;
    private System.Windows.Forms.Button StartGame;
    private System.Windows.Forms.Label NameLabel;
  }
}

