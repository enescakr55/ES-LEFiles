namespace LEFiles.Client.UI
{
  partial class AddAuthKey
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
      token = new TextBox();
      save_btn = new Button();
      label1 = new Label();
      label2 = new Label();
      secret = new TextBox();
      SuspendLayout();
      // 
      // token
      // 
      token.Location = new Point(12, 36);
      token.Multiline = true;
      token.Name = "token";
      token.Size = new Size(501, 40);
      token.TabIndex = 0;
      // 
      // save_btn
      // 
      save_btn.Location = new Point(12, 181);
      save_btn.Name = "save_btn";
      save_btn.Size = new Size(104, 45);
      save_btn.TabIndex = 1;
      save_btn.Text = "Kaydet";
      save_btn.UseVisualStyleBackColor = true;
      save_btn.Click += save_btn_Click;
      // 
      // label1
      // 
      label1.AutoSize = true;
      label1.Location = new Point(11, 13);
      label1.Name = "label1";
      label1.Size = new Size(48, 20);
      label1.TabIndex = 2;
      label1.Text = "Token";
      // 
      // label2
      // 
      label2.AutoSize = true;
      label2.Location = new Point(12, 93);
      label2.Name = "label2";
      label2.Size = new Size(50, 20);
      label2.TabIndex = 3;
      label2.Text = "Secret";
      // 
      // secret
      // 
      secret.Location = new Point(12, 120);
      secret.Name = "secret";
      secret.Size = new Size(500, 27);
      secret.TabIndex = 4;
      // 
      // AddAuthKey
      // 
      AutoScaleDimensions = new SizeF(8F, 20F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(525, 238);
      Controls.Add(secret);
      Controls.Add(label2);
      Controls.Add(label1);
      Controls.Add(save_btn);
      Controls.Add(token);
      Name = "AddAuthKey";
      Text = "Doğrulama Kodu Ekle Veya Güncelle";
      Load += AddAuthKey_Load;
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private TextBox token;
    private Button save_btn;
    private Label label1;
    private Label label2;
    private TextBox secret;
  }
}