using System;
using NUnit.Framework;

namespace DotNetToolkit.Tests
{
    [TestFixture]
    [Category("UnitTests")]
    public class BitFlagsTests
    {
        enum SignedIntegerValue : int
        {
            Value0 = 0b_00000000_00000000_00000000_00000000, Value1 = 0b_00000000_00000000_00000000_00000000,
            Value2 = 0b_01111111_11111111_11111111_11111111, Value3 = 0b_01111111_11111111_11111111_11111111,
            Value4 = 0b_00000001_00000000_00000000_00000000, Value5 = 0b_00000001_00000000_00000000_00000000,
            Value6 = 0b_00000000_00000000_00000000_00000001, Value7 = 0b_00000000_00000000_00000000_00000001,
            Value8 = 0b_01000000_00010000_00000000_00000000, Value9 = 0b_01000000_00011000_00000000_00000000,
            Value10 = 0b_00000000_00000000_00000000_00000000, Value11 = 0b_01111111_11111111_11111111_11111111,
            Value12 = 0b_01000000_00000000_00000000_00000000, Value13 = 0b_00000000_00000000_00000000_00000000,
            Value14 = 0b_01000000_00000000_00000000_00000000, Value15 = 0b_00000000_00000000_00000000_00000001,
            Value16 = 0b_01111111_11111111_11111111_11111111, Value17 = 0b_00000000_00000000_00000000_00000000,
            Value18 = 0b_00111111_01011111_01111111_01111111, Value19 = 0b_01000000_10100000_10000000_10000000
        }

        enum UnsignedIntegerValue : uint
        {
            Value0 = 0b_00000000_00000000_00000000_00000000U, Value1 = 0b_00000000_00000000_00000000_00000000U,
            Value2 = 0b_11111111_11111111_11111111_11111111U, Value3 = 0b_11111111_11111111_11111111_11111111U,
            Value4 = 0b_00000001_00000000_00000000_00000000U, Value5 = 0b_00000001_00000000_00000000_00000000U,
            Value6 = 0b_00000000_00000000_00000000_00000001U, Value7 = 0b_00000000_00000000_00000000_00000001U,
            Value8 = 0b_00000000_00010000_00000000_00000000U, Value9 = 0b_10000000_00011000_00000000_00000000U,
            Value10 = 0b_00000000_00000000_00000000_00000000U, Value11 = 0b_11111111_11111111_11111111_11111111U,
            Value12 = 0b_10000000_00000000_00000000_00000000U, Value13 = 0b_00000000_00000000_00000000_00000000U,
            Value14 = 0b_10000000_00000000_00000000_00000000U, Value15 = 0b_00000000_00000000_00000000_00000001U,
            Value16 = 0b_11111111_11111111_11111111_11111111U, Value17 = 0b_00000000_00000000_00000000_00000000U,
            Value18 = 0b_10111111_01011111_01111111_01111111U, Value19 = 0b_01000000_10100000_10000000_10000000U
        }

        enum SignedIntegerValue2 : int
        {
            Value0 = 0b_00000000_00000000_00000000_00000000, Value1 = 0b_00000000_00000000_00000000_00000000,
            Value2 = 0b_01111111_11111111_11111111_11111111, Value3 = 0b_01111111_11111111_11111111_11111111,
            Value4 = 0b_00000001_00000000_00000000_00000000, Value5 = 0b_00000001_00000000_00000000_00000000,
            Value6 = 0b_00000000_00000000_00000000_00000001, Value7 = 0b_00000000_00000000_00000000_00000001,
            Value8 = 0b_01000000_00010000_00000000_00000000, Value9 = 0b_01000000_00011000_00000000_00000000,
            Value10 = 0b_00000000_00000000_00000000_00000000, Value11 = 0b_01111111_11111111_11111111_11111111,
            Value12 = 0b_01000000_00000000_00000000_00000000, Value13 = 0b_00000000_00000000_00000000_00000000,
            Value14 = 0b_01000000_00000000_00000000_00000000, Value15 = 0b_00000000_00000000_00000000_00000001,
            Value16 = 0b_01111111_11111111_11111111_11111111, Value17 = 0b_00000000_00000000_00000000_00000000,
            Value18 = 0b_00111111_01011111_01111111_01111111, Value19 = 0b_01000000_10100000_10000000_10000000
        }

