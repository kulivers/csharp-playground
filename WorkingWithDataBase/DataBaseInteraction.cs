using System;
using System.Text;
using Microsoft.Data.SqlClient;

namespace WorkingWithDataBase
{
    class DataBaseInteraction
    {
        public void TryConnectToDb()
        {
            try
            {
                Console.WriteLine("Openning Connection ...");

                //open connection
                conn.Open();

                Console.WriteLine("Connection successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }


        string datasource = "LOCAL"; //your server

        string database = "@TestDataBase"; //your database name
        string username = "egor"; //username of server to connect
        string password = "111"; //password
        private string connString;
        private SqlConnection conn;

        public DataBaseInteraction()
        {
            connString = @"Data Source=" + datasource + ";Initial Catalog="
                         + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;
            try
            {
                conn = new SqlConnection(connString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Open()
        {
            try
            {
                conn.Open();
                Console.WriteLine("con opened");
            }
            catch (Exception e)
            {
                Console.WriteLine("connection open error");
                Console.WriteLine(e);
                throw;
            }
        }

        public void Close()
        {
            try
            {
                conn.Close();
                Console.WriteLine("con close");
            }
            catch (Exception e)
            {
                Console.WriteLine("connection close error");
                Console.WriteLine(e);
                throw;
            }
        }

        public void CreateEmployeesTable()
        {
            try
            {
                //create a new SQL Query using StringBuilder
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.Append(@"CREATE TABLE dbo.Employees
            (
                empid INT NOT NULL,
                firstname VARCHAR(30) NOT NULL,
                lastname VARCHAR(30) NOT NULL,
                hiredate DATE NOT NULL,
                mgrid INT NULL,
                ssn VARCHAR(20) NOT NULL,
                salary MONEY NOT NULL
            ); ");
                // strBuilder.Append("(N'Harsh', N'harsh@gmail.com', N'Class X'), ");
                // strBuilder.Append("(N'Ronak', N'ronak@gmail.com', N'Class X') ");

                var sqlQuery = strBuilder.ToString();
                using SqlCommand command = new SqlCommand(sqlQuery, conn);
                command.ExecuteNonQuery(); //execute the Query
                Console.WriteLine("Query Executed.");
            }
            catch (Exception e)
            {
                Console.WriteLine("create table error");
                Console.WriteLine(e);
                throw;
            }
        }
    }
}