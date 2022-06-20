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

namespace System
{
	/// <summary>
    /// Provides a set of static (Shared in Visual Basic) methods for working with bit flags operations.
    /// </summary>
	public static class BitFlagsExtensions
	{
        /// <summary>
        /// Determines whether one or more bit fields are set to a specific value.
        /// If <paramref name="value"/> contains any of bit fields that are set in
        /// <paramref name="flagsToCheck"/> the function will return <c>True</c>.
        /// If <paramref name="value"/> is 0 it will return <c>True</c>.
        /// Otherwise <c>False</c>.
        /// </summary>
        /// <param name="value">The value to be checked against <paramref name="flagsToCheck"/> flags.</param>
        /// <param name="flagsToCheck"></param>
        /// <returns><c>True</c> if <paramref name="value"/> contains any of bit fields that are set in
        /// <paramref name="flagsToCheck"/>, <c>False</c> otherwise.</returns>
        public static bool HasAnyBit(this int value, int flagsToCheck)
        {
            if (value == 0)
            {
                return true;
            }
            return (value & flagsToCheck) != 0;
        }

        /// <summary>
        /// Determines whether all bit fields are set to a specific value.
        /// If <paramref name="value"/> contains all bit fields that are set in
        /// <paramref name="flagsToCheck"/> the function will return <c>True</c>.
        /// If <paramref name="value"/> is 0 it will return <c>True</c>.
        /// Otherwise <c>False</c>.
        /// </summary>
        /// <param name="value">The value to be checked against <paramref name="flagsToCheck"/> flags.</param>
        /// <param name="flagsToCheck"></param>
        /// <returns><c>True</c> if <paramref name="value"/> contains all bit fields that are set in
        /// <paramref name="flagsToCheck"/>, <c>False</c> otherwise.</returns>
        public static bool HasAllBits(this int value, int flagsToCheck)
        {
            if (flagsToCheck == 0)
            {
                return true;
            }
            return (value & flagsToCheck) == flagsToCheck;
        }
        /// <summary>
        /// Determines whether one or more bit fields are set to a specific value.
        /// If <paramref name="value"/> contains any of bit fields that are set in
        /// <paramref name="flagsToCheck"/> the function will return <c>True</c>.
        /// If <paramref name="value"/> is 0 it will return <c>True</c>.
        /// Otherwise <c>False</c>.
        /// </summary>
        /// <param name="value">The value to be checked against <paramref name="flagsToCheck"/> flags.</param>
        /// <param name="flagsToCheck"></param>
        /// <returns><c>True</c> if <paramref name="value"/> contains any of bit fields that are set in
        /// <paramref name="flagsToCheck"/>, <c>False</c> otherwise.</returns>
        public static bool HasAnyBit(this uint value, uint flagsToCheck)
        {
            if (value == 0)
            {
                return true;
            }
            return (value & flagsToCheck) != 0;
        }

        /// <summary>
        /// Determines whether all bit fields are set to a specific value.
        /// If <paramref name="value"/> contains all bit fields that are set in
        /// <paramref name="flagsToCheck"/> the function will return <c>True</c>.
        /// If <paramref name="value"/> is 0 it will return <c>True</c>.
        /// Otherwise <c>False</c>.
        /// </summary>
        /// <param name="value">The value to be checked against <paramref name="flagsToCheck"/> flags.</param>
        /// <param name="flagsToCheck"></param>
        /// <returns><c>True</c> if <paramref name="value"/> contains all bit fields that are set in
        /// <paramref name="flagsToCheck"/>, <c>False</c> otherwise.</returns>
        public static bool HasAllBits(this uint value, uint flagsToCheck)
        {
            if (flagsToCheck == 0)
            {
                return true;
            }
            return (value & flagsToCheck) == flagsToCheck;
        }
        /// <summary>
        /// Determines whether one or more bit fields are set to a specific value.
        /// If <paramref name="value"/> contains any of bit fields that are set in
        /// <paramref name="flagsToCheck"/> the function will return <c>True</c>.
        /// If <paramref name="value"/> is 0 it will return <c>True</c>.
        /// Otherwise <c>False</c>.
        /// </summary>
        /// <param name="value">The value to be checked against <paramref name="flagsToCheck"/> flags.</param>
        /// <param name="flagsToCheck"></param>
        /// <returns><c>True</c> if <paramref name="value"/> contains any of bit fields that are set in
        /// <paramref name="flagsToCheck"/>, <c>False</c> otherwise.</returns>
        public static bool HasAnyBit(this ulong value, ulong flagsToCheck)
        {
            if (value == 0)
            {
                return true;
            }
            return (value & flagsToCheck) != 0;
        }