        enum UnsignedIntegerValue2 : uint
        {
            Value0 = 0b_00000000_00000000_00000000_00000000U, Value1 = 0b_00000000_00000000_00000000_00000000U,
            Value2 = 0b_11111111_11111111_11111111_11111111U, Value3 = 0b_11111111_11111111_11111111_11111111U,
            Value4 = 0b_00000001_00000000_00000000_00000000U, Value5 = 0b_00000001_00000000_00000000_00000000U,
            Value6 = 0b_00000000_00000000_00000000_00000001U, Value7 = 0b_00000000_00000000_00000000_00000001U,
            Value8 = 0b_00000000_00010000_00000000_00000000U, Value9 = 0b_10000000_00011000_00000000_00000000U,
            Value10 = 0b_00000000_00000000_00000000_00000000U, Value11 = 0b_11111111_11111111_11111111_11111111U,
            Value12 = 0b_10000000_00000000_00000000_00000000U, Value13 = 0b_00000000_00000000_00000000_00000000U,
            Value14 = 0b_10000000_00000000_00000000_00000000U, Value15 = 0b_00000000_00000000_00000000_00000001U,
            Value16 = 0b_11111111_11111111_11111111_11111111U, Value17 = 0b_00000000_00000000_00000000_00000000U,
            Value18 = 0b_10111111_01011111_01111111_01111111U, Value19 = 0b_01000000_10100000_10000000_10000000U
        }

        [TestCase(0b_00000000_00000000_00000000_00000000U, 0b_00000000_00000000_00000000_00000000U, true)]
        [TestCase(0b_11111111_11111111_11111111_11111111U, 0b_11111111_11111111_11111111_11111111U, true)]
        [TestCase(0b_00000001_00000000_00000000_00000000U, 0b_00000001_00000000_00000000_00000000U, true)]
        [TestCase(0b_00000000_00000000_00000000_00000001U, 0b_00000000_00000000_00000000_00000001U, true)]
        [TestCase(0b_00000000_00010000_00000000_00000000U, 0b_10000000_00011000_00000000_00000000U, true)]
        [TestCase(0b_00000000_00000000_00000000_00000000U, 0b_11111111_11111111_11111111_11111111U, true)]
        [TestCase(0b_10000000_00000000_00000000_00000000U, 0b_00000000_00000000_00000000_00000000U, false)]
        [TestCase(0b_10000000_00000000_00000000_00000000U, 0b_00000000_00000000_00000000_00000001U, false)]
        [TestCase(0b_11111111_11111111_11111111_11111111U, 0b_00000000_00000000_00000000_00000000U, false)]
        [TestCase(0b_10111111_01011111_01111111_01111111U, 0b_01000000_10100000_10000000_10000000U, false)]
        public void HasAnyBit_CheckForMultipleUIntValues_ShouldReturnExpectedResult(uint value, uint flags, bool hasAnyFlag)
        {
            bool hasAnyFlagResult = value.HasAnyBit(flags);
            Assert.AreEqual(hasAnyFlag, hasAnyFlagResult);
        }


        [TestCase(0b_00000000_00000000_00000000_00000000, 0b_00000000_00000000_00000000_00000000, true)]
        [TestCase(0b_01111111_11111111_11111111_11111111, 0b_01111111_11111111_11111111_11111111, true)]
        [TestCase(0b_00000001_00000000_00000000_00000000, 0b_00000001_00000000_00000000_00000000, true)]
        [TestCase(0b_00000000_00000000_00000000_00000001, 0b_00000000_00000000_00000000_00000001, true)]
        [TestCase(0b_01000000_00010000_00000000_00000000, 0b_01000000_00011000_00000000_00000000, true)]
        [TestCase(0b_00000000_00000000_00000000_00000000, 0b_01111111_11111111_11111111_11111111, true)]
        [TestCase(0b_01000000_00000000_00000000_00000000, 0b_00000000_00000000_00000000_00000000, false)]
        [TestCase(0b_01000000_00000000_00000000_00000000, 0b_00000000_00000000_00000000_00000001, false)]
        [TestCase(0b_01111111_11111111_11111111_11111111, 0b_00000000_00000000_00000000_00000000, false)]
        [TestCase(0b_00111111_01011111_01111111_01111111, 0b_01000000_10100000_10000000_10000000, false)]
        public void HasAnyBit_CheckForMultipleIntValues_ShouldReturnExpectedResult(int value, int flags, bool hasAnyFlag)
        {
            bool hasAnyFlagResult = value.HasAnyBit(flags);
            Assert.AreEqual(hasAnyFlag, hasAnyFlagResult);
        }

