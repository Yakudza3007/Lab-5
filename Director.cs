using System;

namespace ConsoleApp1
{
    internal class Director
    {
        private int _id;
        private string _name;
        private string _country;

        public Director(int id, string name, string country)
        {
            _id = id;
            _name = name;
            _country = country;
        }

        public int Id { get { return _id; } }
        public string Name { get { return _name; } }
        public string Country { get { return _country; } }

        public override string ToString()
        {
            return $"{_id}: {_name} ({_country})";
        }
    }
}