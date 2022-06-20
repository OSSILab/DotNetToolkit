using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DotNetToolkit.Tests
{
    static class Extensions
    {
        public static IEnumerable<object> ToGenericEnumerable(this IEnumerable enumerable)
        {
            return enumerable.Cast<object>();
        }

        public static uint Factorial(this uint number)
        {
            uint fact = 1;
            for (uint i = 2; i <= number; i++)
            {
                fact *= i;
            }
            return fact;
        }
    }
}