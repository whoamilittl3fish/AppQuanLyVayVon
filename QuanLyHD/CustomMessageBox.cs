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

    public static DialogResult ShowCustomYesNoMessageBox(string message, IWin32Window owner = null, Color? backgroundColor = null, int cornerRadius = 18)
    {
        using (var form = new Form())
        {
            form.StartPosition = FormStartPosition.CenterParent;
            form.FormBorderStyle = FormBorderStyle.None;
            form.BackColor = backgroundColor ?? Color.White;
            form.Width = 350;
            form.Height = 160;

            // Bo tròn góc form
            form.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, form.Width, form.Height, cornerRadius, cornerRadius));

            var lbl = new Label
            {
                Text = message,
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 80
            };
            form.Controls.Add(lbl);

            var btnYes = new Button
            {
                Text = "Yes",
                DialogResult = DialogResult.Yes,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Width = 80,
                Height = 32
            };
            btnYes.FlatAppearance.BorderSize = 0;
            btnYes.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnYes.Width, btnYes.Height, 12, 12));
            btnYes.Location = new Point(form.ClientSize.Width / 2 - btnYes.Width - 10, 100);
            form.Controls.Add(btnYes);

            var btnNo = new Button
            {
                Text = "No",
                DialogResult = DialogResult.No,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Width = 80,
                Height = 32
            };
            btnNo.FlatAppearance.BorderSize = 0;
            btnNo.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnNo.Width, btnNo.Height, 12, 12));
            btnNo.Location = new Point(form.ClientSize.Width / 2 + 10, 100);
            form.Controls.Add(btnNo);

            form.AcceptButton = btnYes;
            form.CancelButton = btnNo;

            return owner == null ? form.ShowDialog() : form.ShowDialog(owner);
        }
    }

    public static DialogResult ShowCustomMessageBox(string message, IWin32Window owner = null)
    {
        using (var form = new Form())
        {
            form.StartPosition = FormStartPosition.CenterParent;
            form.FormBorderStyle = FormBorderStyle.None;
            form.BackColor = Color.White;
            form.Width = 500;
            form.Height = 150;

            var lbl = new Label
            {
                Text = message,
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(231, 76, 60),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 70
            };

            // Tính toán kích thước label phù hợp với nội dung
            using (var g = form.CreateGraphics())
            {
                SizeF textSize = g.MeasureString(message, lbl.Font, form.Width - 40);
                int neededHeight = (int)Math.Ceiling(textSize.Height) + 30;
                if (neededHeight > lbl.Height)
                {
                    lbl.Height = neededHeight;
                    form.Height = lbl.Height + 80; // 80 = button + padding
                }
            }
            form.Controls.Add(lbl);

            var btn = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Width = 80,
                Height = 32
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 12, 12));
            btn.Location = new Point((form.ClientSize.Width - btn.Width) / 2, lbl.Bottom + 20);
            form.Controls.Add(btn);

            form.AcceptButton = btn;

            return owner == null ? form.ShowDialog() : form.ShowDialog(owner);
        }
    }
}
