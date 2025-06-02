public static class AppStyle
{
    public static readonly Color BackColor = Color.FromArgb(245, 245, 250);
    public static readonly Font MainFont = new Font("Segoe UI", 11F, FontStyle.Regular);

    public static void Apply(Form form)
    {
        form.BackColor = BackColor;
        form.Font = MainFont;
        form.FormBorderStyle = FormBorderStyle.None;
        form.StartPosition = FormStartPosition.CenterScreen;
    }

    public static void StyleTextBox(TextBox tb)
    {
        tb.BorderStyle = BorderStyle.FixedSingle;
        tb.BackColor = Color.White;
        tb.Font = MainFont;
    }

    public static void StyleButton(Button btn)
    {
        btn.BackColor = Color.FromArgb(0, 120, 215);
        btn.ForeColor = Color.White;
        btn.FlatStyle = FlatStyle.Flat;
        btn.Font = MainFont;
    }
}
