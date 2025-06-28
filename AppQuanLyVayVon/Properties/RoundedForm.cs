using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class RoundedForm : Form
{
    public int BorderRadius { get; set; } = 32;
    public int BorderThickness { get; set; } = 2;
    public Color BorderColor { get; set; } = Color.FromArgb(70, 130, 180); // SteelBlue

    public RoundedForm()
    {
        this.FormBorderStyle = FormBorderStyle.None;
        this.DoubleBuffered = true;
        this.ResizeRedraw = true; // Vẽ lại khi resize
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        UpdateRegion();
        this.Invalidate(); // Gọi lại OnPaint để vẽ viền
    }

    private void UpdateRegion()
    {
        if (this.WindowState == FormWindowState.Maximized || BorderRadius <= 0)
        {
            this.Region = null;
            return;
        }

        using (GraphicsPath path = new GraphicsPath())
        {
            int r = BorderRadius;
            path.AddArc(0, 0, r, r, 180, 90);
            path.AddArc(this.Width - r - 1, 0, r, r, 270, 90);
            path.AddArc(this.Width - r - 1, this.Height - r - 1, r, r, 0, 90);
            path.AddArc(0, this.Height - r - 1, r, r, 90, 90);
            path.CloseFigure();

            this.Region = new Region(path);
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (this.WindowState == FormWindowState.Maximized || BorderThickness <= 0)
            return;

        using (GraphicsPath path = new GraphicsPath())
        using (Pen pen = new Pen(BorderColor, BorderThickness))
        {
            int r = BorderRadius;
            path.AddArc(0, 0, r, r, 180, 90);
            path.AddArc(this.Width - r - 1, 0, r, r, 270, 90);
            path.AddArc(this.Width - r - 1, this.Height - r - 1, r, r, 0, 90);
            path.AddArc(0, this.Height - r - 1, r, r, 90, 90);
            path.CloseFigure();

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawPath(pen, path);
        }
    }
}
