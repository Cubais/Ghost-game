﻿namespace GhostGameClient
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
      this.nameBox = new System.Windows.Forms.TextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.output = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // nameBox
      // 
      this.nameBox.Location = new System.Drawing.Point(176, 124);
      this.nameBox.Name = "nameBox";
      this.nameBox.Size = new System.Drawing.Size(462, 22);
      this.nameBox.TabIndex = 0;
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(314, 152);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(158, 62);
      this.button1.TabIndex = 1;
      this.button1.Text = "Connect";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // output
      // 
      this.output.Location = new System.Drawing.Point(164, 220);
      this.output.Multiline = true;
      this.output.Name = "output";
      this.output.Size = new System.Drawing.Size(462, 189);
      this.output.TabIndex = 2;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.output);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.nameBox);
      this.Name = "Form1";
      this.Text = "Form1";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox nameBox;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.TextBox output;
  }
}

