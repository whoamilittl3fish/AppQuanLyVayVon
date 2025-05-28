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
        private static readonly Dictionary<Type, Form> _formCache = new();

        public static void ShowFormIfNotOpen<T>() where T : Form, new()
        {
            if (_formCache.TryGetValue(typeof(T), out var cachedForm) && cachedForm.IsDisposed)
            {
                _formCache.Remove(typeof(T));
                cachedForm = null;
            }

            if (!_formCache.TryGetValue(typeof(T), out var existingForm) || existingForm == null || existingForm.IsDisposed)
            {
                var form = new T();
                form.FormClosed += (s, e) =>
                {
                    _formCache.Remove(typeof(T));
                    form.Dispose();
                };
                _formCache[typeof(T)] = form;
                form.Show();
            }
            else
            {
                existingForm.BringToFront();
                existingForm.WindowState = FormWindowState.Normal;
                if (!existingForm.Visible)
                    existingForm.Show();
            }
        }

        // Hàm dùng chung để xác nhận thoát form
        public static void ConfirmAndClose(Form form, string message = "Bạn có chắc muốn thoát không?")
        {
            if (form == null) return;

            var confirm = MessageBox.Show(message, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                form.Close();
            }
        }

        public static void ConfirmAndClose_App()
        {
            //string message = "Bạn có chắc muốn thoát không?";
            //var confirm = MessageBox.Show(message, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (confirm == DialogResult.Yes)
            //{
            //    Application.Exit(); // Đóng toàn bộ ứng dụng
            //}
            Application.Exit(); // Đóng toàn bộ ứng dụng
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

    }

}
