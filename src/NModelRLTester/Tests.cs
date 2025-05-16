using NModel.Conformance;
using NModelRLTester.TesterCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NModelRLTester
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void RunAlgorithm()
        {
            //Build an OnlineTester
            // 1. Create the model stepper (uses ModelProgram + Interpreter internally)
            IModelStepper model = new RobotStepper();

            // 2. Provide a dummy implementation (could be a stub or mock)
            IStepper implementation = new RobotStepper(); // or a real SUT IStepper

            // 3. Choose the strategy (Random, LeastCost, RandomizedLeastCost)
            TesterCore.Strategy strategy = TesterCore.Strategy.Random;

            // 4. Instantiate the tester
            var tester = new OnlineTester(model, implementation, strategy, maxRuns: 2, maxSteps: 10);

            // 5. Run the test loop
            tester.RunTests();

            tester.Ttg.PrintSummary();

            Console.WriteLine("Test run completed.");

        }
    }
}
