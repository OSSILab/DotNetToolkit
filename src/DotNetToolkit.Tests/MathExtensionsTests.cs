using System.Collections.Generic;
using System.Linq;
using System.Mathematics;
using NUnit.Framework;

namespace DotNetToolkit.Tests
{
    [TestFixture]
    [Category("UnitTests")]
    class MathExtensionsTests
    {
        class PermutationData<T>:IPermutationData
        {
            public T[] Objects { get; set; }
            public T[][] ExpectedValues { get; set; }

            public void Test()
            {
                var permutations = Objects.Permutations().ToArray();
                uint objectsCount = (uint)Objects.Length;
                uint objectsFactorial = objectsCount.Factorial();
                Assert.AreEqual(permutations.Length, objectsFactorial);
                if (ExpectedValues != null)
                {
                    foreach (T[] expectedValue in ExpectedValues)
                    {
                        Assert.Contains(expectedValue, permutations);
                    }
                }
            }
        }

        public interface IPermutationData
        {
            void Test();
        }

        static IEnumerable<IPermutationData> GetDataSourceForPermutations()
        {
            yield return new PermutationData<int>()
            {
                Objects = new[] {1, 2, 3}, 
                ExpectedValues = new[]
                {
                    new[] {1, 2, 3},
                    new[] {1, 3, 2},
                    new[] {2, 3, 1},
                    new[] {2, 1, 3},
                    new[] {3, 2, 1},
                    new[] {3, 1, 2}
                }
            };
            yield return new PermutationData<string>() {Objects = new[] {"a", "f", "g"}};
            yield return new PermutationData<int>() {Objects = new[] {7, 0, 6, 8, 1}};
        }


        [TestCaseSource(nameof(GetDataSourceForPermutations))]
        public void Permutations_MultipleInputs_ShouldReturnExpectedResult(IPermutationData permutationData)
        {
            permutationData.Test();
        }
    }
}