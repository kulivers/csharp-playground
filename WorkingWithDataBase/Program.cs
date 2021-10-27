namespace WorkingWithDataBase
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new DataBaseInteraction();
            test.Open();
            test.CreateEmployeesTable();
            test.Close();
            // DESKTOP-94PAMUT\MSSQLSERVER01
        }
        
    }
}