        [TestCase(0b_00000000_00000000_00000000_00000000U, 0b_00000000_00000000_00000000_00000000U, true)]
        [TestCase(0b_11111111_11111111_11111111_11111111U, 0b_11111111_11111111_11111111_11111111U, true)]
        [TestCase(0b_00000001_00000000_00000000_00000000U, 0b_00000001_00000000_00000000_00000000U, true)]
        [TestCase(0b_00000000_00000000_00000000_00000001U, 0b_00000000_00000000_00000000_00000001U, true)]
        [TestCase(0b_00000000_00010000_00000000_00000000U, 0b_10000000_00011000_00000000_00000000U, false)]
        [TestCase(0b_00000000_00000000_00000000_00000000U, 0b_11111111_11111111_11111111_11111111U, false)]
        [TestCase(0b_10000000_00000000_00000000_00000000U, 0b_00000000_00000000_00000000_00000000U, true)]
        [TestCase(0b_10000000_00000000_00000000_00000000U, 0b_00000000_00000000_00000000_00000001U, false)]
        [TestCase(0b_11111111_11111111_11111111_11111111U, 0b_00000000_00000000_00000000_00000000U, true)]
        [TestCase(0b_10111111_01011111_01111111_01111111U, 0b_01000000_10100000_10000000_10000000U, false)]
        public void HasAllBits_CheckForMultipleUIntValues_ShouldReturnExpectedResult(uint value, uint flags, bool hasAnyFlag)
        {
            bool hasAnyFlagResult = value.HasAllBits(flags);
            Assert.AreEqual(hasAnyFlag, hasAnyFlagResult);
        }


        [TestCase(0b_00000000_00000000_00000000_00000000, 0b_00000000_00000000_00000000_00000000, true)]
        [TestCase(0b_01111111_11111111_11111111_11111111, 0b_01111111_11111111_11111111_11111111, true)]
        [TestCase(0b_00000001_00000000_00000000_00000000, 0b_00000001_00000000_00000000_00000000, true)]
        [TestCase(0b_00000000_00000000_00000000_00000001, 0b_00000000_00000000_00000000_00000001, true)]
        [TestCase(0b_01000000_00010000_00000000_00000000, 0b_01000000_00011000_00000000_00000000, false)]
        [TestCase(0b_00000000_00000000_00000000_00000000, 0b_01111111_11111111_11111111_11111111, false)]
        [TestCase(0b_01000000_00000000_00000000_00000000, 0b_00000000_00000000_00000000_00000000, true)]
        [TestCase(0b_01000000_00000000_00000000_00000000, 0b_00000000_00000000_00000000_00000001, false)]
        [TestCase(0b_01111111_11111111_11111111_11111111, 0b_00000000_00000000_00000000_00000000, true)]
        [TestCase(0b_00111111_01011111_01111111_01111111, 0b_01000000_10100000_10000000_10000000, false)]
        public void HasAllBits_CheckForMultipleIntValues_ShouldReturnExpectedResult(int value, int flags, bool hasAnyFlag)
        {
            bool hasAnyFlagResult = value.HasAllBits(flags);
            Assert.AreEqual(hasAnyFlag, hasAnyFlagResult);
        }


