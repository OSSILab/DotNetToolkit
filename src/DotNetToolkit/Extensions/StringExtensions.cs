/*************************************************************************************
* The MIT License(MIT)                                                               *
*                                                                                    *
* Copyright(c) Open Source Software Initiative Contributors                          *
*                                                                                    *
* Permission is hereby granted, free of charge, to any person obtaining a copy       *
* of this software and associated documentation files (the “Software”), to deal      *
* in the Software without restriction, including without limitation the rights       *
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies   *
* of the Software, and to permit persons to whom the Software is furnished to do so. *
*                                                                                    *
* THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS            *
* OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,        *
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE        *
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,  *
* WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN    *
* CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.         *
*************************************************************************************/

using System.Linq;

namespace System
{
    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) methods for working with specific
    /// kinds of <see cref="string"/> instances.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Indicates whether the specified string is empty or consists only of white-space characters.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns><c>true</c> if the <paramref name="value"/> is empty or consists exclusively of white-space characters, <c>false</c> otherwise.</returns>
        ///
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public static bool IsEmptyOrWhiteSpace(this string value)
        {
            if(value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            return value.All(char.IsWhiteSpace);
        }
    }
}