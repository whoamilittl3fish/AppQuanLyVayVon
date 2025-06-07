using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyVayVon
{
    public static class Function_Reuse
    {
        // Hàm dùng chung để xác nhận thoát form, thêm subLabel
        private static Color Backcolor = Color.FromArgb(240, 245, 255);
        public static void ConfirmAndClose(Form form, string message = "Bạn có chắc muốn thoát không?", string? subLabel = null)
        {
            if (form == null) return;

            // Xử lý tự động xuống dòng nếu message quá dài
            string formattedMessage = message;
            int maxLineLength = 50;
            if (message.Length > maxLineLength)
            {
                var words = message.Split(' ');
                var sb = new StringBuilder();
                int currentLineLength = 0;
                foreach (var word in words)
                {
                    if (currentLineLength + word.Length + 1 > maxLineLength)
                    {
                        sb.AppendLine();
                        currentLineLength = 0;
                    }
                    if (currentLineLength > 0)
                    {
                        sb.Append(' ');
                        currentLineLength++;
                    }
                    sb.Append(word);
                    currentLineLength += word.Length;
                }
                formattedMessage = sb.ToString();
            }
           
            var confirm = CustomMessageBox.ShowCustomYesNoMessageBox(formattedMessage, form, Backcolor, 18, subLabel);
            if (confirm == DialogResult.Yes)
            {
                form.DialogResult = DialogResult.Yes;
            }
            else
            {
                form.DialogResult = DialogResult.No;
            }
        }

        public static void ConfirmAndClose_App(string message = "Bạn có chắc muốn thoát không?", string? subLabel = null)
        {
            var confirm = CustomMessageBox.ShowCustomYesNoMessageBox(message, null, Backcolor, 18, subLabel);
            if (confirm == DialogResult.No)
            {
                return;
            }
            Application.Exit();
        }

        //textbox tự clear khi click vào
        public static void ClearTextBoxOnClick(TextBox textBox, string placeholderText = "")
        {
            textBox.Text = placeholderText;
            textBox.ForeColor = System.Drawing.Color.Gray;

            if (textBox == null) return;
            textBox.GotFocus += (sender, e) =>
            {
                if (textBox.Text == placeholderText)
                {
                    textBox.Text = string.Empty;
                    textBox.ForeColor = System.Drawing.Color.Black;
                }
            };
            textBox.LostFocus += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholderText;
                    textBox.ForeColor = System.Drawing.Color.Gray;
                }
            };
        }

        public static void ClearRichTextBoxOnClick(RichTextBox richTextBox, string placeholderText = "")
        {
            if (richTextBox == null) return;

            richTextBox.Text = placeholderText;
            richTextBox.ForeColor = System.Drawing.Color.Gray;

            richTextBox.GotFocus += (sender, e) =>
            {
                if (richTextBox.Text == placeholderText)
                {
                    richTextBox.Text = string.Empty;
                    richTextBox.ForeColor = System.Drawing.Color.Black;
                }
            };

            richTextBox.LostFocus += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(richTextBox.Text))
                {
                    richTextBox.Text = placeholderText;
                    richTextBox.ForeColor = System.Drawing.Color.Gray;
                }
            };
        }

        public static bool KiemTraDatabaseTonTai()
        {
            string dbDir = Path.Combine(Application.StartupPath, "DataBase");
            string dbPath = Path.Combine(dbDir, "data.db");
            return File.Exists(dbPath);
        }
    }
}
