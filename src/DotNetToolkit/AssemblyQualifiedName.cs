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

namespace System.Reflection
{
    /// <summary>
    /// A utility class that provides the ability to parse type assembly qualified names.
    /// </summary>
    public static class AssemblyQualifiedName
    {
        /// <summary>
        /// Extracts the assembly name from an assembly qualified name.
        /// For example 'System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e'
        /// will be parsed with 'System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e' assembly name.
        /// </summary>
        /// <param name="assemblyQualifiedName"><see cref="Type.AssemblyQualifiedName"/></param>
        /// <param name="assemblyName"><see cref="AssemblyName"/></param>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="assemblyQualifiedName"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="assemblyQualifiedName"/> is empty or has an invalid format.</exception>
        public static void GetAssemblyName(string assemblyQualifiedName, out AssemblyName assemblyName)
        {
            Parse(assemblyQualifiedName, true, false, out assemblyName, out _);
        }

        /// <summary>
        /// Extracts the assembly name from an assembly qualified name.
        /// For example 'System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e'
        /// will be parsed with 'System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e' assembly name.
        /// </summary>
        /// <param name="assemblyQualifiedName"><see cref="Type.AssemblyQualifiedName"/></param>
        /// <param name="assemblyName"><see cref="AssemblyName"/></param>
        /// <returns><c>True</c> if extraction succeed or <c>False</c> otherwise.</returns>
        public static bool TryGetAssemblyName(string assemblyQualifiedName, out AssemblyName assemblyName)
        {
            return Parse(assemblyQualifiedName, false, false, out assemblyName, out _);
        }

        /// <summary>
        /// Extracts the type name from an assembly qualified name.
        /// For example 'System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e'
        /// will be parsed with 'System.String' as type name.
        /// </summary>
        /// <param name="assemblyQualifiedName"><see cref="Type.AssemblyQualifiedName"/></param>
        /// <param name="parsedTypeName"><see cref="Type.FullName"/></param>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="assemblyQualifiedName"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="assemblyQualifiedName"/> is empty or has an invalid format.</exception>
        public static void GetTypeName(string assemblyQualifiedName, out string parsedTypeName)
        {
            Parse(assemblyQualifiedName, true, true, out _, out parsedTypeName);
        }

        /// <summary>
        /// Extracts the type name from an assembly qualified name.
        /// For example 'System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e'
        /// will be parsed with 'System.String' as type name.
        /// </summary>
        /// <param name="assemblyQualifiedName"><see cref="Type.AssemblyQualifiedName"/></param>
        /// <param name="parsedTypeName"><see cref="Type.FullName"/></param>
        /// <returns><c>True</c> if extraction succeed or <c>False</c> otherwise.</returns>
        public static bool TryGetTypeName(string assemblyQualifiedName, out string parsedTypeName)
        {
            return Parse(assemblyQualifiedName, false, true, out _, out parsedTypeName);
        }

        /// <summary>
        /// Extracts the assembly name and type name from an assembly qualified name.
        /// For example 'System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e'
        /// will be parsed with 'System.String' as type name and 'System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e' the assembly name.
        /// </summary>
        /// <param name="assemblyQualifiedName"><see cref="Type.AssemblyQualifiedName"/></param>
        /// <param name="assemblyName"><see cref="AssemblyName"/></param>
        /// <param name="parsedTypeName"><see cref="Type.FullName"/></param>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="assemblyQualifiedName"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="assemblyQualifiedName"/> is empty or has an invalid format.</exception>
        public static void GetAssemblyNameAndTypeName(string assemblyQualifiedName, out AssemblyName assemblyName, out string parsedTypeName)
        {
            Parse(assemblyQualifiedName, true, true, out assemblyName, out parsedTypeName);
        }


        /// <summary>
        /// Extracts the assembly name and type name from an assembly qualified name.
        /// For example 'System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e'
        /// will be parsed with 'System.String' as type name and 'System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e' the assembly name.
        /// </summary>
        /// <param name="assemblyQualifiedName"><see cref="Type.AssemblyQualifiedName"/></param>
        /// <param name="assemblyName"><see cref="AssemblyName"/></param>
        /// <param name="parsedTypeName"><see cref="Type.FullName"/></param>
        /// <returns><c>True</c> if extraction succeed or <c>False</c> otherwise.</returns>
        public static bool TryGetAssemblyNameAndTypeName(string assemblyQualifiedName, out AssemblyName assemblyName, out string parsedTypeName)
        {
            return Parse(assemblyQualifiedName, false, true, out assemblyName, out parsedTypeName);
        }

