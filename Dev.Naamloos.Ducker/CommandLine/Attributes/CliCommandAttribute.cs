namespace Dev.Naamloos.Ducker.CommandLine.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class CliCommandAttribute : Attribute
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public CliCommandAttribute(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
