﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Recurrency;

namespace RecurrencyTests
{
    [TestFixture]
    public class WeeklyTests
    {
        [Test]
        public void Create_FromPattern()
        {
            WeeklyRecurrency weekly = new WeeklyRecurrency("W 20110512 00000000 0156 0050 YNYNYNN");
            Assert.AreEqual(new DateTime(2011, 5, 12), weekly.StartDate);
            Assert.IsNull(weekly.EndDate);
            Assert.AreEqual(156, weekly.Occurrences);
            Assert.AreEqual(50, weekly.Interval);
            Assert.IsTrue(weekly.Monday);
            Assert.IsFalse(weekly.Tuesday);
            Assert.IsTrue(weekly.Wednesday);
            Assert.IsFalse(weekly.Thursday);
            Assert.IsTrue(weekly.Friday);
            Assert.IsFalse(weekly.Saturday);
            Assert.IsFalse(weekly.Sunday);
        }

        [Test]
        public void Create_WithEndDate()
        {
            WeeklyRecurrency weekly = new WeeklyRecurrency(new DateTime(2011, 4, 5), new DateTime(2011, 4, 25), 2, monday: true, tuesday: true, friday: true);
            Assert.AreEqual(new DateTime(2011, 4, 5), weekly.StartDate);
            Assert.AreEqual(new DateTime(2011, 4, 25), weekly.EndDate);
            Assert.AreEqual(0, weekly.Occurrences);
            Assert.AreEqual(2, weekly.Interval);
            
            Assert.IsTrue(weekly.Monday);
            Assert.IsTrue(weekly.Tuesday);
            Assert.IsFalse(weekly.Wednesday);
            Assert.IsFalse(weekly.Thursday);
            Assert.IsTrue(weekly.Friday);
            Assert.IsFalse(weekly.Saturday);
            Assert.IsFalse(weekly.Sunday);
        }

        [Test]
        public void Create_WithOccurrences()
        {
            WeeklyRecurrency weekly = new WeeklyRecurrency(new DateTime(2011, 4, 5), 20, 3, sunday: true, thursday: true, saturday: true);
            Assert.AreEqual(new DateTime(2011, 4, 5), weekly.StartDate);
            Assert.IsNull(weekly.EndDate);
            Assert.AreEqual(20, weekly.Occurrences);
            Assert.AreEqual(3, weekly.Interval);
            
            Assert.IsFalse(weekly.Monday);
            Assert.IsFalse(weekly.Tuesday);
            Assert.IsFalse(weekly.Wednesday);
            Assert.IsTrue(weekly.Thursday);
            Assert.IsFalse(weekly.Friday);
            Assert.IsTrue(weekly.Saturday);
            Assert.IsTrue(weekly.Sunday);
        }


        // April 2011          
        // M  T  W  T  F  S  S   
        //             1  2  3  
        // 4  5  6  7  8  9  10 
        // 11 12 13 14 15 16 17 
        // 18 19 20 21 22 23 24 
        // 25 26 27 28 29 30

        [Test]
        public void GetNext_EverydayEveryWeek()
        {
            WeeklyRecurrency weekly = new WeeklyRecurrency(new DateTime(2011, 4, 1), 5, 1);
            weekly.Days = WeeklyRecurrency.EveryDay;

            Assert.AreEqual(new DateTime(2011, 4, 1), weekly.StartDate);
            Assert.AreEqual(new DateTime(2011, 4, 1), weekly.GetFirstDate());
            Assert.AreEqual(new DateTime(2011, 4, 2), weekly.GetNextDate(new DateTime(2011, 4, 1)));
            Assert.AreEqual(new DateTime(2011, 4, 3), weekly.GetNextDate(new DateTime(2011, 4, 2)));
            Assert.AreEqual(new DateTime(2011, 4, 4), weekly.GetNextDate(new DateTime(2011, 4, 3)));
            Assert.AreEqual(new DateTime(2011, 4, 5), weekly.GetNextDate(new DateTime(2011, 4, 4)));
            Assert.IsNull(weekly.GetNextDate(new DateTime(2011, 4, 5)));
        }