        [TestCase(SignedIntegerValue.Value0, SignedIntegerValue.Value1, true)]
        [TestCase(SignedIntegerValue.Value2, SignedIntegerValue.Value3, true)]
        [TestCase(SignedIntegerValue.Value4, SignedIntegerValue.Value5, true)]
        [TestCase(SignedIntegerValue.Value6, SignedIntegerValue.Value7, true)]
        [TestCase(SignedIntegerValue.Value8, SignedIntegerValue.Value9, true)]
        [TestCase(SignedIntegerValue.Value10, SignedIntegerValue.Value11, true)]
        [TestCase(SignedIntegerValue.Value12, SignedIntegerValue.Value13, false)]
        [TestCase(SignedIntegerValue.Value14, SignedIntegerValue.Value15, false)]
        [TestCase(SignedIntegerValue.Value16, SignedIntegerValue.Value17, false)]
        [TestCase(SignedIntegerValue.Value18, SignedIntegerValue.Value19, false)]
        public void HasAnyFlag_CheckForMultipleUIntValues_ShouldReturnExpectedResult<TValue, TFlagsToCheck>(TValue value, TFlagsToCheck flags, bool hasAnyFlag) where TValue:Enum where TFlagsToCheck:Enum
        {
            bool hasAnyFlagResult = value.HasAnyFlag(flags);
            Assert.AreEqual(hasAnyFlag, hasAnyFlagResult);
        }

        [TestCase(UnsignedIntegerValue.Value0, UnsignedIntegerValue.Value1, true)]
        [TestCase(UnsignedIntegerValue.Value2, UnsignedIntegerValue.Value3, true)]
        [TestCase(UnsignedIntegerValue.Value4, UnsignedIntegerValue.Value5, true)]
        [TestCase(UnsignedIntegerValue.Value6, UnsignedIntegerValue.Value7, true)]
        [TestCase(UnsignedIntegerValue.Value8, UnsignedIntegerValue.Value9, true)]
        [TestCase(UnsignedIntegerValue.Value10, UnsignedIntegerValue.Value11, true)]
        [TestCase(UnsignedIntegerValue.Value12, UnsignedIntegerValue.Value13, false)]
        [TestCase(UnsignedIntegerValue.Value14, UnsignedIntegerValue.Value15, false)]
        [TestCase(UnsignedIntegerValue.Value16, UnsignedIntegerValue.Value17, false)]
        [TestCase(UnsignedIntegerValue.Value18, UnsignedIntegerValue.Value19, false)]
        public void HasAnyFlag_CheckForMultipleIntValues_ShouldReturnExpectedResult<TValue, TFlagsToCheck>(TValue value, TFlagsToCheck flags, bool hasAnyFlag) where TValue : Enum where TFlagsToCheck : Enum
        {
            bool hasAnyFlagResult = value.HasAnyFlag(flags);
            Assert.AreEqual(hasAnyFlag, hasAnyFlagResult);
        }

        [TestCase(UnsignedIntegerValue.Value0, UnsignedIntegerValue2.Value1, true)]
        [TestCase(UnsignedIntegerValue.Value2, UnsignedIntegerValue2.Value3, true)]
        [TestCase(UnsignedIntegerValue.Value4, UnsignedIntegerValue2.Value5, true)]
        [TestCase(UnsignedIntegerValue.Value6, UnsignedIntegerValue2.Value7, true)]
        [TestCase(UnsignedIntegerValue.Value8, UnsignedIntegerValue2.Value9, true)]
        [TestCase(UnsignedIntegerValue.Value10, UnsignedIntegerValue2.Value11, true)]
        [TestCase(UnsignedIntegerValue.Value12, UnsignedIntegerValue2.Value13, false)]
        [TestCase(UnsignedIntegerValue.Value14, UnsignedIntegerValue2.Value15, false)]
        [TestCase(UnsignedIntegerValue.Value16, UnsignedIntegerValue2.Value17, false)]
        [TestCase(UnsignedIntegerValue.Value18, UnsignedIntegerValue2.Value19, false)]
        public void HasAnyFlag_CheckForDifferentUnderlyingEnumValues_UnsignedAndUnsigned2_ShouldReturnExpectedResult<TValue, TFlagsToCheck>(TValue value, TFlagsToCheck flags, bool hasAnyFlag) where TValue : Enum where TFlagsToCheck : Enum
        {
            bool hasAnyFlagResult = value.HasAnyFlag(flags);
            Assert.AreEqual(hasAnyFlag, hasAnyFlagResult);
        }

