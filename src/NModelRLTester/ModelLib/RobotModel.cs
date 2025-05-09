using System;
using NModel;
using NModel.Attributes;
using NModel.Execution;
using NModel.Internals;

namespace NModelRLTester.ModelLib
{
    public abstract class EnumeratedInstance
    {
        private static int nextId = 0;

        public int Identity { get; }

        protected EnumeratedInstance()
        {
            Identity = nextId++;
        }

        public override string ToString()
        {
            return $"{GetType().Name}#{Identity}";
        }
    }

    public class Robot : EnumeratedInstance
    {
        public int Power { get; set; } = 0;
        public int Reward { get; set; } = 0;

        public Robot() : base() { }

        public override string ToString()
        {
            return $"Robot#{this.Identity}: Power={Power}, Reward={Reward}";
        }
    }

    public class RobotModel
    {
        static int maxNoOfRobots = 5;

        [Action]
        public static void Start(int robotId)
        {
            // guard: not already started
            // update: create new robot
        }

        [Action]
        public static void Search(int robotId)
        {
            // guard: robot.power > 30
            // update: power -= 30, reward += 2
        }

        [Action]
        public static void Wait(int robotId)
        {
            // guard: robot.power <= 50
            // update: reward += 1
        }

        [Action]
        public static void Recharge(int robotId)
        {
            // guard: robot.power < 100
            // update: power = 100
        }
    }
}
