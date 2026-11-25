using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Checked c = new Checked();
            ExcelManager excel = 
                new ExcelManager("LR5-var14.xlsx");
            DatabaseManager db = new DatabaseManager();
            
            bool isRunning = true;
            string userChoice = "";
            List<Film> films = new List<Film>();
            List<Director> dirs = new List<Director>();
            List<Genre> genres = new List<Genre>();

            try
            {
                films = excel.ReadFilms();
                dirs = excel.ReadDirectors();
                genres = excel.ReadGenres();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
                return;
            }

            db.SetData(films, dirs, genres);

            while (isRunning)
            {
                Console.WriteLine("\n0 - Выход");
                Console.WriteLine("1 - Просмотр фильмов");
                Console.WriteLine("2 - Добавить фильм");
                Console.WriteLine("3 - Удалить фильм");
                Console.WriteLine("4 - Выполнить LINQ-запросы");
                Console.Write("Выберите действие: ");
                userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case "0":
                        isRunning = false;
                        break;
                    case "1":
                        ShowData(films);
                        break;
                    case "2":
                        AddFilm(c, films, dirs, genres);
                        excel.SaveFilms(films);
                        db.SetData(films, dirs, genres);
                        break;
                    case "3":
                        DeleteFilm(c, films);
                        excel.SaveFilms(films);
                        db.SetData(films, dirs, genres);
                        break;
                    case "4":
                        RunQueries(db);
                        break;
                    default:
                        Console.WriteLine("Нет такого пункта.");
                        break;
                }
            }
        }

        private static void ShowData(List<Film> films)
        {
            int index = 0;
            for (index = 0; index < films.Count; index++)
            {
                Console.WriteLine(films[index]);
            }
        }

        private static void AddFilm(Checked c, List<Film> films, 
            List<Director> dirs, List<Genre> genres)
        {
            int id = 0;
            string title = "";
            string dirName = "";
            int year = 0;
            string genName = "";
            int dur = 0;
            decimal bud = 0;
            decimal box = 0;
            Director foundDir = null;
            Genre foundGen = null;
            int newId = 0;

            id = c.ReadInt("Введите ID фильма: ");
            title = c.ReadString("Введите название: ");
            
            dirName = c.ReadString("Введите имя режиссера: ");
            
            foundDir = dirs.FirstOrDefault(d => 
                d.Name.ToLower() == dirName.ToLower());

            if (foundDir == null)
            {
                newId = dirs.Max(d => d.Id) + 1;
                foundDir = new Director(
                    newId, dirName, "Неизвестно");
                dirs.Add(foundDir);
                Console.WriteLine(
                    $"Режиссер добавлен. ID = {newId}");
            }

            year = c.ReadInt("Введите год: ");

            genName = c.ReadString("Введите название жанра: ");
            
            foundGen = genres.FirstOrDefault(g => 
                g.Name.ToLower() == genName.ToLower());

            if (foundGen == null)
            {
                newId = genres.Max(g => g.Id) + 1;
                foundGen = new Genre(newId, genName);
                genres.Add(foundGen);
                Console.WriteLine(
                    $"Жанр добавлен. ID = {newId}");
            }

            dur = c.ReadInt("Введите длительность: ");
            bud = (decimal)c.ReadDouble("Введите бюджет: ");
            box = (decimal)c.ReadDouble("Введите сборы: ");

            films.Add(new Film(id, title, foundDir.Id, year, 
                foundGen.Id, dur, bud, box));
                
            Console.WriteLine("Фильм успешно добавлен!");
        }

        private static void DeleteFilm(Checked c, List<Film> films)
        {
            int id = c.ReadInt("Введите ID для удаления: ");
            int removed = films.RemoveAll(f => f.Id == id);
            
            if (removed > 0)
                Console.WriteLine("Фильм удален.");
            else
                Console.WriteLine("Фильм не найден.");
        }

        private static void RunQueries(DatabaseManager db)
        {
            List<Film> q1 = new List<Film>();
            List<string> q3 = new List<string>();
            int index = 0;

            Console.WriteLine("\nЗапрос 1 (После 2000 г. > 2ч):");
            q1 = db.Query1();
            for (index = 0; index < q1.Count; index++)
            {
                Console.WriteLine(q1[index]);
            }

            Console.WriteLine("\nЗапрос 2 (Сборы реж. ID=15):");
            Console.WriteLine(db.Query2(15));

            Console.WriteLine("\nЗапрос 3 (Фантастика Нолана):");
            q3 = db.Query3("Фантастика", "Кристофер Нолан");
            for (index = 0; index < q3.Count; index++)
            {
                Console.WriteLine(q3[index]);
            }

            Console.WriteLine("\nЗапрос 4 (Ср. бюджет Фантастики США):");
            Console.WriteLine(db.Query4("Фантастика", "США"));
        }
    }
}