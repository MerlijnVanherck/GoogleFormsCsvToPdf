namespace GoogleFormsCsvToPdf
{
    public class FormField
    {
        public string Text { get; set; }
        public ExportMode Export { get; set; }

        public FormField(string text, ExportMode exportMode = ExportMode.Include)
        {
            Text = text;
            Export = exportMode;
        }
    }
}