using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Recurrency;

namespace RecurrencyTests
{
    [TestFixture]
    public class BaseRecurrencyTests
    {
        [Test]
        public void CreateFromPattern_EndDate()
        {
            DailyRecurrency daily = new DailyRecurrency("D2011051220110522000000000050X");
            Assert.AreEqual(new DateTime(2011, 5, 12), daily.StartDate);
            Assert.AreEqual(new DateTime(2011, 5, 22), daily.EndDate);
        }

        [Test]
        public void CreateFromPattern_WithSpaces()
        {
            DailyRecurrency daily = new DailyRecurrency("D 20110512 20110522 000000 000050 X");
            Assert.AreEqual(new DateTime(2011, 5, 12), daily.StartDate);
            Assert.AreEqual(new DateTime(2011, 5, 22), daily.EndDate);
        }

        [Test]
        public void CreateFromPattern_Occurences()
        {
            DailyRecurrency daily = new DailyRecurrency("D2011051200000000000156000050X");
            Assert.AreEqual(new DateTime(2011, 5, 12), daily.StartDate);
            Assert.IsNull(daily.EndDate);
            Assert.AreEqual(156, daily.Occurrences);
        }

        [Test]
        public void GetDateFromPattern()
        {
            Assert.IsNull(BaseRecurrency.GetDateFromPattern("00000000", 0));

            Assert.AreEqual(new DateTime(2011, 5, 30), BaseRecurrency.GetDateFromPattern("20110530", 0));
            Assert.AreEqual(new DateTime(2011, 5, 30), BaseRecurrency.GetDateFromPattern("D20110530xxxx", 1));
        }

        [Test]
        public void GetIntFromPattern()
        {
            Assert.AreEqual(2011, BaseRecurrency.GetIntFromPattern("20110530", 0, 4));
            Assert.AreEqual(530, BaseRecurrency.GetIntFromPattern("D20110530xxxx", 5, 4));
        }

        [Test]
        public void GetInitialPattern_EndDate()
        {
            var daily = new DailyRecurrency(new DateTime(2011, 10, 7), new DateTime(2011, 12, 25), interval: 1234);
            Assert.AreEqual("2011100720111225000000001234", daily.GetInitialPattern());
        }

        [Test]
        public void GetInitialPattern_Occurences()
        {
            var daily = new DailyRecurrency(new DateTime(2011, 10, 7), 52, interval: 1234);
            Assert.AreEqual("2011100700000000000052001234", daily.GetInitialPattern());
        }
    }
}
