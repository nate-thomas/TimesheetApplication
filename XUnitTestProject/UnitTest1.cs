using System;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void PassTest()
        {
            int val1 = 3;
            int val2 = 45;

            Assert.Equal(48, Add(val1, val2));
        }

        [Fact]
        public void PassTest2()
        {
            int val1 = 10;
            int val2 = 100;

            Assert.Equal(110, Add(val1, val2));
        }

        int Add(int num1, int num2)
        {
            return num1 + num2;
        }

        [Fact]
        public void PassTest3()
        {
            int val1 = 150;
            int val2 = 1000;

            Assert.Equal(1150, Add(val1, val2));
        }

    }
}
