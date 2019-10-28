using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Open_Lab_04._06
{
    [TestFixture]
    public class Tests
    {

        private Numbers numbers;
        private bool shouldStop;

        private const int RandNumsMinSize = 2;
        private const int RandNumsMaxSize = 20;

        private const int RandSeed = 406406406;
        private const int RandTestCasesCount = 97;

        [OneTimeSetUp]
        public void Init()
        {
            numbers = new Numbers();
            shouldStop = false;
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome == ResultState.Failure ||
                TestContext.CurrentContext.Result.Outcome == ResultState.Error)
                shouldStop = true;
        }

        [TestCase(new []{ 1, 2, 3, 4, 5, 6, 7, 8 }, new []{ 2, 4, 6, 8 })]
        [TestCase(new []{ 43, 65, 23, 89, 53, 9, 6 }, new []{ 6 })]
        [TestCase(new []{ 718, 991, 449, 644, 380, 440 }, new []{ 718, 644, 380, 440 })]
        public void NoOddsTest(int[] nums, int[] expected) =>
            Assert.That(numbers.NoOdds(nums), Is.EqualTo(expected));

        [TestCaseSource(nameof(GetRandom))]
        public void NoOddsTestRandom(int[] nums, int[] expected)
        {
            if (shouldStop)
                Assert.Ignore("Previous test failed!");

            Assert.That(numbers.NoOdds(nums), Is.EqualTo(expected));
        }

        private static IEnumerable GetRandom()
        {
            var rand = new Random(RandSeed);

            for (var i = 0; i < RandTestCasesCount; i++)
            {
                var nums = new int[rand.Next(RandNumsMinSize, RandNumsMaxSize + 1)];

                for (var j = 0; j < nums.Length; j++)
                    nums[j] = rand.Next();

                yield return new TestCaseData(nums, nums.Where(num => num % 2 == 0).ToArray());
            }
        }

    }
}