        [Test]
        public void GetNext_WeekendsEvery2ndWeek()
        {
            WeeklyRecurrency weekly = new WeeklyRecurrency(new DateTime(2011, 4, 1), 5, 2);
            weekly.Days = WeeklyRecurrency.WeekEnd;

            // April 2011          
            // M  T  W  T  F  S  S   
            //             1  2  3  << 2,3
            // 4  5  6  7  8  9  10 
            // 11 12 13 14 15 16 17 << 16, 17
            // 18 19 20 21 22 23 24 
            // 25 26 27 28 29 30    << 30

            Assert.AreEqual(new DateTime(2011, 4, 1), weekly.StartDate);
            Assert.AreEqual(new DateTime(2011, 4, 02), weekly.GetFirstDate());
            Assert.AreEqual(new DateTime(2011, 4, 02), weekly.GetNextDate(new DateTime(2011, 4, 1)));  // Fri -> Sat d1
            Assert.AreEqual(new DateTime(2011, 4, 03), weekly.GetNextDate(new DateTime(2011, 4, 2)));  // Sat -> Sun d2 
            Assert.AreEqual(new DateTime(2011, 4, 16), weekly.GetNextDate(new DateTime(2011, 4, 3)));  // Sun -> Sat 2 weeks ahead d3
            Assert.AreEqual(new DateTime(2011, 4, 16), weekly.GetNextDate(new DateTime(2011, 4, 10))); // Sun -> Sat 1 weeks ahead 
            Assert.AreEqual(new DateTime(2011, 4, 17), weekly.GetNextDate(new DateTime(2011, 4, 16))); // Sun -> Sat d4 
            Assert.AreEqual(new DateTime(2011, 4, 30), weekly.GetNextDate(new DateTime(2011, 4, 17))); // Sun -> Sat 2 weeks ahead d5

            Assert.AreEqual(new DateTime(2011, 4, 30), weekly.GetNextDate(new DateTime(2011, 4, 22)));
            Assert.IsNull(weekly.GetNextDate(new DateTime(2011, 4, 30)));
        }

        [Test]
        public void GetNext_WeekdayEvery2ndWeek()
        {
            WeeklyRecurrency weekly = new WeeklyRecurrency(new DateTime(2011, 4, 1), 5, 2);
            weekly.Days = WeeklyRecurrency.WeekEnd;

            // April 2011          
            // M  T  W  T  F  S  S   
            //             1  2  3  << 2,3
            // 4  5  6  7  8  9  10 
            // 11 12 13 14 15 16 17 << 16, 17
            // 18 19 20 21 22 23 24 
            // 25 26 27 28 29 30    << 30

            Assert.AreEqual(new DateTime(2011, 4, 1), weekly.StartDate);
            Assert.AreEqual(new DateTime(2011, 4, 02), weekly.GetFirstDate());  // d1
            Assert.AreEqual(new DateTime(2011, 4, 02), weekly.GetNextDate(new DateTime(2011, 4, 1)));  // Fri -> Sat d1
            Assert.AreEqual(new DateTime(2011, 4, 03), weekly.GetNextDate(new DateTime(2011, 4, 2)));  // Sat -> Sun d2 
            Assert.AreEqual(new DateTime(2011, 4, 16), weekly.GetNextDate(new DateTime(2011, 4, 3)));  // Sun -> Sat 2 weeks ahead d3
            Assert.AreEqual(new DateTime(2011, 4, 16), weekly.GetNextDate(new DateTime(2011, 4, 10))); // Sun -> Sat 1 weeks ahead 
            Assert.AreEqual(new DateTime(2011, 4, 17), weekly.GetNextDate(new DateTime(2011, 4, 16))); // Sun -> Sat d4 
            Assert.AreEqual(new DateTime(2011, 4, 30), weekly.GetNextDate(new DateTime(2011, 4, 17))); // Sun -> Sat 2 weeks ahead d5

            Assert.AreEqual(new DateTime(2011, 4, 30), weekly.GetNextDate(new DateTime(2011, 4, 22)));
            Assert.IsNull(weekly.GetNextDate(new DateTime(2011, 4, 30)));
        }

