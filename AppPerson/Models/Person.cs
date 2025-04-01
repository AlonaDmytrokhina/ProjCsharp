using System;
using System.Text.RegularExpressions;
using AppPerson.ExceptionHandling;

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
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, @"^[A-Za-zА-Яа-яЇїІіЄєҐґ'-]+$"))
                {
                    throw new UnrealisticNameException(value);
                }
                _name = value;
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, @"^[A-Za-zА-Яа-яЇїІіЄєҐґ'-]+$"))
                {
                    throw new UnrealisticNameException(value);
                }
                _surname = value;
            }
        }

        public string? Email
        {
            get => _email;
            set
            {
                if (!string.IsNullOrEmpty(value) && !Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    throw new InvalidEmailException(value);
                }
                _email = value;
            }
        }

        public DateTime? BirthDate
        {
            get => _birthDate;
            set
            {
                if (!value.HasValue) throw new ArgumentNullException(nameof(BirthDate), "Birth date cannot be null.");

                if (value > DateTime.Today)
                {
                    throw new FutureBirthDateException(value.Value);
                }
                if (value < new DateTime(1900, 1, 1))
                {
                    throw new UnrealisticBirthDateException(value.Value);
                }

                _birthDate = value;
                _age = CalculateAge(value.Value);

                if (_age < 18)
                {
                    throw new IsNotAdultException();
                }
            }
        }

        public int? Age
        {
            get => _age;
        }
        #endregion

        #region Constructors
        public Person(string name, string surname, string? email, DateTime? birthDate)
        {
            Name = name;
            Surname = surname;
            Email = email;
            BirthDate = birthDate;
        }

        public Person(string name, string surname, DateTime birthDate) : this(name, surname, null, birthDate) { }

        public Person(string name, string surname, string email) : this(name, surname, email, null) { }
        #endregion

        #region Methods
        public bool isAdult()
        {

            if (BirthDate.HasValue && _age < 18)
            {
                return false;
            }

            return true;
        }

        public string SunSign()
        {
            if (!BirthDate.HasValue) return "Unknown";

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
            if (!BirthDate.HasValue) return "Unknown";

            string[] animals = { "Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat" };
            return animals[BirthDate.Value.Year % 12];
        }

        public bool IsBirthday()
        {
            Thread.Sleep(2000); // artificial delay
            return BirthDate.HasValue && BirthDate.Value.DayOfYear == DateTime.Today.DayOfYear;
        }

        private int CalculateAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }
        #endregion
    }
}
