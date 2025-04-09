using AppPersonList.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppPersonList.Models
{
    public class Person
    {
        #region Fields
        private string _name;
        private string _surname;
        private string? _email;
        private DateTime? _birthDate;
        private int? _age;
        private string _sunSign;
        private string _chineseSign;
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
                _age = CalculateAge(_birthDate.Value);
                IsAdult = _age >= 18;
                SunSign = GetSunSign();
                ChineseSign = GetChineseSign();
            }
        }

        public int? Age
        {
            get => _age;
        }
        public bool IsAdult { get; private set; }
        public string SunSign
        {
            get => _sunSign;
            set
            {
                if (_sunSign != value)
                {
                    _sunSign = value;
                }
            }
        }

        public string ChineseSign
        {
            get => _chineseSign;
            set
            {
                if (_chineseSign != value)
                {
                    _chineseSign = value;
                }
            }
        }
        #endregion

        public Person() { }

        public Person(string name, string surname, string? email, DateTime? birthDate)
        {
            Name = name;
            Surname = surname;
            Email = email;
            BirthDate = birthDate;
        }

        public static Person GenerateRandom()
        {
            var rnd = new Random();
            var name = $"Name{rnd.Next(1000)}";
            var surname = $"Surname{rnd.Next(1000)}";
            var email = $"{name.ToLower()}@gmail.com";
            var birthDate = DateTime.Today.AddYears(-rnd.Next(10, 90)).AddDays(rnd.Next(365));

            return new Person(name, surname, email, birthDate);
        }

        private static int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--;
            return age;
        }

        private string GetSunSign()
        {
            int day = BirthDate.Value.Day;
            int month = BirthDate.Value.Month;

            return month switch
            {
                1 => (day < 20) ? "Козеріг" : "Водолій",
                2 => (day < 19) ? "Водолій" : "Риби",
                3 => (day < 21) ? "Риби" : "Овен",
                4 => (day < 20) ? "Овен" : "Телець",
                5 => (day < 21) ? "Телець" : "Близнюки",
                6 => (day < 21) ? "Близнюки" : "Рак",
                7 => (day < 23) ? "Рак" : "Лев",
                8 => (day < 23) ? "Лев" : "Діва",
                9 => (day < 23) ? "Діва" : "Терези",
                10 => (day < 23) ? "Терези" : "Скорпіон",
                11 => (day < 22) ? "Скорпіон" : "Стрілець",
                12 => (day < 22) ? "Стрілець" : "Козеріг",
                _ => "Невідомо"
            };
        }

        private string GetChineseSign()
        {
            string[] animals = { "Мавпа", "Півень", "Собака", "Свиня", "Щур", "Бик", "Тигр", "Кролик", "Дракон", "Змія", "Кінь", "Коза" };
            return animals[BirthDate.Value.Year % 12];
        }

    }
}
