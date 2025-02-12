using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Dapper;

namespace _10._02._25
{
    internal class Program
    {
        static string? connectionString;
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            string path = Directory.GetCurrentDirectory();
            builder.SetBasePath(path);
            builder.AddJsonFile("C:\\Users\\vovan\\source\\repos\\10.02.25\\10.02.25\\appsettings.json");
            var config = builder.Build();
            connectionString = config.GetConnectionString("DefaultConnection");
            try
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("1. Показать всех покупателей");
                    Console.WriteLine("2. Отображение email всех покупателей");
                    Console.WriteLine("3. Отображение списка разделов");
                    Console.WriteLine("4. Отображение списка акционных товаров");
                    Console.WriteLine("5. Отображение всех городов");
                    Console.WriteLine("6. Отображение всех стран");
                    Console.WriteLine("7. Отображение всех покупателей из конкретного города");
                    Console.WriteLine("8. Отображение всех покупателей из конкретной страны");
                    Console.WriteLine("9. Далее");
                    Console.WriteLine("0. Выход");
                    int result = int.Parse(Console.ReadLine()!);
                    switch (result)
                    {
                        case 1:
                            Task1();
                            break;
                        case 2:
                            Task2();
                            break;
                        case 3:
                            Task3();
                            break;
                        case 4:
                            Task4();
                            break;
                        case 5:
                            Task5();
                            break;
                        case 6:
                            Task6();
                            break;
                        case 7:
                            Task7();
                            break;
                        case 8:
                            Task8();
                            break;
                        case 9:
                            NextPage1();
                            return;
                        case 0:
                            return;
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void NextPage1()
        {
            Console.Clear();
            Console.WriteLine("1. Отображение всех акций для конкретной страны");
            Console.WriteLine("2. Вставка информации о новых покупателях, странax, городax");
            Console.WriteLine("3. Вставка информации о новых разделах, акциях");
            Console.WriteLine("4. Обновление информации о покупателях ");
            Console.WriteLine("5. Обновление информации о странах");
            Console.WriteLine("6. Обновление информации о городах");
            Console.WriteLine("7. Обновление информации о разделах");
            Console.WriteLine("8. Удаление информации о покупателях");
            Console.WriteLine("9. Далее");
            Console.WriteLine("0. Выход");
            int result = int.Parse(Console.ReadLine()!);
            switch (result)
            {
                case 1:
                    Task9();
                    break;
                case 2:
                    Task10();
                    break;
                case 3:
                    Task11();
                    break;
                case 4:
                    Task12();
                    break;
                case 5:
                    Task13();
                    break;
                case 6:
                    Task14();
                    break;
                case 7:
                    Task15();
                    break;
                case 8:
                    Task16();
                    break;
                case 9:
                    NextPage2();
                    break;
                case 0:
                    return;
            };
        }
        static void NextPage2()
        {
            Console.Clear();
            Console.WriteLine("1. Удаление информации о покупателях страны");
            Console.WriteLine("2. Удаление информации о покупателях города");
            Console.WriteLine("3. Удаление информации о разделах");
            Console.WriteLine("4. Отображение списка городов конкретной страны");
            Console.WriteLine("5. Отображение списка разделов конкретного покупателя");
            Console.WriteLine("6. Отображение списка акций товаров конкретного раздела");
            Console.WriteLine("0. Выход");
            int result = int.Parse(Console.ReadLine()!);
            switch (result)
            {
                case 1:
                    Task17();
                    break;
                case 2:
                    Task18();
                    break;
                case 3:
                    Task19();
                    break;
                case 4:
                    Task20();
                    break;
                case 5:
                    Task21();
                    break;
                case 6:
                    Task22();
                    break;
                case 0:
                    return;
            };
        }
        static void Task1()
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var customers = db.Query<Customer>("SELECT * FROM Customers");
                int iter = 0;
                foreach (var customer in customers)
                    Console.WriteLine($"Customer #{++iter} {customer.Name}");
            }
            Console.ReadKey();
        }
        static void Task2()
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var customers = db.Query<Customer>("SELECT * FROM Customers");
                int iter = 0;
                foreach (var customer in customers)
                    Console.WriteLine($"Customer #{++iter} {customer.Email}");
            }
            Console.ReadKey();
        }
        static void Task3()
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sections = db.Query<Section>("SELECT * FROM Sections");
                int iter = 0;
                foreach (var section in sections)
                    Console.WriteLine($"Customer #{++iter} {section.Name}");
            }
            Console.ReadKey();
        }
        static void Task4()
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var customers = db.Query<Customer>("SELECT DISTINCT Country FROM Customers");
                int iter = 0;
                foreach (var customer in customers)
                    Console.WriteLine($"Country #{++iter} {customer.Country}");
            }
            Console.ReadKey();
        }
        static void Task5()
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var customers = db.Query<Customer>("SELECT DISTINCT City FROM Customers");
                int iter = 0;
                foreach (var customer in customers)
                    Console.WriteLine($"Customer #{++iter} {customer.City}");
            }
            Console.ReadKey();
        }
        static void Task6()
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var customers = db.Query<string>("SELECT DISTINCT Country FROM Customers");
                foreach (var country in customers)
                    Console.WriteLine(country);
            }
            Console.ReadKey();
        }

        static void Task7()
        {
            Console.Clear();
            Console.Write("Введите город: ");
            string city = Console.ReadLine();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var customers = db.Query<Customer>("SELECT * FROM Customers WHERE City = @City", new { City = city });
                foreach (var customer in customers)
                    Console.WriteLine($"{customer.Name} {customer.Surname}");
            }
            Console.ReadKey();
        }

        static void Task8()
        {
            Console.Clear();
            Console.Write("Введите страну: ");
            string country = Console.ReadLine();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var customers = db.Query<Customer>("SELECT * FROM Customers WHERE Country = @Country", new { Country = country });
                foreach (var customer in customers)
                    Console.WriteLine($"{customer.Name} {customer.Surname}");
            }
            Console.ReadKey();
        }
        static void Task9()
        {
            Console.Clear();
            Console.Write("Введите ID покупателя, акции страны которого хотите получить: ");
            string country = Console.ReadLine();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var promos = db.Query<Section>("SELECT Sections.Name, Sections.StartDate, Sections.EndDate FROM Sections INNER JOIN CustomerSections CS ON Sections.Id = CS.SectionId WHERE CS.CustomerId = @Country", new { Country = country });
                foreach (var promo in promos)
                    Console.WriteLine($"You have promotion on {promo.Name}: {promo.StartDate} - {promo.EndDate}");
            }
            Console.ReadKey();
        }

        static void Task10()
        {
            Console.Clear();

            Console.Write("Введите имя: ");
            string name = Console.ReadLine();

            Console.Write("Введите фамилию: ");
            string surname = Console.ReadLine();

            Console.Write("Введите дату рождения (ГГГГ-ММ-ДД): ");
            DateTime birthdate = DateTime.Parse(Console.ReadLine());

            Console.Write("Введите пол (Male/Female): ");
            string gender = Console.ReadLine();

            Console.Write("Введите email: ");
            string email = Console.ReadLine();

            Console.Write("Введите страну: ");
            string country = Console.ReadLine();

            Console.Write("Введите город: ");
            string city = Console.ReadLine();

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sql = "INSERT INTO Customers (Name, Surname, Birthdate, Gender, Email, Country, City) " +
                          "VALUES (@Name, @Surname, @Birthdate, @Gender, @Email, @Country, @City)";

                db.Execute(sql, new
                {
                    Name = name,
                    Surname = surname,
                    Birthdate = birthdate,
                    Gender = gender,
                    Email = email,
                    Country = country,
                    City = city
                });
            }

            Console.WriteLine("Покупатель добавлен.");
            Console.ReadKey();
        }


        static void Task11()
        {
            Console.Clear();
            Console.Write("Введите название раздела: ");
            string section = Console.ReadLine();
            Console.Write("Введите начало акции (ГГГГ-ММ-ДД): ");
            DateTime promoItemStart = DateTime.Parse(Console.ReadLine());
            Console.Write("Введите конец акции (ГГГГ-ММ-ДД): ");
            DateTime promoItemEnd = DateTime.Parse(Console.ReadLine());
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute("INSERT INTO Sections (Name, StartDate, EndDate) VALUES (@Name, @StartDate, @EndDate)", new { Name = section, StartDate = promoItemStart, EndDate = promoItemEnd });
            }
            Console.WriteLine("Раздел добавлен!");
            Console.ReadKey();
        }
        static void Task12()
        {
            Console.Clear();
            Console.Write("Введите ID покупателя: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Введите новое имя: ");
            string name = Console.ReadLine();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute("UPDATE Customers SET Name = @Name WHERE Id = @Id", new { Name = name, Id = id });
            }
            Console.WriteLine("Данные обновлены!");
            Console.ReadKey();
        }

        static void Task13()
        {
            Console.Clear();
            Console.Write("Введите ID покупателя: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Введите новое название страны: ");
            string country = Console.ReadLine();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute("UPDATE Customers SET Country = @Name WHERE Id = @Id", new { Name = country, Id = id });
            }
            Console.WriteLine("Страна обновлена!");
            Console.ReadKey();
        }

        static void Task14()
        {
            Console.Clear();
            Console.Write("Введите ID покупателя: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Введите новое название города: ");
            string city = Console.ReadLine();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute("UPDATE Customers SET Name = @City WHERE Id = @Id", new { Name = city, Id = id });
            }
            Console.WriteLine("Город обновлен!");
            Console.ReadKey();
        }

        static void Task15()
        {
            Console.Clear();
            Console.Write("Введите ID раздела: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Введите новое название раздела: ");
            string section = Console.ReadLine();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute("UPDATE Sections SET Name = @Name WHERE Id = @Id", new { Name = section, Id = id });
            }
            Console.WriteLine("Раздел обновлен!");
            Console.ReadKey();
        }



        static void Task16()
        {
            Console.Clear();
            Console.Write("Введите ID покупателя, которого нужно удалить: ");
            int id = int.Parse(Console.ReadLine());
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute("DELETE FROM Customers WHERE Id = @Id", new { Id = id });
            }
            Console.WriteLine("Покупатель удален!");
            Console.ReadKey();
        }

        static void Task17()
        {
            Console.Clear();
            Console.Write("Введите названия страны, покупателей из которой нужно удалить: ");
            string name = Console.ReadLine();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute("DELETE FROM Customers WHERE Country = @Name", new { Name = name });
            }
            Console.WriteLine("Покупатель удален!");
            Console.ReadKey();
        }

        static void Task18()
        {
            Console.Clear();
            Console.Write("Введите название города, покупателей из которого нужно удалить: ");
            string name = Console.ReadLine();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute("DELETE FROM Customers WHERE City = @Name", new { Name = name });
            }
            Console.WriteLine("Покупатель удален!");
            Console.ReadKey();
        }

        static void Task19()
        {
            Console.Clear();
            Console.Write("Введите ID раздела, который нужно удалить: ");
            int id = int.Parse(Console.ReadLine());
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute("DELETE FROM Sections WHERE Id = @Id", new { Id = id });
            }
            Console.WriteLine("Раздел удален!");
            Console.ReadKey();
        }


        static void Task20()
        {
            Console.Clear();
            Console.Write("Введите страну: ");
            string country = Console.ReadLine();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var cities = db.Query<string>("SELECT DISTINCT Customers.City FROM Customers WHERE Country = @Country", new { Country = country });
                foreach (var city in cities)
                    Console.WriteLine(city);
            }
            Console.ReadKey();
        }

        static void Task21()
        {
            Console.Clear();
            Console.Write("Введите ID покупателя: ");
            int customerId = int.Parse(Console.ReadLine());
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sections = db.Query<string>("SELECT S.Name FROM Sections S INNER JOIN CustomerSections CS ON S.Id = CS.SectionId WHERE CS.CustomerId = @CustomerId", new { CustomerId = customerId });
                foreach (var section in sections)
                    Console.WriteLine(section);
            }
            Console.ReadKey();
        }

        static void Task22()
        {
            Console.Clear();
            Console.Write("Введите ID раздела: ");
            int sectionId = int.Parse(Console.ReadLine());
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var promos = db.Query<Section>("SELECT Name, StartDate, EndDate FROM Sections WHERE Id = @SectionId", new { SectionId = sectionId });
                int iter = 0;
                foreach (var promo in promos)
                    Console.WriteLine($"Promotion on {promo.Name} #{++iter}:  {promo.StartDate} - {promo.EndDate}");
            }
            Console.ReadKey();
        }
    }
}

