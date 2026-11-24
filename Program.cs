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
            ExcelManager excel = new ExcelManager("LR5-var14.xlsx");
            DatabaseManager db = new DatabaseManager();
            
            bool isRunning = true;
            string userChoice = "";
            List<Film> films = new List<Film>();
            List<Director> dirs = new List<Director>();
            List<Genre> genres = new List<Genre>();

            try
            {
                films = excel.ReadFilms();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
                return;
            }

            dirs.Add(new Director(1, "Мартин Скорсезе", "США"));
            dirs.Add(new Director(2, "Уэс Андерсон", "США"));
            dirs.Add(new Director(3, "Клинт Иствуд", "США"));
            dirs.Add(new Director(4, "Квентин Тарантино", "США"));
            dirs.Add(new Director(5, "Дени Вильнёв", "Канада"));
            dirs.Add(new Director(6, "Даррен Аронофски", "США"));
            dirs.Add(new Director(7, "Ларс Фон Триер", "Дания"));
            dirs.Add(new Director(8, "Стивен Спилберг", "США"));
            dirs.Add(new Director(9, "Ридли Скотт", 
                "Великобритания"));
            dirs.Add(new Director(10, "Паоло Соррентино", 
                "Италия"));
            dirs.Add(new Director(11, "Андрей Звягинцев", 
                "Россия"));
            dirs.Add(new Director(12, "Андрей Тарковский", "СССР"));
            dirs.Add(new Director(13, "Сергей Эйзенштейн", 
                "СССР"));
            dirs.Add(new Director(14, "Стэнли Кубрик", "США"));
            dirs.Add(new Director(15, "Кристофер Нолан", 
                "Великобритания"));
            dirs.Add(new Director(16, "Дэвид Финчер", "США"));
            dirs.Add(new Director(17, "Фрэнк Капра", "Италия"));
            dirs.Add(new Director(18, "Пон Джун-хо", 
                "Южная Корея"));
            dirs.Add(new Director(19, "Альфред Хичкок", 
                "Великобритания"));
            dirs.Add(new Director(20, "Алексей Балабанов", 
                "Россия"));
            dirs.Add(new Director(21, "Хлоя Чжао", "Китай"));
            dirs.Add(new Director(22, "Тони Кэй", 
                "Великобритания"));
            dirs.Add(new Director(23, "Роберт Земекис", "США"));

            genres.Add(new Genre(1, "Триллер"));
            genres.Add(new Genre(2, "Комедия"));
            genres.Add(new Genre(3, "Драма"));
            genres.Add(new Genre(4, "Криминал"));
            genres.Add(new Genre(5, "Мелодрама"));
            genres.Add(new Genre(6, "Мультфильм"));
            genres.Add(new Genre(7, "Вестерн"));
            genres.Add(new Genre(8, "Боевик"));
            genres.Add(new Genre(9, "Фантастика"));
            genres.Add(new Genre(10, "Ужас"));
            genres.Add(new Genre(11, "Приключения"));
            genres.Add(new Genre(12, "Исторический"));
            genres.Add(new Genre(13, "Детектив"));

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
            int i = 0;
            for (i = 0; i < films.Count; i++)
            {
                Console.WriteLine(films[i]);
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

            id = c.ReadInt("Введите ID: ");
            title = c.ReadString("Введите название: ");
            
            dirName = c.ReadString("Введите имя режиссера: ");
            var findDir = from d in dirs
                          where d.Name.ToLower() == 
                                dirName.ToLower()
                          select d;
            foundDir = findDir.FirstOrDefault();

            if (foundDir == null)
            {
                Console.WriteLine("Ошибка: такого режиссера нет.");
                return;
            }

            year = c.ReadInt("Введите год: ");

            genName = c.ReadString("Введите название жанра: ");
            var findGen = from g in genres
                          where g.Name.ToLower() == 
                                genName.ToLower()
                          select g;
            foundGen = findGen.FirstOrDefault();

            if (foundGen == null)
            {
                Console.WriteLine("Ошибка: такого жанра нет.");
                return;
            }

            dur = c.ReadInt("Введите длительность: ");
            bud = (decimal)c.ReadDouble("Введите бюджет: ");
            box = (decimal)c.ReadDouble("Введите сборы: ");

            films.Add(new Film(id, title, foundDir.Id, year, 
                foundGen.Id, dur, bud, box));
                
            Console.WriteLine("Фильм успешно добавлен.");
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
            Console.WriteLine("\nЗапрос 1 (После 2000 г. > 2 часов):");
            List<Film> q1 = db.Query1();
            int i = 0;
            for (i = 0; i < q1.Count; i++)
            {
                Console.WriteLine(q1[i]);
            }

            Console.WriteLine("\nЗапрос 2 (Сборы реж. ID=1):");
            Console.WriteLine(db.Query2(1));

            Console.WriteLine("\nЗапрос 3 (Фантастика Нолана):");
            List<string> q3 = db.Query3("Фантастика", 
                "Кристофер Нолан");
            for (i = 0; i < q3.Count; i++)
            {
                Console.WriteLine(q3[i]);
            }

            Console.WriteLine("\nЗапрос 4 (Средний бюджет Фантастики США):");
            Console.WriteLine(db.Query4("Фантастика", "США"));
        }
    }
}