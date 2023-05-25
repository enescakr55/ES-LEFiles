namespace LEFiles.Client.UI
{
  partial class UserInterface
  {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserInterface));
      this.button1 = new System.Windows.Forms.Button();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.button2 = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.button3 = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // button1
      // 
      resources.ApplyResources(this.button1, "button1");
      this.button1.Name = "button1";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // textBox1
      // 
      resources.ApplyResources(this.textBox1, "textBox1");
      this.textBox1.Name = "textBox1";
      this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
      // 
      // button2
      // 
      resources.ApplyResources(this.button2, "button2");
      this.button2.Name = "button2";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // label1
      // 
      resources.ApplyResources(this.label1, "label1");
      this.label1.BackColor = System.Drawing.Color.Transparent;
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Name = "label1";
      this.label1.Click += new System.EventHandler(this.label1_Click);
      // 
      // button3
      // 
      resources.ApplyResources(this.button3, "button3");
      this.button3.Name = "button3";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new System.EventHandler(this.button3_Click);
      // 
      // label2
      // 
      resources.ApplyResources(this.label2, "label2");
      this.label2.BackColor = System.Drawing.Color.Transparent;
      this.label2.ForeColor = System.Drawing.Color.White;
      this.label2.Name = "label2";
      this.label2.Click += new System.EventHandler(this.label2_Click);
      this.label2.MouseLeave += new System.EventHandler(this.label2_MouseLeave);
      this.label2.MouseHover += new System.EventHandler(this.label2_MouseHover);
      // 
      // UserInterface
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ControlBox = false;
      this.Controls.Add(this.label2);
      this.Controls.Add(this.button3);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.button1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "UserInterface";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
      this.TopMost = true;
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserInterface_FormClosing);
      this.Load += new System.EventHandler(this.UserInterface_Load);
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.UserInterface_Paint);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UserInterface_MouseDown);
      this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UserInterface_MouseMove);
      this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UserInterface_MouseUp);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Button button1;
    private TextBox textBox1;
    private Button button2;
    private Label label1;
    private Button button3;
    private Label label2;
  }
}