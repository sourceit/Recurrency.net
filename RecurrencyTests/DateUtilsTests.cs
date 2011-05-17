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


        [Test]
        public void ChangeDay_DayOfMonth()
        {
            Assert.AreEqual(new DateTime(2011, 4, 20), new DateTime(2011, 4, 04).ChangeDay(20));
            Assert.AreEqual(new DateTime(2011, 4, 20), new DateTime(2011, 4, 20).ChangeDay(20));
            Assert.AreEqual(new DateTime(2011, 4, 01), new DateTime(2011, 4, 20).ChangeDay(01));
        }

        [Test]
        public void ChangeDay_GoesIntoNextMonth()
        {
            Assert.AreEqual(new DateTime(2011, 5, 01), new DateTime(2011, 4, 20).ChangeDay(31));
        }

        // April 2011          
        // M  T  W  T  F  S  S   
        //             1  2  3  
        // 4  5  6  7  8  9  10 
        // 11 12 13 14 15 16 17 
        // 18 19 20 21 22 23 24 
        // 25 26 27 28 29 30
        [Test]
        public void ChangeDay_DayOfWeek()
        {
            Assert.AreEqual(new DateTime(2011, 4, 01), new DateTime(2011, 4, 04).ChangeDay(DayOfWeek.Friday, DayIndex.First ));
            Assert.AreEqual(new DateTime(2011, 4, 08), new DateTime(2011, 4, 04).ChangeDay(DayOfWeek.Friday, DayIndex.Second));
            Assert.AreEqual(new DateTime(2011, 4, 15), new DateTime(2011, 4, 04).ChangeDay(DayOfWeek.Friday, DayIndex.Third ));
            Assert.AreEqual(new DateTime(2011, 4, 22), new DateTime(2011, 4, 04).ChangeDay(DayOfWeek.Friday, DayIndex.Fourth));
            Assert.AreEqual(new DateTime(2011, 4, 29), new DateTime(2011, 4, 04).ChangeDay(DayOfWeek.Friday, DayIndex.Last  ));  // last

            Assert.AreEqual(new DateTime(2011, 4, 04), new DateTime(2011, 4, 04).ChangeDay(DayOfWeek.Monday, DayIndex.First));
            Assert.AreEqual(new DateTime(2011, 4, 11), new DateTime(2011, 4, 04).ChangeDay(DayOfWeek.Monday, DayIndex.Second));
            Assert.AreEqual(new DateTime(2011, 4, 18), new DateTime(2011, 4, 04).ChangeDay(DayOfWeek.Monday, DayIndex.Third));
            Assert.AreEqual(new DateTime(2011, 4, 25), new DateTime(2011, 4, 04).ChangeDay(DayOfWeek.Monday, DayIndex.Fourth));
            Assert.AreEqual(new DateTime(2011, 4, 25), new DateTime(2011, 4, 04).ChangeDay(DayOfWeek.Monday, DayIndex.Last));  // last

            Assert.AreEqual(new DateTime(2011, 4, 07), new DateTime(2011, 4, 04).ChangeDay(DayOfWeek.Thursday, DayIndex.First));
            Assert.AreEqual(new DateTime(2011, 4, 14), new DateTime(2011, 4, 04).ChangeDay(DayOfWeek.Thursday, DayIndex.Second));
            Assert.AreEqual(new DateTime(2011, 4, 21), new DateTime(2011, 4, 04).ChangeDay(DayOfWeek.Thursday, DayIndex.Third));
            Assert.AreEqual(new DateTime(2011, 4, 28), new DateTime(2011, 4, 04).ChangeDay(DayOfWeek.Thursday, DayIndex.Fourth));
            Assert.AreEqual(new DateTime(2011, 4, 28), new DateTime(2011, 4, 04).ChangeDay(DayOfWeek.Thursday, DayIndex.Last));  // last

            Assert.AreEqual(new DateTime(2011, 4, 03), new DateTime(2011, 4, 04).ChangeDay(DayOfWeek.Sunday, DayIndex.First));
            Assert.AreEqual(new DateTime(2011, 4, 10), new DateTime(2011, 4, 04).ChangeDay(DayOfWeek.Sunday, DayIndex.Second));
            Assert.AreEqual(new DateTime(2011, 4, 17), new DateTime(2011, 4, 04).ChangeDay(DayOfWeek.Sunday, DayIndex.Third));
            Assert.AreEqual(new DateTime(2011, 4, 24), new DateTime(2011, 4, 04).ChangeDay(DayOfWeek.Sunday, DayIndex.Fourth));
            Assert.AreEqual(new DateTime(2011, 4, 24), new DateTime(2011, 4, 04).ChangeDay(DayOfWeek.Sunday, DayIndex.Last));  // last
        }
    }
}
