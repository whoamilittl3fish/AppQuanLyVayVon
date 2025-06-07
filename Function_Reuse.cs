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


        // Hàm dùng chung để xác nhận thoát form
        public static void ConfirmAndClose(Form form, string message = "Bạn có chắc muốn thoát không?")
        {
            if (form == null) return;

            // Xử lý tự động xuống dòng nếu message quá dài
            string formattedMessage = message;
            int maxLineLength = 50; // Số ký tự tối đa trên 1 dòng, có thể điều chỉnh
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

            var confirm = CustomMessageBox.ShowCustomYesNoMessageBox(formattedMessage, form);
            if (confirm == DialogResult.Yes)
            {
                form.DialogResult = DialogResult.OK; // Hoặc DialogResult.Cancel tùy theo logic
            }
            else
            {
                form.DialogResult = DialogResult.No; // Hoặc DialogResult.None nếu không cần thiết
            }
        }

        public static void ConfirmAndClose_App(string message = "Bạn có chắc muốn thoát không?")
        {

            var confirm = CustomMessageBox.ShowCustomYesNoMessageBox(message);
            if (confirm == DialogResult.No)
            {
                return;

            }
            Application.Exit(); // Thoát ứng dụng nếu người dùng chọn Yes

        }


        //textbox tự clear khi click vào
        public static void ClearTextBoxOnClick(TextBox textBox, string placeholderText = "")
        {
            textBox.Text = placeholderText;
            textBox.ForeColor = System.Drawing.Color.Gray; // Đặt màu chữ khi đặt placeholder

            if (textBox == null) return;
            textBox.GotFocus += (sender, e) =>
            {
                if (textBox.Text == placeholderText)
                {
                    textBox.Text = string.Empty;
                    textBox.ForeColor = System.Drawing.Color.Black; // Đặt màu chữ khi clear
                }
            };
            textBox.LostFocus += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholderText;
                    textBox.ForeColor = System.Drawing.Color.Gray; // Đặt màu chữ khi đặt lại placeholder
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
