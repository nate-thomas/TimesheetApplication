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

//         [Fact]
//         public void FailTest()
//         {
//             int x = 3000;
//             int y = 6001;

//             Assert.Equal(9000, Add(x, y));
//         }

        int Add(int num1, int num2)
        {
            return num1 + num2;
        }
    }
}
