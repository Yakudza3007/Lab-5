using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    internal class DatabaseManager
    {
        private List<Film> _films;
        private List<Director> _directors;
        private List<Genre> _genres;

        public DatabaseManager()
        {
            _films = new List<Film>();
            _directors = new List<Director>();
            _genres = new List<Genre>();
        }

        public void SetData(List<Film> f, List<Director> d, 
            List<Genre> g)
        {
            _films = f;
            _directors = d;
            _genres = g;
        }

        public List<Film> Query1()
        {
            var result = from f in _films
                         where f.Year > 2000 
                         && f.Duration > 7200
                         select f;
            return result.ToList();
        }

        public decimal Query2(int directorId)
        {
            var result = from f in _films
                         join d in _directors 
                         on f.DirectorId equals d.Id
                         where d.Id == directorId
                         select f.BoxOffice;
            return result.Sum();
        }

        public List<string> Query3(string genreName, 
            string directorName)
        {
            var result = from f in _films
                         join g in _genres 
                         on f.GenreId equals g.Id
                         join d in _directors 
                         on f.DirectorId equals d.Id
                         where g.Name == genreName 
                         && d.Name == directorName
                         select f.Title;
            return result.ToList();
        }

        public double Query4(string genreName, 
            string countryName)
        {
            var result = from f in _films
                         join g in _genres 
                         on f.GenreId equals g.Id
                         join d in _directors 
                         on f.DirectorId equals d.Id
                         where g.Name == genreName 
                         && d.Country == countryName
                         select (double)f.Budget;
            return result.Average();
        }
    }
}