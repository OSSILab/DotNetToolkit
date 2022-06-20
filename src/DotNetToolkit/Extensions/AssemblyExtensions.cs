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

using System.IO;

namespace System.Reflection
{
    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) methods for working with specific
    /// kinds of <see cref="Assembly"/> instances.
    /// </summary>
    public static class AssemblyExtensions
    {

        /// <summary>
        /// Gets the directory path of the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly for which the location needs to be retrieved.</param>
        /// <returns>The directory path of the specified assembly.</returns>
        public static string GetDirectoryPath(this Assembly assembly)
        {
            string assemblyLocation;
            try
            {
                assemblyLocation = assembly.Location;
            }
            catch (NotSupportedException e)
            {
                throw new Exception("The provided assembly is a dynamic assembly", e);
            }
            if (string.IsNullOrWhiteSpace(assemblyLocation))
            {
                throw new Exception("The provided assembly is loaded from a byte array");
            }
            if (assemblyLocation[0] == '\\' || assemblyLocation[0] == '/')
            {
                int escapeCharIndex = assemblyLocation.LastIndexOf(assemblyLocation[0]);
                if (escapeCharIndex == -1)
                {
                    throw new Exception($"Unknown assembly location format:{assemblyLocation}");
                }
                return assemblyLocation.Substring(0, escapeCharIndex);
            }
            else
            {
                return Path.GetDirectoryName(assemblyLocation);
            }
        }
    }
}