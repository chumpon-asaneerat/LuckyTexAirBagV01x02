using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataControl.ClassData
{
    public class MathEx
    {
        #region Round
        public static decimal Round(decimal value)
        {
            decimal output = decimal.Zero;

            try
            {
                if (value != decimal.Zero)
                {
                    output = Math.Round(value, 2, MidpointRounding.AwayFromZero);
                    return output;
                }
                else
                {
                    return output;
                }
            }
            catch
            {
                return output;
            }
        }
        #endregion

        #region Round
        /// <summary>
        /// Simulate arithmatic rounding like in Excel 
        /// </summary>
        /// <param name="value">A Decimal value to round</param>
        /// <param name="digits">presision digit</param>
        /// <returns>A value rounded to decimals number of decimal places</returns>
        public static decimal Round(decimal value, int digits)
        {
            decimal scale = (decimal)Math.Pow(10.0, (double)(digits + 1));
            value = Decimal.Floor(value * scale) / scale;

            if ((Math.Floor((double)value * Math.Pow(10.0, digits)) % 2) == 0)	// check for even nearest
                value += (decimal)1.0 / scale;

            return Math.Round(value, digits);
        }

        #endregion

        #region Round
        /// <summary>
        /// Simulate arithmatic rounding like in Excel 
        /// </summary>
        /// <param name="value">A Decimal value to round</param>
        /// <param name="digits">presision digit</param>
        /// <returns>A value rounded to decimals number of decimal places</returns>
        public static decimal Round(float value, int digits)
        {
            try
            {
                decimal valueF = (decimal)Math.Round(value, digits+1);

                decimal scale = (decimal)Math.Pow(10.0, (double)(digits + 1));
                valueF = Decimal.Floor(valueF * scale) / scale;

                if ((Math.Floor((double)value * Math.Pow(10.0, digits)) % 2) == 0)	// check for even nearest
                    valueF += (decimal)1.0 / scale;

                return Math.Round(valueF, digits);
            }
            catch
            {
                return 0;
            }
        }

        #endregion

        public static decimal TruncateDecimal(decimal value, int precision)
        {
            decimal step = (decimal)Math.Pow(10, precision);
            int tmp = (int)Math.Truncate(step * value);
            return tmp / step;
        }
    }

    public class Rounding
    {
        #region RoundUp
        public static decimal RoundUp(decimal number, int places)
        {
            decimal factor = RoundFactor(places);
            number *= factor;
            number = Math.Ceiling(number);
            number /= factor;
            return number;
        }
        #endregion

        #region RoundDown
        public static decimal RoundDown(decimal number, int places)
        {
            decimal factor = RoundFactor(places);
            number *= factor;
            number = Math.Floor(number);
            number /= factor;
            return number;
        }
        #endregion

        #region RoundDownToCents
        public static decimal RoundDownToCents(decimal value, int digits)
        {
            decimal number = 0;

            decimal scale = (decimal)Math.Pow(10.0, (double)(digits + 1));
            value = Decimal.Floor(value * scale) / scale;

            if ((Math.Floor((double)value * Math.Pow(14.0, digits)) % 2) == 0)
            {
                number = RoundDown(value, digits);
            }
            else
            {
                value += (decimal)1.0 / scale;
                number = Math.Round(value, digits);
            }

            return number;
        }
        #endregion

        #region RoundFactor
        internal static decimal RoundFactor(int places)
        {
            decimal factor = 1m;

            if (places < 0)
            {
                places = -places;
                for (int i = 0; i < places; i++)
                    factor /= 10m;
            }

            else
            {
                for (int i = 0; i < places; i++)
                    factor *= 10m;
            }

            return factor;
        }
        #endregion
    }

}
