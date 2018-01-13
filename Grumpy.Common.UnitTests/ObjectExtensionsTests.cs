using System;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using Grumpy.Common.Extensions;
using Grumpy.Common.UnitTests.Helper;
using Xunit;

namespace Grumpy.Common.UnitTests
{
    [UseReporter(typeof(DiffReporter))]
    public class ObjectExtensionsTests
    {
        [Fact]
        public void ShouldSerializeXml()
        {
            var testObj = new TestPerson
            {
                Age = 8,
                Name = "Sara"
            };

            Approvals.Verify(testObj.SerializeToXml());
        }

        [Fact]
        public void SerializeXml_DoNotFormat_UnformattedXml()
        {
            var testObj = new TestPerson
            {
                Age = 8,
                Name = "Sara"
            };

            testObj.SerializeToXml(false).Should().Be(@"<TestPerson><Age>8</Age><Name>Sara</Name></TestPerson>");
        }

        [Fact]
        public void IsNull_Null_True()
        {
            ((string)null).IsNull().Should().BeTrue();
        }

        [Fact]
        public void IsNull_NotNull_False()
        {
            "".IsNull().Should().BeFalse();
        }

        [Fact]
        public void In_IntergerInList_True()
        {
            Assert.True(1.In(6, 9, 3, 1, 4));
        }

        [Fact]
        public void In_Null_True()
        {
            string s = null;

            Assert.False(s.In("a", "b"));
        }

        [Fact]
        public void In_Bool_Null_True()
        {
            string s = null;

            Assert.True(s.In(null));
        }

        [Fact]
        public void In_StringInList_True()
        {
            Assert.True("1".In("6", "9", "3", "1", "4"));
        }

        [Fact]
        public void In_IntergerNotInList_False()
        {
            Assert.False(1.In(6, 9, 3, 0, 4));
        }

        [Fact]
        public void In_StringNotInList_False()
        {
            Assert.False("1".In("6", "9", "3", " 1 ", "4"));
        }

        [Fact]
        public void In_ValueMultipleTimesInList_True()
        {
            Assert.True("1".In("6", "9", "3", "1", "4", "1"));
        }

        [Fact]
        public void Between_ValueSmallerThanStart_False()
        {
            Assert.False(2.Between(3, 7));
        }

        [Fact]
        public void Between_ValueEqualStart_True()
        {
            Assert.True(3.Between(3, 7));
        }

        [Fact]
        public void Between_ValueInRange_True()
        {
            Assert.True(5.Between(3, 7));
        }

        [Fact]
        public void Between_ValueEqualEnd_True()
        {
            Assert.True(7.Between(3, 7));
        }

        [Fact]
        public void Between_ValueLargerThanEnd_False()
        {
            Assert.False(9.Between(3, 7));
        }

        [Fact]
        public void Between_SmallerWhenStartEqualEnd_False()
        {
            Assert.False(4.Between(5, 5));
        }

        [Fact]
        public void Between_InRangeWhenStartEqualEnd_True()
        {
            Assert.True(5.Between(5, 5));
        }

        [Fact]
        public void Between_LargerWhenStartEqualEnd_False()
        {
            Assert.False(6.Between(5, 5));
        }

        [Fact]
        public void Between_ValueInRangeWhenStartLargerThanEnd_False()
        {
            Assert.False(5.Between(7, 3));
        }

        [Fact]
        public void Between_SmallerValueWhenStartLargerThanEnd_False()
        {
            Assert.False(1.Between(7, 3));
        }

        [Fact]
        public void Between_LargerValueWhenStartLargerThanEnd_False()
        {
            Assert.False(9.Between(7, 3));
        }

        [Fact]
        public void Between_UsedOnDateTime_True()
        {
            Assert.True(DateTime.Now.Between(DateTime.Now.AddMilliseconds(-100), DateTime.Now.AddMilliseconds(100)));
        }

        [Fact]
        public void Between_UsedOnString_True()
        {
            Assert.True("e".Between("c", "f"));
        }

        [Fact]
        public void BetweenExclusive_ValueSmallerThanStart_False()
        {
            Assert.False(2.BetweenExclusive(3, 7));
        }

        [Fact]
        public void BetweenExclusive_ValueEqualStart_False()
        {
            Assert.False(3.BetweenExclusive(3, 7));
        }

        [Fact]
        public void BetweenExclusive_ValueInRange_True()
        {
            Assert.True(5.BetweenExclusive(3, 7));
        }

        [Fact]
        public void BetweenExclusive_ValueEqualEnd_False()
        {
            Assert.False(7.BetweenExclusive(3, 7));
        }

        [Fact]
        public void BetweenExclusive_ValueLargerThanEnd_False()
        {
            Assert.False(9.BetweenExclusive(3, 7));
        }

        [Fact]
        public void BetweenExclusive_SmallerWhenStartEqualEnd_False()
        {
            Assert.False(4.BetweenExclusive(5, 5));
        }

        [Fact]
        public void BetweenExclusive_InRangeWhenStartEqualEnd_False()
        {
            Assert.False(5.BetweenExclusive(5, 5));
        }

        [Fact]
        public void BetweenExclusive_LargerWhenStartEqualEnd_False()
        {
            Assert.False(6.BetweenExclusive(5, 5));
        }

        [Fact]
        public void BetweenExclusive_ValueInRangeWhenStartLargerThanEnd_False()
        {
            Assert.False(5.BetweenExclusive(7, 3));
        }

        [Fact]
        public void BetweenExclusive_SmallerValueWhenStartLargerThanEnd_False()
        {
            Assert.False(1.BetweenExclusive(7, 3));
        }

        [Fact]
        public void BetweenExclusive_LargerValueWhenStartLargerThanEnd_False()
        {
            Assert.False(9.BetweenExclusive(7, 3));
        }

        [Fact]
        public void BetweenExclusive_UsedOnDateTime_True()
        {
            Assert.True(DateTime.Now.BetweenExclusive(DateTime.Now.AddMilliseconds(-100), DateTime.Now.AddMilliseconds(100)));
        }

        [Fact]
        public void BetweenExclusive_DateTimeValueEqualToStart_False()
        {
            var dateTime = DateTime.Now;

            Assert.False(dateTime.BetweenExclusive(dateTime, dateTime.AddDays(1)));
        }

        [Fact]
        public void BetweenExclusive_DateTimeValueOnMillisecondBeforeStart_True()
        {
            var dateTime = DateTime.Now;

            Assert.True(dateTime.BetweenExclusive(dateTime.AddMilliseconds(-1), dateTime.AddDays(1)));
        }

        [Fact]
        public void BetweenExclusive_UsedOnString_True()
        {
            Assert.True("e".BetweenExclusive("c", "f"));
        }

        [Fact]
        public void AsEnumerable_OnInterger_Work()
        {
            const int i = 5;

            Assert.Equal(i, i.AsEnumerable().Single());
        }

        [Fact]
        public void AsEnumerable_OnDouble_Work()
        {
            const double d = 5.4;

            Assert.Equal(d, d.AsEnumerable().Single());
        }

        [Fact]
        public void AsEnumerable_OnString_Work()
        {
            const string s = "string";

            Assert.Equal(s, s.AsEnumerable().Single());
        }

        [Fact]
        public void IsNumber_Work()
        {
            const string s = "string";
            const int i = 1;
            const decimal d = 1;
            const double f = 1;

            i.IsNumber().Should().BeTrue();
            d.IsNumber().Should().BeTrue();
            f.IsNumber().Should().BeTrue();
            s.IsNumber().Should().BeFalse();
        }
    }
}