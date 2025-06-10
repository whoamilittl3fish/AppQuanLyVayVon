using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Numerics; // Thêm namespace này

namespace QuanLyVayVon
{
    public static class Function_Reuse
    {

        // Hàm dùng chung để xác nhận thoát form, thêm subLabel
        private static Color Backcolor = Color.FromArgb(240, 245, 255);
        public static DialogResult ConfirmAndClose(Form form, string message = "Bạn có chắc muốn thoát không?", string? subLabel = null)
        {
            if (form == null) return DialogResult.None;

            // Tự động xuống dòng nếu dài
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
            return confirm;
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

        /// <summary>
        /// Chỉ cho phép nhập số và một dấu chấm (không ở đầu), các phím điều khiển được phép.
        /// </summary>
        public static void OnlyAllowDigit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender is not TextBox textBox) return;

            // Cho phép phím điều khiển (Backspace, Delete, mũi tên, v.v.)
            if (char.IsControl(e.KeyChar))
                return;

            // Cho phép dấu chấm hoặc dấu phẩy nếu chưa có và không ở vị trí đầu
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                if ((textBox.Text.Contains('.') || textBox.Text.Contains(',')) || textBox.SelectionStart == 0)
                {
                    e.Handled = true; // chặn nếu đã có dấu thập phân hoặc dấu ở đầu
                }
                return;
            }

            // Chỉ cho phép số
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }


        public static void FormatTextBoxWithThousands(TextBox tb, string placeholder)
        {
            if (tb.Text == placeholder) return;

            string text = tb.Text;
            int selectionStart = tb.SelectionStart;
            int lengthBefore = text.Length;

            // Giữ nguyên dấu chấm, chỉ thêm dấu phẩy cho phần nguyên
            // Tách phần nguyên và phần thập phân (nếu có)
            int dotIndex = text.IndexOf('.');
            string integerPart = dotIndex >= 0 ? text.Substring(0, dotIndex) : text;
            string decimalPart = dotIndex >= 0 ? text.Substring(dotIndex) : "";

            // Loại bỏ ký tự không phải số trong phần nguyên
            var filteredInteger = new string(integerPart.Where(char.IsDigit).ToArray());

            // Định dạng phần nguyên với dấu phẩy hàng nghìn
            string formattedInteger = string.IsNullOrEmpty(filteredInteger)
                ? ""
                : string.Format("{0:N0}", BigInteger.Parse(filteredInteger)).Replace(".", ",");

            // Ghép lại với phần thập phân (giữ nguyên dấu chấm và các số sau dấu chấm)
            tb.Text = formattedInteger + decimalPart;

            int lengthAfter = tb.Text.Length;
            int diff = lengthAfter - lengthBefore;
            tb.SelectionStart = Math.Max(0, selectionStart + diff);
        }


        public static void ClearPlaceholderOnEnter(TextBox tb, string placeholder)
        {
            if (tb.Text == placeholder)
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }

        public static void SetPlaceholderIfEmpty(TextBox tb, string placeholder)
        {
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = placeholder;
                tb.ForeColor = Color.Gray;
            }
        }


        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="placeholderText"></param>



        // Hàm chuẩn hóa chuỗi số: chỉ giữ số và dấu thập phân
        public static string ExtractNumberString(string input)
        {

            if (string.IsNullOrWhiteSpace(input))
                return "0";

            // Loại bỏ ký tự không phải số, dấu chấm hoặc dấu phẩy
            string cleaned = Regex.Replace(input, @"[^0-9.,]", "");

          


            return cleaned;
        }


        public static string FormatNumberWithThousandsSeparator(decimal value)
        {
            // Dùng CultureInfo.InvariantCulture để có dấu phẩy là dấu phân cách hàng nghìn, dấu chấm là thập phân
            return value.ToString("#,##0.##", CultureInfo.InvariantCulture);
        }
        public static string FormatNumberWithThousandsSeparator(int value)
        {
            // Dùng CultureInfo.InvariantCulture để có dấu phẩy là dấu phân cách hàng nghìn, dấu chấm là thập phân
            return value.ToString("#,##0.##", CultureInfo.InvariantCulture);
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