        private static bool GetAssemblyName(string assemblyQualifiedName, bool throwException, out AssemblyName parsedAssemblyName)
        {
            if (string.IsNullOrWhiteSpace(assemblyQualifiedName))
            {
                if (throwException)
                {
                    if (assemblyQualifiedName == null)
                    {
                        throw new ArgumentNullException($"{nameof(assemblyQualifiedName)}");
                    }
                    throw new ArgumentException($"{nameof(assemblyQualifiedName)} cannot be empty.");
                }
                parsedAssemblyName = null;
                return false;
            }

            int lastSquireBracketIndex = assemblyQualifiedName.LastIndexOf(']');
            if (lastSquireBracketIndex != -1)
            {
                int assemblyStartIndex = assemblyQualifiedName.IndexOf(',', lastSquireBracketIndex);
                if (assemblyStartIndex == -1)
                {
                    if (throwException)
                    {
                        throw new ArgumentException($"{nameof(assemblyQualifiedName)} must contain the assembly name.");
                    }
                    parsedAssemblyName = null;
                    return false;
                }
            }
            AssemblyName assemblyNameToReturn = null;
            Assembly LocalGetAssembly(AssemblyName assemblyName)
            {
                assemblyNameToReturn = assemblyName;
                return null;
            }

            try
            {
                Type.GetType(assemblyQualifiedName, LocalGetAssembly, null, false);
            }
            catch {/*ignore*/}

            if (assemblyNameToReturn == null)
            {
                if (throwException)
                {
                    throw new ArgumentException($"{nameof(assemblyQualifiedName)} must contain the assembly name.");
                }
                parsedAssemblyName = null;
                return false;
            }
            parsedAssemblyName = assemblyNameToReturn;
            return true;
        }

        private static bool Parse(string assemblyQualifiedName, bool throwException, bool getTypeName, out AssemblyName assemblyName, out string parsedTypeName)
        {
            bool assemblyNameWasExtracted = GetAssemblyName(assemblyQualifiedName, throwException, out AssemblyName extractedAssemblyName);
            if (!assemblyNameWasExtracted)
            {
                if (throwException)
                {
                    throw new ArgumentException($"{nameof(assemblyQualifiedName)} must contain the assembly name.");
                }
                assemblyName = null;
                parsedTypeName = null;
                return false;
            }
            string assemblyNameToReturn = extractedAssemblyName.FullName;
            if (getTypeName)
            {
                
                int assemblyNameIndex = assemblyQualifiedName.LastIndexOf(assemblyNameToReturn, StringComparison.Ordinal);
                if (assemblyNameIndex == -1)
                {
                    if (throwException)
                    {
                        throw new ArgumentException($"{nameof(assemblyQualifiedName)} must contain the assembly name.");
                    }

                    parsedTypeName = null;
                    assemblyName = null;
                    return false;
                }
                else
                {
                    //typename represents the entire content till assembly name
                    for (int i = assemblyNameIndex; i > 0; i--)
                    {
                        if (assemblyQualifiedName[i] == ',')
                        {
                            string tempTypeName = assemblyQualifiedName.Substring(0, i);
                            if (string.IsNullOrWhiteSpace(tempTypeName))
                            {
                                if (throwException)
                                {
                                    throw new ArgumentException($"{nameof(assemblyQualifiedName)} must contain a valid type name.");
                                }

                                assemblyName = null;
                                parsedTypeName = null;
                                return false;
                            }

                            parsedTypeName = tempTypeName;
                            assemblyName = extractedAssemblyName;
                            return true;
                        }
                    }

                    if (throwException)
                    {
                        throw new ArgumentException($"{nameof(assemblyQualifiedName)} must have a valid format.");
                    }
                    assemblyName = null;
                    parsedTypeName = null;
                    return false;
                }
            }
            else
            {
                assemblyName = extractedAssemblyName;
                parsedTypeName = null;
                return true;
            }
        }
    }
}