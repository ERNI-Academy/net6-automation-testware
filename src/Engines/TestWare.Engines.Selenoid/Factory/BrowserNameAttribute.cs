namespace TestWare.Engines.Selenoid.Factory
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class BrowserNameAttribute : Attribute
    {
        private readonly string Name;

        public BrowserNameAttribute(string name)
        {
            this.Name = name;
        }

        public static string GetPropertyName()
        {
            return "BrowserName";
        }

        public string GetValue()
        {
            return this.Name;
        }
    }
}