        [Test]
        public void GetNext_Every2ndSunday()
        {
            WeeklyRecurrency weekly = new WeeklyRecurrency(new DateTime(2011, 4, 3), new DateTime(2011,5,1), 2, sunday: true);

            // April 2011          
            // M  T  W  T  F  S  S  
            //             1  2  3  << 3
            // 4  5  6  7  8  9  10 
            // 11 12 13 14 15 16 17 << 17
            // 18 19 20 21 22 23 24 
            // 25 26 27 28 29 30    << 1

            Assert.AreEqual(new DateTime(2011, 4, 3), weekly.StartDate);    
            Assert.AreEqual(new DateTime(2011, 4, 3), weekly.GetFirstDate());   // d1
            Assert.AreEqual(new DateTime(2011, 4, 17), weekly.GetNextDate(new DateTime(2011, 4, 4)));  // d2
            Assert.AreEqual(new DateTime(2011, 4, 17), weekly.GetNextDate(new DateTime(2011, 4, 10))); 
            Assert.AreEqual(new DateTime(2011, 4, 17), weekly.GetNextDate(new DateTime(2011, 4, 16))); 
            Assert.AreEqual(new DateTime(2011, 5, 01), weekly.GetNextDate(new DateTime(2011, 4, 17))); // d3
            Assert.AreEqual(new DateTime(2011, 5, 01), weekly.GetNextDate(new DateTime(2011, 4, 18)));
            Assert.AreEqual(new DateTime(2011, 5, 01), weekly.GetNextDate(new DateTime(2011, 4, 30))); 

            Assert.IsNull(weekly.GetNextDate(new DateTime(2011, 5, 1)));

        }

        [Test]
        public void GetTypeCode()
        {
            WeeklyRecurrency weekly = new WeeklyRecurrency("W 20110512 00000000 0156 0050 YNYNYNN");
            Assert.AreEqual('W', weekly.GetTypeCode());
        }

        [Test]
        public void GetPattern()
        {
            WeeklyRecurrency weekly = new WeeklyRecurrency("W 20110512 00000000 0156 0050 YNYNYNN");
            Assert.AreEqual("W201105120000000001560050YNYNYNN", weekly.GetPattern());
        }

        [Test]
        public void GetDayNames()
        {
            WeeklyRecurrency weekly = new WeeklyRecurrency(new DateTime(2011, 5, 12), 10, 1, monday: true, tuesday: true);
            Assert.AreEqual("Mon, Tue", weekly.GetDayNames());
            weekly.Days = WeeklyRecurrency.WeekEnd;
            Assert.AreEqual("Sat, Sun", weekly.GetDayNames());
            weekly.Days = WeeklyRecurrency.EveryDay;
            Assert.AreEqual("Mon, Tue, Wed, Thu, Fri, Sat, Sun", weekly.GetDayNames());
        }
        [Test]
        public void ToString()
        {
            WeeklyRecurrency weekly = new WeeklyRecurrency(new DateTime(2011, 5, 12), 10, 1, monday: true, tuesday: true);
            Assert.AreEqual("Every week on Mon, Tue from 12 May 2011 for 10 occurrences", weekly.ToString());

            weekly = new WeeklyRecurrency(new DateTime(2011, 5, 12), new DateTime(2011, 7, 12), 2, monday: true, wednesday: true);
            Assert.AreEqual("Every 2 weeks on Mon, Wed from 12 May 2011 until 12 Jul 2011", weekly.ToString());
        }

        [Test]
        public void GetType()
        {
            var r = new WeeklyRecurrency(DateTime.Today);
            Assert.AreEqual(RecurrencyType.Weekly, r.GetType());
        }
    }
}