        [TestCase(SignedIntegerValue.Value0, SignedIntegerValue2.Value1, true)]
        [TestCase(SignedIntegerValue.Value2, SignedIntegerValue2.Value3, true)]
        [TestCase(SignedIntegerValue.Value4, SignedIntegerValue2.Value5, true)]
        [TestCase(SignedIntegerValue.Value6, SignedIntegerValue2.Value7, true)]
        [TestCase(SignedIntegerValue.Value8, SignedIntegerValue2.Value9, true)]
        [TestCase(SignedIntegerValue.Value10, SignedIntegerValue2.Value11, true)]
        [TestCase(SignedIntegerValue.Value12, SignedIntegerValue2.Value13, false)]
        [TestCase(SignedIntegerValue.Value14, SignedIntegerValue2.Value15, false)]
        [TestCase(SignedIntegerValue.Value16, SignedIntegerValue2.Value17, false)]
        [TestCase(SignedIntegerValue.Value18, SignedIntegerValue2.Value19, false)]
        public void HasAnyFlag_CheckForDifferentUnderlyingEnumValues_SignedAndSigned2_ShouldReturnExpectedResult<TValue, TFlagsToCheck>(TValue value, TFlagsToCheck flags, bool hasAnyFlag) where TValue : Enum where TFlagsToCheck : Enum
        {
            bool hasAnyFlagResult = value.HasAnyFlag(flags);
            Assert.AreEqual(hasAnyFlag, hasAnyFlagResult);
        }


        [TestCase(SignedIntegerValue.Value0, SignedIntegerValue.Value1, true)]
        [TestCase(SignedIntegerValue.Value2, SignedIntegerValue.Value3, true)]
        [TestCase(SignedIntegerValue.Value4, SignedIntegerValue.Value5, true)]
        [TestCase(SignedIntegerValue.Value6, SignedIntegerValue.Value7, true)]
        [TestCase(SignedIntegerValue.Value8, SignedIntegerValue.Value9, false)]
        [TestCase(SignedIntegerValue.Value10, SignedIntegerValue.Value11, false)]
        [TestCase(SignedIntegerValue.Value12, SignedIntegerValue.Value13, true)]
        [TestCase(SignedIntegerValue.Value14, SignedIntegerValue.Value15, false)]
        [TestCase(SignedIntegerValue.Value16, SignedIntegerValue.Value17, true)]
        [TestCase(SignedIntegerValue.Value18, SignedIntegerValue.Value19, false)]
        public void HasAllFlags_CheckForMultipleUIntValues_ShouldReturnExpectedResult<TValue, TFlagsToCheck>(TValue value, TFlagsToCheck flags, bool hasAnyFlag) where TValue : Enum where TFlagsToCheck : Enum
        {
            bool hasAnyFlagResult = value.HasAllFlags(flags);
            Assert.AreEqual(hasAnyFlag, hasAnyFlagResult);
        }

        [TestCase(UnsignedIntegerValue.Value0, UnsignedIntegerValue.Value1, true)]
        [TestCase(UnsignedIntegerValue.Value2, UnsignedIntegerValue.Value3, true)]
        [TestCase(UnsignedIntegerValue.Value4, UnsignedIntegerValue.Value5, true)]
        [TestCase(UnsignedIntegerValue.Value6, UnsignedIntegerValue.Value7, true)]
        [TestCase(UnsignedIntegerValue.Value8, UnsignedIntegerValue.Value9, false)]
        [TestCase(UnsignedIntegerValue.Value10, UnsignedIntegerValue.Value11, false)]
        [TestCase(UnsignedIntegerValue.Value12, UnsignedIntegerValue.Value13, true)]
        [TestCase(UnsignedIntegerValue.Value14, UnsignedIntegerValue.Value15, false)]
        [TestCase(UnsignedIntegerValue.Value16, UnsignedIntegerValue.Value17, true)]
        [TestCase(UnsignedIntegerValue.Value18, UnsignedIntegerValue.Value19, false)]
        public void HasAllFlags_CheckForMultipleIntValues_ShouldReturnExpectedResult<TValue, TFlagsToCheck>(TValue value, TFlagsToCheck flags, bool hasAnyFlag) where TValue : Enum where TFlagsToCheck : Enum
        {
            bool hasAnyFlagResult = value.HasAllFlags(flags);
            Assert.AreEqual(hasAnyFlag, hasAnyFlagResult);
        }