        /// <summary>
        /// Determines whether all bit fields are set to a specific value.
        /// If <paramref name="value"/> contains all bit fields that are set in
        /// <paramref name="flagsToCheck"/> the function will return <c>True</c>.
        /// If <paramref name="value"/> is 0 it will return <c>True</c>.
        /// Otherwise <c>False</c>.
        /// </summary>
        /// <param name="value">The value to be checked against <paramref name="flagsToCheck"/> flags.</param>
        /// <param name="flagsToCheck"></param>
        /// <returns><c>True</c> if <paramref name="value"/> contains all bit fields that are set in
        /// <paramref name="flagsToCheck"/>, <c>False</c> otherwise.</returns>
        public static bool HasAllBits(this ulong value, ulong flagsToCheck)
        {
            if (flagsToCheck == 0)
            {
                return true;
            }
            return (value & flagsToCheck) == flagsToCheck;
        }
        /// <summary>
        /// Determines whether one or more bit fields are set to a specific value.
        /// If <paramref name="value"/> contains any of bit fields that are set in
        /// <paramref name="flagsToCheck"/> the function will return <c>True</c>.
        /// If <paramref name="value"/> is 0 it will return <c>True</c>.
        /// Otherwise <c>False</c>.
        /// </summary>
        /// <param name="value">The value to be checked against <paramref name="flagsToCheck"/> flags.</param>
        /// <param name="flagsToCheck"></param>
        /// <returns><c>True</c> if <paramref name="value"/> contains any of bit fields that are set in
        /// <paramref name="flagsToCheck"/>, <c>False</c> otherwise.</returns>
        public static bool HasAnyBit(this long value, long flagsToCheck)
        {
            if (value == 0)
            {
                return true;
            }
            return (value & flagsToCheck) != 0;
        }

        /// <summary>
        /// Determines whether all bit fields are set to a specific value.
        /// If <paramref name="value"/> contains all bit fields that are set in
        /// <paramref name="flagsToCheck"/> the function will return <c>True</c>.
        /// If <paramref name="value"/> is 0 it will return <c>True</c>.
        /// Otherwise <c>False</c>.
        /// </summary>
        /// <param name="value">The value to be checked against <paramref name="flagsToCheck"/> flags.</param>
        /// <param name="flagsToCheck"></param>
        /// <returns><c>True</c> if <paramref name="value"/> contains all bit fields that are set in
        /// <paramref name="flagsToCheck"/>, <c>False</c> otherwise.</returns>
        public static bool HasAllBits(this long value, long flagsToCheck)
        {
            if (flagsToCheck == 0)
            {
                return true;
            }
            return (value & flagsToCheck) == flagsToCheck;
        }
        /// <summary>
        /// Determines whether one or more bit fields are set to a specific value.
        /// If <paramref name="value"/> contains any of bit fields that are set in
        /// <paramref name="flagsToCheck"/> the function will return <c>True</c>.
        /// If <paramref name="value"/> is 0 it will return <c>True</c>.
        /// Otherwise <c>False</c>.
        /// </summary>
        /// <param name="value">The value to be checked against <paramref name="flagsToCheck"/> flags.</param>
        /// <param name="flagsToCheck"></param>
        /// <returns><c>True</c> if <paramref name="value"/> contains any of bit fields that are set in
        /// <paramref name="flagsToCheck"/>, <c>False</c> otherwise.</returns>
        public static bool HasAnyBit(this sbyte value, sbyte flagsToCheck)
        {
            if (value == 0)
            {
                return true;
            }
            return (value & flagsToCheck) != 0;
        }

        /// <summary>
        /// Determines whether all bit fields are set to a specific value.
        /// If <paramref name="value"/> contains all bit fields that are set in
        /// <paramref name="flagsToCheck"/> the function will return <c>True</c>.
        /// If <paramref name="value"/> is 0 it will return <c>True</c>.
        /// Otherwise <c>False</c>.
        /// </summary>
        /// <param name="value">The value to be checked against <paramref name="flagsToCheck"/> flags.</param>
        /// <param name="flagsToCheck"></param>
        /// <returns><c>True</c> if <paramref name="value"/> contains all bit fields that are set in
        /// <paramref name="flagsToCheck"/>, <c>False</c> otherwise.</returns>
        public static bool HasAllBits(this sbyte value, sbyte flagsToCheck)
        {
            if (flagsToCheck == 0)
            {
                return true;
            }
            return (value & flagsToCheck) == flagsToCheck;
        }
        /// <summary>
        /// Determines whether one or more bit fields are set to a specific value.
        /// If <paramref name="value"/> contains any of bit fields that are set in
        /// <paramref name="flagsToCheck"/> the function will return <c>True</c>.
        /// If <paramref name="value"/> is 0 it will return <c>True</c>.
        /// Otherwise <c>False</c>.
        /// </summary>
        /// <param name="value">The value to be checked against <paramref name="flagsToCheck"/> flags.</param>
        /// <param name="flagsToCheck"></param>
        /// <returns><c>True</c> if <paramref name="value"/> contains any of bit fields that are set in
        /// <paramref name="flagsToCheck"/>, <c>False</c> otherwise.</returns>
        public static bool HasAnyBit(this byte value, byte flagsToCheck)
        {
            if (value == 0)
            {
                return true;
            }
            return (value & flagsToCheck) != 0;
        }

