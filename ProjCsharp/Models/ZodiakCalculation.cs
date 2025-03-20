using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppZodiac.Models
{
    internal static class ZodiakCalculation
    {
        internal static string CalcWesternZodiac(DateTime birthDate)
        {
            int day = birthDate.Day;
            int month = birthDate.Month;

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

        internal static string CalcChineseZodiac(DateTime birthDate)
        {
            string[] animals = { "Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat" };
            return animals[birthDate.Year % 12];
        }

        internal static string CalcWesternZodiacPrediction(string zodiacSign)
        {
            return zodiacSign switch
            {
                "Capricorn" => "Hard work leads to success, but balance is necessary. Take time to rest and reflect on your achievements, as perseverance without self-care can lead to burnout.",
                "Aquarius" => "Innovation is your strength; embrace unconventional paths. Your unique approach will inspire others, but ensure you remain grounded in reality to turn ideas into action.",
                "Pisces" => "Creativity flourishes, but grounding yourself is essential. Dreams and imagination will guide you, but clarity of purpose and steady progress will bring true fulfillment.",
                "Aries" => "Energy and ambition will bring success, but patience is key. Acting too quickly may lead to mistakes; take the time to plan before charging ahead.",
                "Taurus" => "Stability is your strength, but don’t fear change—it may bring growth. Comfort zones are tempting, but stepping outside of them will lead to new and exciting opportunities.",
                "Gemini" => "Your curiosity leads to new opportunities, but focus is needed. While your versatility is a gift, setting clear priorities will help you achieve meaningful results.",
                "Cancer" => "Emotional balance is crucial; trust your intuition. Strength lies in vulnerability, and by embracing your emotions, you can build stronger connections with those around you.",
                "Leo" => "Charisma attracts success, but humility will sustain it. Your natural leadership shines, but listening to others and valuing teamwork will take you even further.",
                "Virgo" => "Precision and planning pay off, but don’t overthink. Perfectionism can be a double-edged sword; trust in your abilities and allow yourself to move forward with confidence.",
                "Libra" => "Harmony is within reach, but clear decisions are needed. Striving for balance is important, but making firm choices will empower you to shape your destiny.",
                "Scorpio" => "Passion fuels progress, but guard against stubbornness. Your determination is unmatched, but learning to adapt and compromise will lead to even greater success.",
                "Sagittarius" => "Adventure calls, but responsibility should not be ignored. Explore new horizons with enthusiasm, but remember to honor your commitments along the way.",
                _ => "Prediction is not found."
            };
        }

        internal static string CalcChineseZodiacPrediction(string chineseZodiac)
        {
            return chineseZodiac switch
            {
                "Monkey" => "Intelligence shines, but avoid being too impulsive. Your quick thinking will open doors, but patience and careful planning will ensure long-term success.",
                "Rooster" => "Hard work will pay off; attention to detail is crucial. Stay disciplined and committed, as small efforts will accumulate into significant achievements.",
                "Dog" => "Loyalty is rewarded, but self-care is just as important. Helping others is admirable, but remember to nurture your own well-being and set boundaries when needed.",
                "Pig" => "A peaceful year, but stay proactive in achieving your goals. Enjoy life’s pleasures, but don’t let comfort prevent you from pursuing your dreams with determination.",
                "Rat" => "Clever ideas lead to success, but avoid unnecessary risks. Your resourcefulness is a powerful tool, but carefully weighing your options will bring better outcomes.",
                "Ox" => "Steady efforts will bring rewards; patience is your ally. Perseverance will lead you to success, and consistency will be the key to unlocking new opportunities.",
                "Tiger" => "Confidence will open doors, but don’t rush into things. Bold action is favored, but taking time to strategize will ensure you make the right moves at the right time.",
                "Rabbit" => "Diplomacy will help you navigate challenges smoothly. Your kindness and tact will build strong relationships, leading to mutual success and growth.",
                "Dragon" => "A year of transformation — embrace change with courage. The opportunities ahead will reshape your life, and your boldness will determine how high you soar.",
                "Snake" => "Your year! Opportunities arise, but choose wisely. Luck is on your side, but thoughtful decisions and calculated risks will be necessary to make the most of it.",
                "Horse" => "Fast progress is possible, but persistence is required. Your enthusiasm will drive you forward, but steady determination will be what turns ideas into reality.",
                "Goat" => "Creativity and kindness will bring unexpected rewards. Express yourself freely and maintain a compassionate heart, as generosity will bring joy in return.",
                _ => "Prediction is not found."
            };
        }


    }
}
