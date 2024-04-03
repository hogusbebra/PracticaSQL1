

using System;

using Microsoft.Data.SqlClient;


class Program
{
    static async Task Main(string[] args)
    {
        while (true)
        {
            string connectionString = "Server=DESKTOP-5BD88QO\\SQLEXPRESS;Database=CompanyBD;;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();


                string sql = $"SELECT * FROM Employees;";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["EmployeeID"]} {reader["FirstName"]} {reader["LastName"]}, {reader["Position"]}, {reader["Salary"]}");
                        }
                    }
                }
                Console.WriteLine("\nVыберите дейстVие:");
                Console.WriteLine("1. Create");
                Console.WriteLine("2. Update");
                Console.WriteLine("3. Delete\n");
                Console.ResetColor();
                int number = Convert.ToInt16(Console.ReadLine());
                switch (number)
                {
                    //создание
                    case 1:
                        using (SqlCommand create = new SqlCommand(sql, connection))
                        {
                            int num = await create.ExecuteNonQueryAsync();
                            Console.WriteLine("\nVедите имя:");
                            string firstname = Console.ReadLine();
                            Console.WriteLine("\nVедите фамилию:");
                            string lastname = Console.ReadLine();
                            string pos = "Position";
                            Console.WriteLine("\nVедит Salary");
                            int salary = Int32.Parse(Console.ReadLine());

                            sql = $"INSERT INTO Employees (FirstName,LastNAme,Position,Salary) VALUES ('{firstname}','{lastname}','{pos}','{salary}')";
                            create.CommandText = sql;
                            num = await create.ExecuteNonQueryAsync();
                            Console.WriteLine($"\nдобаVлено объектоV: {num}");
                        }
                        break;

                    //изменение
                    case 2:
                        using (SqlCommand update = new SqlCommand(sql, connection))
                        {
                            int numbe = await update.ExecuteNonQueryAsync();

                            Console.WriteLine("\nVедите Salary:");
                            int salary = Int32.Parse(Console.ReadLine());


                            Console.WriteLine("Vедите ID:");
                            string employeeID = Console.ReadLine();

                            sql = $"UPDATE Employees SET Salary='{salary}' WHERE EmployeeID={employeeID}";
                            update.CommandText = sql;
                            numbe = await update.ExecuteNonQueryAsync();
                            Console.WriteLine($"\nобноVлено объектоV: {numbe}");
                        }
                        break;
                    //удаление
                    case 3:
                        using (SqlCommand delete = new SqlCommand(sql, connection))
                        {
                            int numb = await delete.ExecuteNonQueryAsync();
                            Console.WriteLine("Vедите ID для удаления:");
                            string empID = Console.ReadLine();
                            sql = $"DELETE FROM Employees WHERE EmployeeID={empID}";
                            delete.CommandText = sql;
                            numb = await delete.ExecuteNonQueryAsync();
                            Console.WriteLine($"\nудалено объектоV:{numb}");
                        }
                        break;
                }
            }
        }
    }
}