using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCar()
        {
            Debug.WriteLine("TestCar.");
            float VExpected1 = 6.0f;
            float VExpected2 = 2.0f;
            SpeedyCars.Car auto = new SpeedyCars.Car(0.0f, 2.0f, 1.0f, 2.4f);
            auto.updateVelocity(3.0f, true);
            Assert.AreEqual(VExpected1, auto.getVelocity());
            auto.updateVelocity(4.0f, false);
            Assert.AreEqual(VExpected2, auto.getVelocity());
            Assert.AreEqual(2.4f, auto.getLength());
        }
        [TestMethod]
        public void TestRoad()
        {
            List<float> numbers = new List<float>() { 5.0f, 30.0f, 50.0f, 80.0f };
            SpeedyCars.Road weg = new SpeedyCars.Road(100.0f, numbers);
            Assert.AreEqual(5.0f, weg.getNextLightDistance(0.0f));
            Assert.AreEqual(5.0f, weg.getNextLightDistance(25.0f));
            Assert.AreEqual(20.0f, weg.getNextLightDistance(60.0f));
            Assert.AreEqual(numbers, weg.getLights());
        }
        [TestMethod]
        public void TestCarLength()
        {
            Assert.AreEqual(12.0f, SpeedyCars.Calculate.CarLength(5.0f, 2.0f, 2.0f));
        }
    }
}
