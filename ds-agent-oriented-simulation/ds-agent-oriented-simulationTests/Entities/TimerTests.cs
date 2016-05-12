using Microsoft.VisualStudio.TestTools.UnitTesting;
using ds_agent_oriented_simulation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ds_agent_oriented_simulation.Entities.Tests
{
    [TestClass()]
    public class TimerTests
    {
        [TestMethod()]
        public void ToHoursTest()
        {
            double result = Timer.ToHours(6570);
            double expected = 13.5;
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void NewWorkDayStartsAtTest()
        {
            double currentTime = 6570; // 13:30 piatok
            double workStartsAt = 7; // za 17.5 hodiny 
            double expected = 7620;
            double result = Timer.NewWorkDayStartsAt(currentTime, workStartsAt);
            Assert.AreEqual(expected, result);

             currentTime = 6570; // 13:30 piatok
             workStartsAt = 14; // za 0.5 hodiny 
             expected = 6600;
             result = Timer.NewWorkDayStartsAt(currentTime, workStartsAt);
            Assert.AreEqual(expected, result);
        }
    }
}