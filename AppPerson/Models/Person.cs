using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPerson.Models
{
    internal class Person
    {
        #region Fields
        private string _name;
        private string _surname;
        private string? _email;
        private DateTime? _birthDate;
        private int? _age;
        #endregion

        #region Properties
        public string Name { get => _name; set => _name = value; }
        public string Surname { get => _surname; set => _surname = value; }
        public string? Email { get => _email; set => _email = value; }
        public DateTime? BirthDate { get => _birthDate; set => _birthDate = value; }
        public int? Age { get => _age;}
        #endregion

        public Person(string name, string surname, string? email, DateTime? birthDate)
        {
            Thread.Sleep(2000); //artificial delay
            Name = name;
            Surname = surname;
            Email = email;
            BirthDate = birthDate;
            if (BirthDate.HasValue)
            {
                _age = CalculateAge(BirthDate.Value);
            }
        }

        public Person(string name, string surname, DateTime birthDate) : this(name, surname, null, birthDate){}

        public Person(string name, string surname, string email) : this(name, surname, email, null){}


        public bool isAdult()
        {

            if (BirthDate.HasValue && _age < 18) {
                return false;
            }

            return true;
        }

        public string SunSign()
        {
            if(!BirthDate.HasValue)
            {
                return "Unknown";
            }

            int day = BirthDate.Value.Day;
            int month = BirthDate.Value.Month;

            return month switch
            {
                1 => (day < 20) ? "Capricorn" : "Aquarius",
                2 => (day < 19) ? "Aquarius" : "Pisces",
                3 => (day < 21) ? "Pisces" : "Aries",
                4 => (day < 20) ? "Aries" : "Taurus",
                5 => (day < 21) ? "Taurus" : "Gemini",
                6 => (day < 21) ? "Gemini" : "Cancer",
                7 => (day < 23) ? "Cancer" : "Leo",
                8 => (day < 23) ? "Leo" : "Virgo",
                9 => (day < 23) ? "Virgo" : "Libra",
                10 => (day < 23) ? "Libra" : "Scorpio",
                11 => (day < 22) ? "Scorpio" : "Sagittarius",
                12 => (day < 22) ? "Sagittarius" : "Capricorn",
                _ => "Unknown"
            };
        }

        public string ChineseSign()
        {
            if (!BirthDate.HasValue)
            {
                return "Unknown";
            }

            string[] animals = { "Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat" };
            return animals[BirthDate.Value.Year % 12];
        }

        public bool IsBirthday()
        {
            if (BirthDate.HasValue && BirthDate.Value.DayOfYear == DateTime.Today.DayOfYear)
            {
                return true;
            }

            return false;
        }

        private int CalculateAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
