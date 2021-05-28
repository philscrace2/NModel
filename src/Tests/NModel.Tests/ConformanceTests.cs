using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NModel.Tests
{
    [TestFixture]
    public class ConformanceTests
    {
        [Test]
        public void Test1()
        {
            Conformance.ConformanceTester.RunWithCommandLineArguments(new[] { @"/r:C:\source\NModel\bin\RobotModel.dll", "RobotModel1.Factory.Create", 
                "/r:RobotImpl.dll", "/iut:RobotImpl1.Stepper.Create", "/runs:10" });


        }

        [Test]
        public void Test2()
        {
            Conformance.ConformanceTester.RunWithCommandLineArguments(new[] { @"/r:C:\source\NModel\bin\RobotModel.dll", "RobotModel1.Factory.Create" });


        }
    }
}
