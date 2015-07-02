using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace TCR.Lib.Utility
{
    public static class RandomFactory
    {
        //minValue is inclusive and maxValue is exclusive!
        //http://msdn.microsoft.com/en-us/library/2dx6wyd4.aspx
        public static int GenerateInteger(int minValue, int maxValue)
        {
            byte[] bytes = new byte[1];
            RandomNumberGenerator.Create().GetBytes(bytes);
            Random random = new Random(bytes[0]);
            int randomNumber = random.Next(minValue, maxValue);
            return randomNumber;
        }

        public static decimal GenerateDecimal(int minValue, int maxValue, int digits)
        {
            byte[] bytes = new byte[1];
            RandomNumberGenerator.Create().GetBytes(bytes);
            Random random = new Random(bytes[0]);
            int randomInt = random.Next(minValue, maxValue);
            double randomDbl = random.NextDouble();

            return (decimal) (randomInt + Math.Round(randomDbl, digits));
        }

        public static bool GenerateBoolean()
        {
            int number = GenerateInteger(0, 2);
            return Convert.ToBoolean(number);
        }

        public static DateTime GenerateDateTime(DateTime minValue, DateTime maxValue)
        {
            byte[] bytes = new byte[1];
            RandomNumberGenerator.Create().GetBytes(bytes);
            Random random = new Random(bytes[0]);
            double randomDbl = random.NextDouble();

            TimeSpan timeSpan = maxValue - minValue;
            TimeSpan randomSpan = new TimeSpan((long)(timeSpan.Ticks * randomDbl));
            return minValue + randomSpan;
        }

        public static TEnum GenerateEnum<TEnum>()
        {
            Type enumType;
            if (IsNullable(typeof(TEnum)))
                enumType = Nullable.GetUnderlyingType(typeof(TEnum));
            else
                enumType = typeof(TEnum);

            if (!enumType.IsEnum)
                throw new Exception("Generic parameter must be an enum.");

            List<TEnum> enumMembers = enumType.GetFields()
                .Where(f => f.IsLiteral)
                .Select(f => (TEnum) Enum.Parse(enumType, f.GetValue(null).ToString(), false))
                .ToList();

            TEnum randomEnum = enumMembers.OrderBy(x => Guid.NewGuid()).Take(1).FirstOrDefault();
            return randomEnum;
        }

        #region Helper Methods

        public static bool IsNullable(Type type)
        {
            bool isNullable;

            try
            {
                isNullable = type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Nullable<>));
            }
            catch
            {
                isNullable = false;
            }

            return isNullable;
        }

        #endregion
    }
}