        /// <summary>
        /// Determines whether all bit fields are set to a specific value.
        /// If <paramref name="value"/> contains all bit fields that are set in
        /// <paramref name="flagsToCheck"/> the function will return <c>True</c>.
        /// If <paramref name="value"/> is 0 it will return <c>True</c>.
        /// Otherwise <c>False</c>.
        /// </summary>
        /// <param name="value">The value to be checked against <paramref name="flagsToCheck"/> flags.</param>
        /// <param name="flagsToCheck"></param>
        /// <returns><c>True</c> if <paramref name="value"/> contains all bit fields that are set in
        /// <paramref name="flagsToCheck"/>, <c>False</c> otherwise.</returns>
        public static bool HasAllBits(this byte value, byte flagsToCheck)
        {
            if (flagsToCheck == 0)
            {
                return true;
            }
            return (value & flagsToCheck) == flagsToCheck;
        }
        /// <summary>
        /// Determines whether one or more bit fields are set to a specific value.
        /// If <paramref name="value"/> contains any of bit fields that are set in
        /// <paramref name="flagsToCheck"/> the function will return <c>True</c>.
        /// If <paramref name="value"/> is 0 it will return <c>True</c>.
        /// Otherwise <c>False</c>.
        /// </summary>
        /// <param name="value">The value to be checked against <paramref name="flagsToCheck"/> flags.</param>
        /// <param name="flagsToCheck"></param>
        /// <returns><c>True</c> if <paramref name="value"/> contains any of bit fields that are set in
        /// <paramref name="flagsToCheck"/>, <c>False</c> otherwise.</returns>
        public static bool HasAnyBit(this short value, short flagsToCheck)
        {
            if (value == 0)
            {
                return true;
            }
            return (value & flagsToCheck) != 0;
        }

        /// <summary>
        /// Determines whether all bit fields are set to a specific value.
        /// If <paramref name="value"/> contains all bit fields that are set in
        /// <paramref name="flagsToCheck"/> the function will return <c>True</c>.
        /// If <paramref name="value"/> is 0 it will return <c>True</c>.
        /// Otherwise <c>False</c>.
        /// </summary>
        /// <param name="value">The value to be checked against <paramref name="flagsToCheck"/> flags.</param>
        /// <param name="flagsToCheck"></param>
        /// <returns><c>True</c> if <paramref name="value"/> contains all bit fields that are set in
        /// <paramref name="flagsToCheck"/>, <c>False</c> otherwise.</returns>
        public static bool HasAllBits(this short value, short flagsToCheck)
        {
            if (flagsToCheck == 0)
            {
                return true;
            }
            return (value & flagsToCheck) == flagsToCheck;
        }
        /// <summary>
        /// Determines whether one or more bit fields are set to a specific value.
        /// If <paramref name="value"/> contains any of bit fields that are set in
        /// <paramref name="flagsToCheck"/> the function will return <c>True</c>.
        /// If <paramref name="value"/> is 0 it will return <c>True</c>.
        /// Otherwise <c>False</c>.
        /// </summary>
        /// <param name="value">The value to be checked against <paramref name="flagsToCheck"/> flags.</param>
        /// <param name="flagsToCheck"></param>
        /// <returns><c>True</c> if <paramref name="value"/> contains any of bit fields that are set in
        /// <paramref name="flagsToCheck"/>, <c>False</c> otherwise.</returns>
        public static bool HasAnyBit(this ushort value, ushort flagsToCheck)
        {
            if (value == 0)
            {
                return true;
            }
            return (value & flagsToCheck) != 0;
        }

