namespace AddictionalFeatures
{
    public class InitProperties //Working only on .Net 5
    {
        public readonly string pseudoName;
        public string Name { get; init; }
        public int Age { get; set; }

        public InitProperties()
        {
            pseudoName = "asada";
        }

        public static void LocalMain()
        {
            InitProperties initProperties = new() { Age = 4, Name = "Egor" };
            // InitProperties initProperties = new InitProperties(){Age = 4,Name = "Egor", pseudoName = "sada"};//ERROR
            initProperties.Age = 3;
            // initProperties.Name = "asda";//Error
            // initProperties.pseudoName = "asda";//Error
        }
    }
}