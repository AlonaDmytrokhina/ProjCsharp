using AppZodiac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppZodiac.ViewModels
{
    internal class ZodiacViewModel
    {
        public DateTime BirthDate { get; }
        public int Age { get; }
        public string WesternZodiac { get; }
        public string ChineseZodiac { get; }
        public string WesternZodiacPrediction { get; }
        public string ChineseZodiacPrediction { get; }

        public ZodiacViewModel(DateTime birthDate)
        {
            BirthDate = birthDate;
            Age = AgeCalc.CalculateAge(birthDate);
            WesternZodiac = GetWesternZodiac(BirthDate);
            ChineseZodiac = GetChineseZodiac(BirthDate);
            WesternZodiacPrediction = GetWesternZodiacPrediction(WesternZodiac);
            ChineseZodiacPrediction = GetChineseZodiacPrediction(ChineseZodiac);
        }

        private string GetWesternZodiac(DateTime birthDate)
        {
            return ZodiakCalculation.CalcWesternZodiac(birthDate);
        }

        private string GetChineseZodiac(DateTime birthDate)
        {
            return ZodiakCalculation.CalcChineseZodiac(birthDate);
        }

        private string GetWesternZodiacPrediction(string zodiac)
        {
            return ZodiakCalculation.CalcWesternZodiacPrediction(zodiac);
        }

        private string GetChineseZodiacPrediction(string zodiac)
        {
            return ZodiakCalculation.CalcChineseZodiacPrediction(zodiac);
        }
    }
}
