using System.Globalization;
using System.Numerics; // Thêm namespace này
using System.Text;
using System.Text.RegularExpressions;

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

        public static string FormatDate(string ngay)
        {
            if (DateTime.TryParse(ngay, out DateTime dt))
                return dt.ToString("dd/MM/yyyy");
            return "(chưa có)";
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

        public static (DialogResult Result, string? Value) ShowCustomInputMoneyBox(
    string prompt,
    IWin32Window? owner = null,
    string? title = "Nhập dữ liệu",
    string? defaultValue = null,
    Color? backgroundColor = null,
    int cornerRadius = 18
)
        {
            backgroundColor ??= Color.FromArgb(240, 245, 255);

            using (var form = new Form())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.None;
                form.BackColor = backgroundColor.Value;
                form.Width = 400;
                form.Height = 210;
                form.Region = Region.FromHrgn(CustomMessageBox.CreateRoundRectRgn(0, 0, form.Width, form.Height, cornerRadius, cornerRadius));

                var panel = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = backgroundColor.Value,
                    Padding = new Padding(16)
                };
                form.Controls.Add(panel);

                if (!string.IsNullOrWhiteSpace(title))
                {
                    var lblTitle = new Label
                    {
                        Text = title,
                        Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                        ForeColor = Color.FromArgb(41, 128, 185),
                        AutoSize = false,
                        Width = form.Width,
                        Height = 40,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Location = new Point(0, 10)
                    };
                    panel.Controls.Add(lblTitle);
                }

                var lblPrompt = new Label
                {
                    Text = prompt,
                    Font = new Font("Segoe UI", 10.5F, FontStyle.Regular),
                    AutoSize = false,
                    Width = form.Width - 32,
                    Height = 30,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Location = new Point(16, 65)
                };
                panel.Controls.Add(lblPrompt);

                var txtInput = new TextBox
                {
                    Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                    Width = form.ClientSize.Width - 40,
                    Text = defaultValue ?? ""
                };
                txtInput.Location = new Point((form.ClientSize.Width - txtInput.Width) / 2, 105);
                panel.Controls.Add(txtInput);

                txtInput.KeyPress += Function_Reuse.OnlyAllowDigit_KeyPress;
                txtInput.TextChanged += (s, e) => Function_Reuse.FormatTextBoxWithThousands(txtInput, "");

                var btnOK = new Button
                {
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    BackColor = Color.FromArgb(46, 204, 113),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Width = 90,
                    Height = 36
                };
                btnOK.FlatAppearance.BorderSize = 0;
                btnOK.Region = Region.FromHrgn(CustomMessageBox.CreateRoundRectRgn(0, 0, btnOK.Width, btnOK.Height, 14, 14));
                btnOK.Location = new Point(form.ClientSize.Width / 2 - 100, form.Height - 60);
                panel.Controls.Add(btnOK);

                var btnCancel = new Button
                {
                    Text = "Hủy",
                    DialogResult = DialogResult.Cancel,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    BackColor = Color.FromArgb(231, 76, 60),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Width = 90,
                    Height = 36
                };
                btnCancel.FlatAppearance.BorderSize = 0;
                btnCancel.Region = Region.FromHrgn(CustomMessageBox.CreateRoundRectRgn(0, 0, btnCancel.Width, btnCancel.Height, 14, 14));
                btnCancel.Location = new Point(form.ClientSize.Width / 2 + 10, form.Height - 60);
                panel.Controls.Add(btnCancel);

                form.AcceptButton = btnOK;
                form.CancelButton = btnCancel;

                txtInput.Focus();
                var result = owner == null ? form.ShowDialog() : form.ShowDialog(owner);

                return (result, txtInput.Text.Trim());
            }
        }



        public static string? ShowCustomInputBox(
    string prompt,
    IWin32Window owner = null,
    string? title = "Nhập dữ liệu",
    string? defaultValue = null,
    Color? backgroundColor = null,
    int cornerRadius = 18
)
        {
            backgroundColor ??= Color.FromArgb(240, 245, 255);

            using (var form = new Form())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.None;
                form.BackColor = backgroundColor.Value;
                form.Width = 400;
                form.Height = 210;
                form.Region = Region.FromHrgn(CustomMessageBox.CreateRoundRectRgn(0, 0, form.Width, form.Height, cornerRadius, cornerRadius));

                var panel = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = backgroundColor.Value,
                    Padding = new Padding(16)
                };
                form.Controls.Add(panel);

                if (!string.IsNullOrWhiteSpace(title))
                {
                    var lblTitle = new Label
                    {
                        Text = title,
                        Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                        ForeColor = Color.FromArgb(41, 128, 185),
                        Dock = DockStyle.Top,
                        Height = 36,
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    panel.Controls.Add(lblTitle);
                }

                var lblPrompt = new Label
                {
                    Text = prompt,
                    Font = new Font("Segoe UI", 10.5F, FontStyle.Regular),
                    Dock = DockStyle.Top,
                    Height = 32,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(0, 10, 0, 0)
                };
                panel.Controls.Add(lblPrompt);

                var txtInput = new TextBox
                {
                    Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                    Width = form.ClientSize.Width - 40,
                    Text = defaultValue ?? ""
                };
                txtInput.Location = new Point((form.ClientSize.Width - txtInput.Width) / 2, 85);
                panel.Controls.Add(txtInput);

                var btnOK = new Button
                {
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    BackColor = Color.FromArgb(46, 204, 113),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Width = 90,
                    Height = 36
                };
                btnOK.FlatAppearance.BorderSize = 0;
                btnOK.Region = Region.FromHrgn(CustomMessageBox.CreateRoundRectRgn(0, 0, btnOK.Width, btnOK.Height, 14, 14));
                btnOK.Location = new Point(form.ClientSize.Width / 2 - 100, form.Height - 60);
                panel.Controls.Add(btnOK);

                var btnCancel = new Button
                {
                    Text = "Hủy",
                    DialogResult = DialogResult.Cancel,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    BackColor = Color.FromArgb(231, 76, 60),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Width = 90,
                    Height = 36
                };
                btnCancel.FlatAppearance.BorderSize = 0;
                btnCancel.Region = Region.FromHrgn(CustomMessageBox.CreateRoundRectRgn(0, 0, btnCancel.Width, btnCancel.Height, 14, 14));
                btnCancel.Location = new Point(form.ClientSize.Width / 2 + 10, form.Height - 60);
                panel.Controls.Add(btnCancel);

                form.AcceptButton = btnOK;
                form.CancelButton = btnCancel;

                txtInput.Focus();
                var result = owner == null ? form.ShowDialog() : form.ShowDialog(owner);

                return result == DialogResult.OK ? txtInput.Text.Trim() : null;
            }
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
            // Dùng "N0" để format như: 1,234,567 (không có phần thập phân)
            return value.ToString("N0", CultureInfo.InvariantCulture);
        }

        public static string FormatNumberWithThousandsSeparator(int value)
        {
            return value.ToString("N0", CultureInfo.InvariantCulture);
        }

        public static string FormatNumberWithThousandsSeparator(object value)
        {
            if (value == null || value == DBNull.Value) return "0";
            if (decimal.TryParse(value.ToString(), out decimal number))
                return FormatNumberWithThousandsSeparator(number);
            return "0";
        }

        public static void OnlyAllowDigitAndDot_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cho phép số, phím điều khiển và duy nhất một dấu chấm
            TextBox tb = sender as TextBox;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            // Chỉ cho phép một dấu chấm
            if (e.KeyChar == '.' && tb != null && tb.Text.Contains('.'))
            {
                e.Handled = true;
            }
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
