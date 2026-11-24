using System;

namespace ConsoleApp1
{
    internal class Film
    {
        private int _id;
        private string _title;
        private int _directorId;
        private int _year;
        private int _genreId;
        private int _duration;
        private decimal _budget;
        private decimal _boxOffice;

        public Film(int id, string title, int directorId, 
            int year, int genreId, int duration, 
            decimal budget, decimal boxOffice)
        {
            _id = id;
            _title = title;
            _directorId = directorId;
            _year = year;
            _genreId = genreId;
            _duration = duration;
            _budget = budget;
            _boxOffice = boxOffice;
        }

        public int Id { get { return _id; } }
        public string Title { get { return _title; } }
        public int DirectorId { get { return _directorId; } }
        public int Year { get { return _year; } }
        public int GenreId { get { return _genreId; } }
        public int Duration { get { return _duration; } }
        public decimal Budget { get { return _budget; } }
        public decimal BoxOffice { get { return _boxOffice; } }

        public override string ToString()
        {
            string res = $"{_id}: {_title} ({_year})";
            res += $" | Бюджет: {_budget}";
            return res;
        }
    }
}