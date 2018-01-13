using System;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Grumpy.Common.Extensions;
using Grumpy.Common.UnitTests.Helper;

namespace Grumpy.Common.UnitTests
{
    [UseReporter(typeof(DiffReporter))]
    [TestClass]
    public class ObjectExtensionsTests
    {
        [TestMethod]
        public void ShouldSerializeXml()
        {
            var testObj = new TestPerson
            {
                Age = 8,
                Name = "Sara"
            };

            Approvals.Verify(testObj.SerializeToXml());
        }

        [TestMethod]
        public void SerializeXml_DoNotFormat_UnformattedXml()
        {
            var testObj = new TestPerson
            {
                Age = 8,
                Name = "Sara"
            };

            testObj.SerializeToXml(false).Should().Be(@"<TestPerson><Age>8</Age><Name>Sara</Name></TestPerson>");
        }

        [TestMethod]
        public void IsNull_Null_True()
        {
            ((string)null).IsNull().Should().BeTrue();
        }

        [TestMethod]
        public void IsNull_NotNull_False()
        {
            "".IsNull().Should().BeFalse();
        }

        [TestMethod]
        public void In_IntergerInList_True()
        {
            Assert.IsTrue(1.In(6, 9, 3, 1, 4));
        }

        [TestMethod]
        public void In_Null_True()
        {
            string s = null;

            Assert.IsFalse(s.In("a", "b"));
        }

        [TestMethod]
        public void In_Bool_Null_True()
        {
            string s = null;

            Assert.IsTrue(s.In(null));
        }

        [TestMethod]
        public void In_StringInList_True()
        {
            Assert.IsTrue("1".In("6", "9", "3", "1", "4"));
        }

        [TestMethod]
        public void In_IntergerNotInList_False()
        {
            Assert.IsFalse(1.In(6, 9, 3, 0, 4));
        }

        [TestMethod]
        public void In_StringNotInList_False()
        {
            Assert.IsFalse("1".In("6", "9", "3", " 1 ", "4"));
        }

        [TestMethod]
        public void In_ValueMultipleTimesInList_True()
        {
            Assert.IsTrue("1".In("6", "9", "3", "1", "4", "1"));
        }

        [TestMethod]
        public void Between_ValueSmallerThanStart_False()
        {
            Assert.IsFalse(2.Between(3, 7));
        }

        [TestMethod]
        public void Between_ValueEqualStart_True()
        {
            Assert.IsTrue(3.Between(3, 7));
        }

        [TestMethod]
        public void Between_ValueInRange_True()
        {
            Assert.IsTrue(5.Between(3, 7));
        }

        [TestMethod]
        public void Between_ValueEqualEnd_True()
        {
            Assert.IsTrue(7.Between(3, 7));
        }

        [TestMethod]
        public void Between_ValueLargerThanEnd_False()
        {
            Assert.IsFalse(9.Between(3, 7));
        }

        [TestMethod]
        public void Between_SmallerWhenStartEqualEnd_False()
        {
            Assert.IsFalse(4.Between(5, 5));
        }

        [TestMethod]
        public void Between_InRangeWhenStartEqualEnd_True()
        {
            Assert.IsTrue(5.Between(5, 5));
        }

        [TestMethod]
        public void Between_LargerWhenStartEqualEnd_False()
        {
            Assert.IsFalse(6.Between(5, 5));
        }

        [TestMethod]
        public void Between_ValueInRangeWhenStartLargerThanEnd_False()
        {
            Assert.IsFalse(5.Between(7, 3));
        }

        [TestMethod]
        public void Between_SmallerValueWhenStartLargerThanEnd_False()
        {
            Assert.IsFalse(1.Between(7, 3));
        }

        [TestMethod]
        public void Between_LargerValueWhenStartLargerThanEnd_False()
        {
            Assert.IsFalse(9.Between(7, 3));
        }