        /// <summary>
        /// Determines whether all bit fields are set to a specific value.
        /// If <paramref name="value"/> contains all bit fields that are set in
        /// <paramref name="flagsToCheck"/> the function will return <c>True</c>.
        /// If <paramref name="value"/> is 0 it will return <c>True</c>.
        /// Otherwise <c>False</c>.
        /// </summary>
        /// <param name="value">The value to be checked against <paramref name="flagsToCheck"/> flags.</param>
        /// <param name="flagsToCheck"></param>
        /// <returns><c>True</c> if <paramref name="value"/> contains all bit fields that are set in
        /// <paramref name="flagsToCheck"/>, <c>False</c> otherwise.</returns>
        public static bool HasAllBits(this ushort value, ushort flagsToCheck)
        {
            if (flagsToCheck == 0)
            {
                return true;
            }
            return (value & flagsToCheck) == flagsToCheck;
        }
        /// <summary>
        /// Determines whether one or more bit fields are set to a specific value.
        /// If <paramref name="value"/> contains any of bit fields that are set in
        /// <paramref name="flagsToCheck"/> the function will return <c>True</c>.
        /// If <paramref name="value"/> is 0 it will return <c>True</c>.
        /// Otherwise <c>False</c>.
        /// </summary>
        /// <param name="value">The value to be checked against <paramref name="flagsToCheck"/> flags.</param>
        /// <param name="flagsToCheck"></param>
        /// <returns><c>True</c> if <paramref name="value"/> contains any of bit fields that are set in
        /// <paramref name="flagsToCheck"/>, <c>False</c> otherwise.</returns>
        public static bool HasAnyBit(this char value, char flagsToCheck)
        {
            if (value == 0)
            {
                return true;
            }
            return (value & flagsToCheck) != 0;
        }

