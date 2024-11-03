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
      button1 = new Button();
      textBox1 = new TextBox();
      button2 = new Button();
      label1 = new Label();
      button3 = new Button();
      label2 = new Label();
      SuspendLayout();
      // 
      // button1
      // 
      resources.ApplyResources(button1, "button1");
      button1.Name = "button1";
      button1.UseVisualStyleBackColor = true;
      button1.Click += button1_Click;
      // 
      // textBox1
      // 
      resources.ApplyResources(textBox1, "textBox1");
      textBox1.Name = "textBox1";
      textBox1.TextChanged += textBox1_TextChanged;
      // 
      // button2
      // 
      resources.ApplyResources(button2, "button2");
      button2.Name = "button2";
      button2.UseVisualStyleBackColor = true;
      button2.Click += button2_Click;
      // 
      // label1
      // 
      resources.ApplyResources(label1, "label1");
      label1.BackColor = Color.Transparent;
      label1.ForeColor = Color.White;
      label1.Name = "label1";
      label1.Click += label1_Click;
      // 
      // button3
      // 
      resources.ApplyResources(button3, "button3");
      button3.Name = "button3";
      button3.UseVisualStyleBackColor = true;
      button3.Click += button3_Click;
      // 
      // label2
      // 
      resources.ApplyResources(label2, "label2");
      label2.BackColor = Color.Transparent;
      label2.ForeColor = Color.White;
      label2.Name = "label2";
      label2.Click += label2_Click;
      label2.MouseLeave += label2_MouseLeave;
      label2.MouseHover += label2_MouseHover;
      // 
      // UserInterface
      // 
      resources.ApplyResources(this, "$this");
      AutoScaleMode = AutoScaleMode.Font;
      ControlBox = false;
      Controls.Add(label2);
      Controls.Add(button3);
      Controls.Add(label1);
      Controls.Add(button2);
      Controls.Add(textBox1);
      Controls.Add(button1);
      FormBorderStyle = FormBorderStyle.None;
      Name = "UserInterface";
      ShowIcon = false;
      ShowInTaskbar = false;
      SizeGripStyle = SizeGripStyle.Show;
      TopMost = true;
      FormClosing += UserInterface_FormClosing;
      Load += UserInterface_Load;
      Paint += UserInterface_Paint;
      MouseDown += UserInterface_MouseDown;
      MouseMove += UserInterface_MouseMove;
      MouseUp += UserInterface_MouseUp;
      ResumeLayout(false);
      PerformLayout();
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