        [TestMethod]
        public void Between_UsedOnDateTime_True()
        {
            Assert.IsTrue(DateTime.Now.Between(DateTime.Now.AddMilliseconds(-100), DateTime.Now.AddMilliseconds(100)));
        }

        [TestMethod]
        public void Between_UsedOnString_True()
        {
            Assert.IsTrue("e".Between("c", "f"));
        }

        [TestMethod]
        public void BetweenExclusive_ValueSmallerThanStart_False()
        {
            Assert.IsFalse(2.BetweenExclusive(3, 7));
        }

        [TestMethod]
        public void BetweenExclusive_ValueEqualStart_False()
        {
            Assert.IsFalse(3.BetweenExclusive(3, 7));
        }

        [TestMethod]
        public void BetweenExclusive_ValueInRange_True()
        {
            Assert.IsTrue(5.BetweenExclusive(3, 7));
        }

        [TestMethod]
        public void BetweenExclusive_ValueEqualEnd_False()
        {
            Assert.IsFalse(7.BetweenExclusive(3, 7));
        }

        [TestMethod]
        public void BetweenExclusive_ValueLargerThanEnd_False()
        {
            Assert.IsFalse(9.BetweenExclusive(3, 7));
        }

        [TestMethod]
        public void BetweenExclusive_SmallerWhenStartEqualEnd_False()
        {
            Assert.IsFalse(4.BetweenExclusive(5, 5));
        }

        [TestMethod]
        public void BetweenExclusive_InRangeWhenStartEqualEnd_False()
        {
            Assert.IsFalse(5.BetweenExclusive(5, 5));
        }

        [TestMethod]
        public void BetweenExclusive_LargerWhenStartEqualEnd_False()
        {
            Assert.IsFalse(6.BetweenExclusive(5, 5));
        }

        [TestMethod]
        public void BetweenExclusive_ValueInRangeWhenStartLargerThanEnd_False()
        {
            Assert.IsFalse(5.BetweenExclusive(7, 3));
        }

        [TestMethod]
        public void BetweenExclusive_SmallerValueWhenStartLargerThanEnd_False()
        {
            Assert.IsFalse(1.BetweenExclusive(7, 3));
        }

        [TestMethod]
        public void BetweenExclusive_LargerValueWhenStartLargerThanEnd_False()
        {
            Assert.IsFalse(9.BetweenExclusive(7, 3));
        }

        [TestMethod]
        public void BetweenExclusive_UsedOnDateTime_True()
        {
            Assert.IsTrue(DateTime.Now.BetweenExclusive(DateTime.Now.AddMilliseconds(-100), DateTime.Now.AddMilliseconds(100)));
        }

        [TestMethod]
        public void BetweenExclusive_DateTimeValueEqualToStart_False()
        {
            var dateTime = DateTime.Now;

            Assert.IsFalse(dateTime.BetweenExclusive(dateTime, dateTime.AddDays(1)));
        }

        [TestMethod]
        public void BetweenExclusive_DateTimeValueOnMillisecondBeforeStart_True()
        {
            var dateTime = DateTime.Now;

            Assert.IsTrue(dateTime.BetweenExclusive(dateTime.AddMilliseconds(-1), dateTime.AddDays(1)));
        }

        [TestMethod]
        public void BetweenExclusive_UsedOnString_True()
        {
            Assert.IsTrue("e".BetweenExclusive("c", "f"));
        }

        [TestMethod]
        public void AsEnumerable_OnInterger_Work()
        {
            const int i = 5;

            Assert.AreEqual(i, i.AsEnumerable().Single());
        }

        [TestMethod]
        public void AsEnumerable_OnDouble_Work()
        {
            const double d = 5.4;

            Assert.AreEqual(d, d.AsEnumerable().Single());
        }

        [TestMethod]
        public void AsEnumerable_OnString_Work()
        {
            const string s = "string";

            Assert.AreEqual(s, s.AsEnumerable().Single());
        }

        [TestMethod]
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