using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Recurrency;

namespace RecurrencyTests
{

    // April 2011          
    // M  T  W  T  F  S  S   
    //             1  2  3  
    // 4  5  6  7  8  9  10 
    // 11 12 13 14 15 16 17 
    // 18 19 20 21 22 23 24 
    // 25 26 27 28 29 30

    [TestFixture]
    public class DateUtilsTests
    {
        [Test]
        public void DayOfWeek_Mondayised()
        {
            Assert.AreEqual(0, new DateTime(2011, 4, 4).DayOfWeek_Mondayised());  // monday
            Assert.AreEqual(1, new DateTime(2011, 4, 5).DayOfWeek_Mondayised());  // tues
            Assert.AreEqual(2, new DateTime(2011, 4, 6).DayOfWeek_Mondayised());  // wed
            Assert.AreEqual(3, new DateTime(2011, 4, 7).DayOfWeek_Mondayised());  // thurs
            Assert.AreEqual(4, new DateTime(2011, 4, 8).DayOfWeek_Mondayised());  // fri
            Assert.AreEqual(5, new DateTime(2011, 4, 9).DayOfWeek_Mondayised());  // sat
            Assert.AreEqual(6, new DateTime(2011, 4, 10).DayOfWeek_Mondayised()); // sun
            Assert.AreEqual(0, new DateTime(2011, 4, 11).DayOfWeek_Mondayised()); // mon
        }

        [Test]
        public void FirstDayOfWeek_Mondayised()
        {
            Assert.AreEqual(new DateTime(2011, 4, 4), new DateTime(2011, 4, 4).FirstDayOfWeek_Mondayised()); // monday
            Assert.AreEqual(new DateTime(2011, 4, 4), new DateTime(2011, 4, 5).FirstDayOfWeek_Mondayised()); // tues
            Assert.AreEqual(new DateTime(2011, 4, 4), new DateTime(2011, 4, 6).FirstDayOfWeek_Mondayised()); // wed
            Assert.AreEqual(new DateTime(2011, 4, 4), new DateTime(2011, 4, 7).FirstDayOfWeek_Mondayised()); // thur
            Assert.AreEqual(new DateTime(2011, 4, 4), new DateTime(2011, 4, 8).FirstDayOfWeek_Mondayised()); // fri
            Assert.AreEqual(new DateTime(2011, 4, 4), new DateTime(2011, 4, 9).FirstDayOfWeek_Mondayised()); // sat
            Assert.AreEqual(new DateTime(2011, 4, 4), new DateTime(2011, 4, 10).FirstDayOfWeek_Mondayised()); // sun
            Assert.AreEqual(new DateTime(2011, 4, 11), new DateTime(2011, 4, 11).FirstDayOfWeek_Mondayised()); // monday
        }
    }
}