        /// <summary>
        /// Determines whether all bit fields are set to a specific value.
        /// If <paramref name="value"/> contains all bit fields that are set in
        /// <paramref name="flagsToCheck"/> the function will return <c>True</c>.
        /// If <paramref name="value"/> is 0 it will return <c>True</c>.
        /// Otherwise <c>False</c>.
        /// </summary>
        /// <param name="value">The value to be checked against <paramref name="flagsToCheck"/> flags.</param>
        /// <param name="flagsToCheck"></param>
        /// <returns><c>True</c> if <paramref name="value"/> contains all bit fields that are set in
        /// <paramref name="flagsToCheck"/>, <c>False</c> otherwise.</returns>
        public static bool HasAllBits(this char value, char flagsToCheck)
        {
            if (flagsToCheck == 0)
            {
                return true;
            }
            return (value & flagsToCheck) == flagsToCheck;
        }
    }


    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) methods for working with enumeration flags operations.
    /// </summary>
    public static class BitFlagsEnumExtensions
    {
        /// <summary>
        /// Determines whether one or more flags(bit fields) are set to a specific enum value.
        /// If <paramref name="value"/> contains any flags(bit fields) that are set in
        /// <paramref name="flagsToCheck"/> the function will return <c>True</c>.
        /// If <paramref name="value"/> is 0 it will return <c>True</c>.
        /// Otherwise <c>False</c>.
        /// </summary>
        /// <param name="value">The value to be checked against <paramref name="flagsToCheck"/> flags.</param>
        /// <param name="flagsToCheck"></param>
        /// <returns><c>True</c> if <paramref name="value"/> contains any flags(bit fields) that are set in
        /// <paramref name="flagsToCheck"/>, <c>False</c> otherwise.</returns>
        public static bool HasAnyFlag<TValue, TFlagsToCheck>(this TValue value, TFlagsToCheck flagsToCheck) where TValue : Enum where TFlagsToCheck : Enum
        {
            // handle each underlying type
            Type valueUnderlyingType = Enum.GetUnderlyingType(typeof(TValue));
            Type flagsToCheckUnderlyingType = Enum.GetUnderlyingType(typeof(TFlagsToCheck));
            if(valueUnderlyingType != flagsToCheckUnderlyingType)
            {
                throw new ArgumentException($"The underlying type of {nameof(value)}({valueUnderlyingType.Name}) is not the same as underlaying type of {nameof(flagsToCheck)}({flagsToCheckUnderlyingType.Name}).");
            }
            object unboxedValue = value, unboxedFlagsToCheck = flagsToCheck;
            if(valueUnderlyingType == typeof(int))
                    return BitFlagsExtensions.HasAnyBit((int)unboxedValue, (int)unboxedFlagsToCheck);
            if(valueUnderlyingType == typeof(uint))
                    return BitFlagsExtensions.HasAnyBit((uint)unboxedValue, (uint)unboxedFlagsToCheck);
            if(valueUnderlyingType == typeof(ulong))
                    return BitFlagsExtensions.HasAnyBit((ulong)unboxedValue, (ulong)unboxedFlagsToCheck);
            if(valueUnderlyingType == typeof(long))
                    return BitFlagsExtensions.HasAnyBit((long)unboxedValue, (long)unboxedFlagsToCheck);
            if(valueUnderlyingType == typeof(sbyte))
                    return BitFlagsExtensions.HasAnyBit((sbyte)unboxedValue, (sbyte)unboxedFlagsToCheck);
            if(valueUnderlyingType == typeof(byte))
                    return BitFlagsExtensions.HasAnyBit((byte)unboxedValue, (byte)unboxedFlagsToCheck);
            if(valueUnderlyingType == typeof(short))
                    return BitFlagsExtensions.HasAnyBit((short)unboxedValue, (short)unboxedFlagsToCheck);
            if(valueUnderlyingType == typeof(ushort))
                    return BitFlagsExtensions.HasAnyBit((ushort)unboxedValue, (ushort)unboxedFlagsToCheck);
            if(valueUnderlyingType == typeof(char))
                    return BitFlagsExtensions.HasAnyBit((char)unboxedValue, (char)unboxedFlagsToCheck);
            throw new ArgumentException($"Unknown enum underlying type {valueUnderlyingType.Name}.");
        }


        /// <summary>
        /// Determines whether all flags(bit fields) are set to a specific enum value.
        /// If <paramref name="value"/> contains all flags(bit fields) that are set in
        /// <paramref name="flagsToCheck"/> the function will return <c>True</c>.
        /// If <paramref name="value"/> is 0 it will return <c>True</c>.
        /// Otherwise <c>False</c>.
        /// </summary>
        /// <param name="value">The value to be checked against <paramref name="flagsToCheck"/> flags.</param>
        /// <param name="flagsToCheck"></param>
        /// <returns><c>True</c> if <paramref name="value"/> contains all flags(bit fields) that are set in
        /// <paramref name="flagsToCheck"/>, <c>False</c> otherwise.</returns>
        public static bool HasAllFlags<TValue, TFlagsToCheck>(this TValue value, TFlagsToCheck flagsToCheck) where TValue : Enum where TFlagsToCheck : Enum
        {
            // handle each underlying type
            Type valueUnderlyingType = Enum.GetUnderlyingType(typeof(TValue));
            Type flagsToCheckUnderlyingType = Enum.GetUnderlyingType(typeof(TFlagsToCheck));
            if(valueUnderlyingType != flagsToCheckUnderlyingType)
            {
                throw new ArgumentException($"The underlying type of {nameof(value)}({valueUnderlyingType.Name}) is not the same as underlaying type of {nameof(flagsToCheck)}({flagsToCheckUnderlyingType.Name}).");
            }
            object unboxedValue = value, unboxedFlagsToCheck = flagsToCheck;
            if(valueUnderlyingType == typeof(int))
                    return BitFlagsExtensions.HasAllBits((int)unboxedValue, (int)unboxedFlagsToCheck);
            if(valueUnderlyingType == typeof(uint))
                    return BitFlagsExtensions.HasAllBits((uint)unboxedValue, (uint)unboxedFlagsToCheck);
            if(valueUnderlyingType == typeof(ulong))
                    return BitFlagsExtensions.HasAllBits((ulong)unboxedValue, (ulong)unboxedFlagsToCheck);
            if(valueUnderlyingType == typeof(long))
                    return BitFlagsExtensions.HasAllBits((long)unboxedValue, (long)unboxedFlagsToCheck);
            if(valueUnderlyingType == typeof(sbyte))
                    return BitFlagsExtensions.HasAllBits((sbyte)unboxedValue, (sbyte)unboxedFlagsToCheck);
            if(valueUnderlyingType == typeof(byte))
                    return BitFlagsExtensions.HasAllBits((byte)unboxedValue, (byte)unboxedFlagsToCheck);
            if(valueUnderlyingType == typeof(short))
                    return BitFlagsExtensions.HasAllBits((short)unboxedValue, (short)unboxedFlagsToCheck);
            if(valueUnderlyingType == typeof(ushort))
                    return BitFlagsExtensions.HasAllBits((ushort)unboxedValue, (ushort)unboxedFlagsToCheck);
            if(valueUnderlyingType == typeof(char))
                    return BitFlagsExtensions.HasAllBits((char)unboxedValue, (char)unboxedFlagsToCheck);
            throw new ArgumentException($"Unknown enum underlying type {valueUnderlyingType.Name}.");
        }
    }
}

