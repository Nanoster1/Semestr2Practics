using System;

namespace OverLoad
{
    class Program
    {
        static void Main(string[] args)
        {
            Fraction fraction1 = new Fraction(2, 1, 2);
            Fraction fraction2 = new Fraction(2, 1, 2);
            Console.WriteLine(fraction1 + fraction2);
            Console.WriteLine(fraction1 - fraction2);
            Console.WriteLine(fraction1 * fraction2);
            Console.WriteLine(fraction1 / fraction2);
            Console.WriteLine(fraction1 + 1);
            Console.WriteLine(fraction1 - 1);
            Console.WriteLine(fraction1 * 1);
            Console.WriteLine(fraction1 / 1);
        }
    }
    public class Fraction
    {
        public int WholePart { get; private set; }
        public int FractionalPart1 { get; private set; }
        public int FractionalPart2 { get; private set; }
        public Fraction(int wholePart, int fractionalPart1, int fractionalPart2)
        {
            if (fractionalPart2 == 0)
            {
                throw new Exception("Знаменатель не может быть равен 0");
            }
            WholePart = wholePart;
            FractionalPart1= fractionalPart1;
            FractionalPart2 = fractionalPart2;
        }
        public override string ToString()
        {
            if (FractionalPart2 == 1)
                WholePart += FractionalPart1;
            string str = $"{WholePart}_{FractionalPart1}_{FractionalPart2}";
            if (WholePart == 0)
                str = str.Remove(0, str.IndexOf("_") + 1);
            if (FractionalPart1 % FractionalPart2 == 0)
               str = str.Replace($"_{FractionalPart1}_{FractionalPart2}", "");
            return str;
        }
        public static Fraction operator+(Fraction fraction1, Fraction fraction2)
        {
            int wholePart = fraction1.WholePart + fraction2.WholePart;
            int fractionalPart1 = 0;
            if (fraction1.FractionalPart2 == fraction2.FractionalPart2)
            {
                fractionalPart1 = fraction1.FractionalPart1 + fraction2.FractionalPart1;
                return Shorten(new Fraction(wholePart, fractionalPart1, fraction1.FractionalPart2));
            }
            else
            {
                int fractionalPart2 = 0;
                for (int i = 1; i <= fraction1.FractionalPart2 * fraction2.FractionalPart1; i++)
                {
                    if (i%fraction1.FractionalPart2 == 0 && i%fraction2.FractionalPart1 == 0)
                    {
                        fractionalPart2 = i;
                        break;
                    }
                }
                fractionalPart1 = fraction1.FractionalPart1 * (fractionalPart2 / fraction1.FractionalPart2) + fraction2.FractionalPart2 * (fractionalPart2 / fraction2.FractionalPart2);
                return Shorten(new Fraction(wholePart, fractionalPart1, fractionalPart2));
            }
        }
        public static Fraction operator-(Fraction fraction1, Fraction fraction2)
        {
            int wholePart = fraction1.WholePart - fraction2.WholePart;
            int fractionalPart1 = 0;
            if (fraction1.FractionalPart2 == fraction2.FractionalPart2)
            {
                fractionalPart1 = fraction1.FractionalPart1 + fraction2.FractionalPart1;
                return Shorten(new Fraction(wholePart, fractionalPart1, fraction1.FractionalPart2));
            }
            else
            {
                int fractionalPart2 = 0;
                for (int i = 1; i <= fraction1.FractionalPart2 * fraction2.FractionalPart1; i++)
                {
                    if (i % fraction1.FractionalPart2 == 0 && i % fraction2.FractionalPart1 == 0)
                    {
                        fractionalPart2 = i;
                        break;
                    }
                }
                fractionalPart1 = fraction1.FractionalPart1 * (fractionalPart2 / fraction1.FractionalPart2) - fraction2.FractionalPart2 * (fractionalPart2 / fraction2.FractionalPart2);
                return Shorten(new Fraction(wholePart, fractionalPart1, fractionalPart2));
            }
        }
        public static Fraction operator*(Fraction fraction1, Fraction fraction2)
        {
            int wholePart = fraction1.WholePart * fraction2.WholePart;
            int fractionalPart1 = fraction1.FractionalPart1 * fraction2.FractionalPart1;
            int fractionalPart2 = fraction1.FractionalPart2 * fraction2.FractionalPart2;
            return Shorten(new Fraction(wholePart, fractionalPart1, fractionalPart2));
        }
        public static Fraction operator/(Fraction fraction1, Fraction fraction2)
        {
            Fraction firstFraction = new Fraction(0, fraction1.WholePart * fraction1.FractionalPart2 + fraction1.FractionalPart1, fraction1.FractionalPart2);
            Fraction secondFraction = new Fraction(0, fraction2.FractionalPart2, fraction2.WholePart * fraction2.FractionalPart2 + fraction2.FractionalPart1);
            return Shorten(firstFraction * secondFraction);
        }
        public static Fraction operator+(Fraction fraction1, int num)
        {
            Fraction fraction2 = new Fraction(num, 1, 1);
            return Shorten(fraction1 + fraction2);
        }
        public static Fraction operator-(Fraction fraction1, int num)
        {
            Fraction fraction2 = new Fraction(num, 1, 1);
            return Shorten(fraction1 - fraction2);
        }
        public static Fraction operator*(Fraction fraction1, int num)
        {
            Fraction fraction2 = new Fraction(num, 1, 1);
            return Shorten(fraction1 * fraction2);
        }
        public static Fraction operator/(Fraction fraction1, int num)
        {
            Fraction fraction2 = new Fraction(num, 1, 1);
            return Shorten(fraction1 / fraction2);
        }
        public static Fraction Shorten(Fraction fraction)
        {
            int max = 0;
            int gcd = 1;
            if (fraction.FractionalPart1 > fraction.FractionalPart2)
                max = fraction.FractionalPart1;
            else
                max = fraction.FractionalPart2;
            for (int i = 0; i <= max; i++)
            {
                if (fraction.FractionalPart1 % max == 0 && fraction.FractionalPart2 % max == 0)
                    gcd = max;
            }
            fraction.FractionalPart1 /= gcd;
            fraction.FractionalPart2 /= gcd;
            return fraction;
        }
    }
}