        [TestCase(UnsignedIntegerValue.Value0, UnsignedIntegerValue2.Value1, true)]
        [TestCase(UnsignedIntegerValue.Value2, UnsignedIntegerValue2.Value3, true)]
        [TestCase(UnsignedIntegerValue.Value4, UnsignedIntegerValue2.Value5, true)]
        [TestCase(UnsignedIntegerValue.Value6, UnsignedIntegerValue2.Value7, true)]
        [TestCase(UnsignedIntegerValue.Value8, UnsignedIntegerValue2.Value9, false)]
        [TestCase(UnsignedIntegerValue.Value10, UnsignedIntegerValue2.Value11, false)]
        [TestCase(UnsignedIntegerValue.Value12, UnsignedIntegerValue2.Value13, true)]
        [TestCase(UnsignedIntegerValue.Value14, UnsignedIntegerValue2.Value15, false)]
        [TestCase(UnsignedIntegerValue.Value16, UnsignedIntegerValue2.Value17, true)]
        [TestCase(UnsignedIntegerValue.Value18, UnsignedIntegerValue2.Value19, false)]
        public void HasAllFlags_CheckForDifferentUnderlyingEnumValues_UnsignedAndSigned_ShouldReturnExpectedResult<TValue, TFlagsToCheck>(TValue value, TFlagsToCheck flags, bool hasAnyFlag) where TValue : Enum where TFlagsToCheck : Enum
        {
            bool hasAnyFlagResult = value.HasAllFlags(flags);
            Assert.AreEqual(hasAnyFlag, hasAnyFlagResult);
        }

        [TestCase(SignedIntegerValue.Value0, SignedIntegerValue2.Value1, true)]
        [TestCase(SignedIntegerValue.Value2, SignedIntegerValue2.Value3, true)]
        [TestCase(SignedIntegerValue.Value4, SignedIntegerValue2.Value5, true)]
        [TestCase(SignedIntegerValue.Value6, SignedIntegerValue2.Value7, true)]
        [TestCase(SignedIntegerValue.Value8, SignedIntegerValue2.Value9, false)]
        [TestCase(SignedIntegerValue.Value10, SignedIntegerValue2.Value11, false)]
        [TestCase(SignedIntegerValue.Value12, SignedIntegerValue2.Value13, true)]
        [TestCase(SignedIntegerValue.Value14, SignedIntegerValue2.Value15, false)]
        [TestCase(SignedIntegerValue.Value16, SignedIntegerValue2.Value17, true)]
        [TestCase(SignedIntegerValue.Value18, SignedIntegerValue2.Value19, false)]
        public void HasAllFlags_CheckForDifferentUnderlyingEnumValues_SignedAndUnsigned_ShouldReturnExpectedResult<TValue, TFlagsToCheck>(TValue value, TFlagsToCheck flags, bool hasAnyFlag) where TValue : Enum where TFlagsToCheck : Enum
        {
            bool hasAnyFlagResult = value.HasAllFlags(flags);
            Assert.AreEqual(hasAnyFlag, hasAnyFlagResult);
        }

        [TestCase(SignedIntegerValue.Value0, UnsignedIntegerValue.Value0)]
        [TestCase(SignedIntegerValue.Value1, UnsignedIntegerValue.Value1)]
        [TestCase(SignedIntegerValue.Value2, UnsignedIntegerValue.Value2)]
        [TestCase(SignedIntegerValue.Value0, UnsignedIntegerValue.Value2)]
        [TestCase(SignedIntegerValue.Value2, UnsignedIntegerValue.Value0)]
        [TestCase(UnsignedIntegerValue.Value0, SignedIntegerValue.Value0)]
        [TestCase(UnsignedIntegerValue.Value1, SignedIntegerValue.Value1)]
        [TestCase(UnsignedIntegerValue.Value2, SignedIntegerValue.Value2)]
        [TestCase(UnsignedIntegerValue.Value0, SignedIntegerValue.Value2)]
        [TestCase(UnsignedIntegerValue.Value2, SignedIntegerValue.Value0)]
        public void HasFlags_DifferentUnderlyingTypesEnums_ShouldThrowExpectedException<TValue, TFlagsToCheck>(TValue value, TFlagsToCheck flags) where TValue : Enum where TFlagsToCheck : Enum
        {
            Assert.Throws<ArgumentException>(() => value.HasAnyFlag(flags));
            Assert.Throws<ArgumentException>(() => value.HasAllFlags(flags));
        }
    }
}