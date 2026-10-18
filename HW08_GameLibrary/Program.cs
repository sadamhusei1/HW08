using System;
using System.Collections.Generic;
using System.Globalization;

namespace HW08_GameLibrary
{
    // Клас, що описує одну гру
    internal class Game
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public double Rating { get; set; } // з 10
        public bool IsCompleted { get; set; }

        // Конструктор з 5 параметрами
        public Game(string title, string genre, int year, double rating, bool isCompleted)
        {
            Title = title;
            Genre = genre;
            Year = year;
            Rating = rating;
            IsCompleted = isCompleted;
        }

        public void Print()
        {
            // InvariantCulture, щоб рейтинг завжди був з крапкою (9.0, а не 9,0)
            string rating = Rating.ToString("F1", CultureInfo.InvariantCulture);
            Console.WriteLine($"[{Title}] жанр: {Genre}, рік: {Year}, рейтинг: {rating}/10, пройдена: {IsCompleted}");
        }
    }

    // Клас-колекція ігор
    internal class GameLibrary
    {
        private List<Game> games = new List<Game>();

        public void Add(Game g)
        {
            games.Add(g);
        }

        public void ShowAll()
        {
            foreach (Game g in games)
            {
                g.Print();
            }
        }

        public int CountCompleted()
        {
            int count = 0;
            foreach (Game g in games)
            {
                if (g.IsCompleted)
                {
                    count++;
                }
            }
            return count;
        }

        public double AverageRating()
        {
            if (games.Count == 0)
            {
                return 0;
            }

            double sum = 0;
            foreach (Game g in games)
            {
                sum += g.Rating;
            }
            return sum / games.Count;
        }

        public Game? FindHighestRated()
        {
            Game? best = null;
            foreach (Game g in games)
            {
                if (best == null || g.Rating > best.Rating)
                {
                    best = g;
                }
            }
            return best;
        }

        // Бонус: сортування за спаданням рейтингу
        public void SortByRating()
        {
            games.Sort((x, y) => y.Rating.CompareTo(x.Rating));
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            GameLibrary library = new GameLibrary();

            // Додаємо 5 улюблених ігор
            library.Add(new Game("S.T.A.L.K.E.R.: Тінь Чорнобиля", "Шутер/RPG", 2007, 9.5, true));
            library.Add(new Game("Minecraft", "Пісочниця", 2011, 9.0, true));
            library.Add(new Game("Genshin Impact", "RPG", 2020, 8.5, false));
            library.Add(new Game("Fortnite", "Battle Royale", 2017, 7.5, false));
            library.Add(new Game("Counter-Strike 2", "Шутер", 2023, 8.0, false));

            Console.WriteLine("=== Усі ігри ===");
            library.ShowAll();

            Console.WriteLine();
            Console.WriteLine($"Пройдено ігор: {library.CountCompleted()}");
            Console.WriteLine($"Середній рейтинг: {library.AverageRating():F2}");

            Console.WriteLine();
            Console.WriteLine("=== Найкраща гра за рейтингом ===");
            Game? best = library.FindHighestRated();
            if (best != null)
            {
                best.Print();
            }

            // Бонус
            Console.WriteLine();
            Console.WriteLine("=== Сортування за рейтингом (спадання) ===");
            library.SortByRating();
            library.ShowAll();

            Console.WriteLine();
            Console.WriteLine("Натисніть будь-яку клавішу для виходу…");
            Console.ReadKey();
        }
    }
}
