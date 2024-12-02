using LEFiles.Client.UI.Services.Clients;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LEFiles.Client.UI
{
  public partial class AddAuthKey : Form
  {
    public AddAuthKey()
    {
      InitializeComponent();
    }

    private void AddAuthKey_Load(object sender, EventArgs e)
    {

    }
    protected override void OnPaintBackground(PaintEventArgs e)
    {
      Set_Background(null, e);
    }
    private void Set_Background(object sender, PaintEventArgs e)
    {
      Graphics graphics = e.Graphics;

      //the rectangle, the same size as our Form
      Rectangle gradient_rectangle = new Rectangle(0, 0, Width, Height);

      //define gradient's properties
      Brush b = new LinearGradientBrush(gradient_rectangle, Color.FromArgb(0, 0, 0), Color.FromArgb(57, 128, 227), 65f);

      //apply gradient         
      graphics.FillRectangle(b, gradient_rectangle);
    }

    private void save_btn_Click(object sender, EventArgs e)
    {
      var authtoken = token.Text;
      var authsecret = secret.Text;
      var registerClientSvc = new RegisterClientService();
      try {
        registerClientSvc.Register(authtoken, authsecret);
        this.Close();
      }catch{
        MessageBox.Show("Tekrar deneyin");
      }

    }
  }
}
