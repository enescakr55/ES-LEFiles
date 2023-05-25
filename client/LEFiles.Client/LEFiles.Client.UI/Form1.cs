using System.Drawing.Drawing2D;
namespace LEFiles.Client.UI
{
  public partial class UserInterface : Form
  {
    private bool _moving = false;
    private Point _offset;
    private NotifyIcon _notifyIcon;
    private ContextMenuStrip _contextMenuStrip;
    private List<ToolStripItem> _toolStripItems;

    public UserInterface()
    {
      InitializeComponent();
      _notifyIcon = new NotifyIcon();
      _contextMenuStrip = new ContextMenuStrip();
      _toolStripItems = new List<ToolStripItem>();
      this.SetStyle(ControlStyles.AllPaintingInWmPaint
              | ControlStyles.OptimizedDoubleBuffer
              | ControlStyles.ResizeRedraw
              | ControlStyles.DoubleBuffer
              | ControlStyles.UserPaint
              , true);

    }
    protected override void OnPaintBackground(PaintEventArgs e)
    {
      Set_Background(null, e);
    }

    private void UserInterface_Load(object sender, EventArgs e)
    {
      PrepareNotifyIcon();
      this.TopMost = true;
    }

    private void NotifyIcon1_BalloonTipShown(object? sender, EventArgs e)
    {
      
    }

    private void UserInterface_MouseDown(object sender, MouseEventArgs e)
    {
      _moving = true;
      _offset = new Point(e.X, e.Y);
      Cursor.Current = Cursors.NoMove2D;
    }

    private void UserInterface_MouseUp(object sender, MouseEventArgs e)
    {
      _moving = false;
      Cursor.Current = Cursors.Default;
    }

    private void UserInterface_MouseMove(object sender, MouseEventArgs e)
    {
      if (_moving)
      {
        Point newLocation = this.Location;
        newLocation.X += e.X - _offset.X;
        newLocation.Y += e.Y - _offset.Y;
        this.Location = newLocation;
      }
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

    private void button1_Click(object sender, EventArgs e)
    {
      DriveInfo[] driveInfo = DriveInfo.GetDrives();
      foreach(var drive in driveInfo)
      {
        textBox1.Text += drive.RootDirectory.FullName + " " + drive.VolumeLabel+Environment.NewLine;
        IEnumerable<string> entries = Directory.EnumerateFileSystemEntries(drive.RootDirectory.FullName);
        IEnumerable<string> hiddenFilesQuery = from file in entries
                                               let info = new FileInfo(file)
                                               where (info.Attributes & FileAttributes.Hidden) == 0 & (info.Attributes & FileAttributes.System) == 0
                                               select file;
        foreach (string entry in hiddenFilesQuery)
        {
          textBox1.Text += entry + Environment.NewLine;
        }
        /*foreach (string folder in folders)
        {
          textBox1.Text += folder + Environment.NewLine;
        }
        foreach (string file in files)
        {
          textBox1.Text += file+Environment.NewLine;
        } */
      }

    }

    private void UserInterface_Paint(object sender, PaintEventArgs e)
    {

    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {

    }

    private void button2_Click(object sender, EventArgs e)
    {
      Properties.Settings.Default.AuthInformation = "test";
      MessageBox.Show(Properties.Settings.Default.AuthInformation);

    }

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void button3_Click(object sender, EventArgs e)
    {

    }
    private void PrepareNotifyIcon()
    {
      var icon = Icon.FromHandle(Properties.Resources.appico.ToBitmap().GetHicon());
      _notifyIcon.Visible = true;
      _notifyIcon.Icon = icon;
      _notifyIcon.Text = "LE-Files Client";

      _notifyIcon.BalloonTipTitle = "LE-Files";
      _notifyIcon.BalloonTipText = "Sunucu ile baðlantý kurmaya çalýþýyoruz lütfen bekleyin.";
      _notifyIcon.ShowBalloonTip(5000);
      _toolStripItems.Add(new ToolStripMenuItem("Programý Göster", null, new EventHandler(Open)));
      _toolStripItems.Add(new ToolStripMenuItem("Çýkýþ", null, new EventHandler(Exit)));
      _toolStripItems.Add(new ToolStripMenuItem("Ayarlar", null, new EventHandler(Settings)));
      var toolStripItems = _toolStripItems.ToArray();
      _contextMenuStrip.Items.AddRange(toolStripItems);
      _notifyIcon.ContextMenuStrip = _contextMenuStrip;
    }
    private void Open(object sender,EventArgs e)
    {
      this.WindowState = FormWindowState.Normal;
    }
    private void Exit(object sender,EventArgs e)
    {
      Application.Exit();
      Environment.Exit(0);

    }
    private void Settings(object sender,EventArgs e)
    {

    }

    private void UserInterface_FormClosing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = true;
    }

    private void label2_Click(object sender, EventArgs e)
    {
      this.WindowState = FormWindowState.Minimized;
    }

    private void label2_MouseHover(object sender, EventArgs e)
    {
      label2.BackColor = Color.Gray;
    }

    private void label2_MouseLeave(object sender, EventArgs e)
    {
      label2.BackColor = Color.Transparent;
    }
  }
}