namespace Plugins.Survicate
{
    public class SurvicateFontSystem
    {
        public string regular { get; private set; }
        public string regularItalic { get; private set; }
        public string bold { get; private set; }
        public string boldItalic { get; private set; }

        public SurvicateFontSystem(string regular, string regularItalic, string bold, string boldItalic)
        {
            this.regular = regular ?? "";
            this.regularItalic = regularItalic ?? "";
            this.bold = bold ?? "";
            this.boldItalic = boldItalic ?? "";
        }
    }
}
