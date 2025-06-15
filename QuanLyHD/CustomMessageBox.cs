using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public static class CustomMessageBox
{
    [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
    public static extern IntPtr CreateRoundRectRgn(
        int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
        int nWidthEllipse, int nHeightEllipse
    );

    private static readonly Color DefaultBackColor = Color.FromArgb(240, 245, 255);

    // Thêm tham số title cho message box
    public static DialogResult ShowCustomYesNoMessageBox(
        string message,
        IWin32Window owner = null,
        Color? backgroundColor = null,
        int cornerRadius = 18,
        string? title = null
    )
    {
        backgroundColor ??= DefaultBackColor;
        using (var form = new Form())
        {
            form.StartPosition = FormStartPosition.CenterParent;
            form.FormBorderStyle = FormBorderStyle.None;
            form.BackColor = backgroundColor.Value;
            form.Width = 370;
            form.Height = title == null ? 160 : 200;

            form.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, form.Width, form.Height, cornerRadius, cornerRadius));

            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = DefaultBackColor,
                Padding = new Padding(2)
            };
            form.Controls.Add(panel);

            Label? lblTitle = null;
            if (!string.IsNullOrWhiteSpace(title))
            {
                lblTitle = new Label
                {
                    Text = title,
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(41, 128, 185),
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Top,
                    Height = 36,
                    Margin = new Padding(0, 0, 0, 0)
                };
                panel.Controls.Add(lblTitle);
                lblTitle.BringToFront();
            }

            var lbl = new Label
            {
                Text = message,
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 60,
                Margin = new Padding(0, 10, 0, 0)
            };
            panel.Controls.Add(lbl);

            if (lblTitle != null)
                lbl.BringToFront();

            var btnYes = new Button
            {
                Text = "Yes",
                DialogResult = DialogResult.Yes,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Width = 90,
                Height = 36,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnYes.FlatAppearance.BorderSize = 0;
            btnYes.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnYes.Width, btnYes.Height, 14, 14));
            btnYes.Location = new Point(form.ClientSize.Width / 2 - btnYes.Width - 12, form.Height - 60);
            panel.Controls.Add(btnYes);

            var btnNo = new Button
            {
                Text = "No",
                DialogResult = DialogResult.No,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Width = 90,
                Height = 36,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnNo.FlatAppearance.BorderSize = 0;
            btnNo.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnNo.Width, btnNo.Height, 14, 14));
            btnNo.Location = new Point(form.ClientSize.Width / 2 + 12, form.Height - 60);
            panel.Controls.Add(btnNo);

            form.AcceptButton = btnYes;
            form.CancelButton = btnNo;

            form.Paint += (s, e) =>
            {
                using (var path = new GraphicsPath())
                {
                    path.AddEllipse(-20, form.Height - 40, form.Width + 40, 40);
                    using (var brush = new PathGradientBrush(path)
                    {
                        CenterColor = Color.FromArgb(40, 44, 62, 80),
                        SurroundColors = new[] { Color.Transparent }
                    })
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                }
            };

            return owner == null ? form.ShowDialog() : form.ShowDialog(owner);
        }
    }

    public static DialogResult ShowCustomMessageBox(
        string message,
        IWin32Window owner = null,
        string? title = null
    )
    {
        using (var form = new Form())
        {
            form.StartPosition = FormStartPosition.CenterParent;
            form.FormBorderStyle = FormBorderStyle.None;
            form.BackColor = DefaultBackColor;
            form.Width = 700;
            form.Height = title == null ? 150 : 190;

            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(2)
            };
            form.Controls.Add(panel);

            Label? lblTitle = null;
            if (!string.IsNullOrWhiteSpace(title))
            {
                lblTitle = new Label
                {
                    Text = title,
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(41, 128, 185),
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Top,
                    Height = 36,
                    Margin = new Padding(0, 0, 0, 0)
                };
                panel.Controls.Add(lblTitle);
                lblTitle.BringToFront();
            }

            var lbl = new Label
            {
                Text = message,
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(231, 76, 60),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 60,
                Margin = new Padding(0, 10, 0, 0)
            };
            panel.Controls.Add(lbl);

            if (lblTitle != null)
                lbl.BringToFront();

            using (var g = form.CreateGraphics())
            {
                SizeF textSize = g.MeasureString(message, lbl.Font, form.Width - 40);
                int neededHeight = (int)Math.Ceiling(textSize.Height) + 30;
                if (neededHeight > lbl.Height)
                {
                    lbl.Height = neededHeight;
                    form.Height = lbl.Height + 80 + (title == null ? 0 : 40);
                }
            }

            var btn = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.Yes,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Width = 90,
                Height = 36,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 14, 14));
            btn.Location = new Point(
                (form.ClientSize.Width - btn.Width) / 2,
                form.ClientSize.Height - btn.Height - 24
            );
            panel.Controls.Add(btn);

            form.AcceptButton = btn;

            form.Paint += (s, e) =>
            {
                using (var path = new GraphicsPath())
                {
                    path.AddEllipse(-20, form.Height - 40, form.Width + 40, 40);
                    using (var brush = new PathGradientBrush(path)
                    {
                        CenterColor = Color.FromArgb(40, 44, 62, 80),
                        SurroundColors = new[] { Color.Transparent }
                    })
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                }
            };

            return owner == null ? form.ShowDialog() : form.ShowDialog(owner);
        }
    }
}
