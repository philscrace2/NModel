using System;
using System.Collections.Generic;
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

    public class Robot
    {
        public int Power { get; set; } = 0;
        public int Reward { get; set; } = 0;
        public int Id { get; set; } = 0;

        public Robot()
        {

        }

        //public override string ToString()
        //{
        //    return $"Robot#{this.Identity}: Power={Power}, Reward={Reward}";
        //}
    }

    public class RobotModel
    {
        public void Start(int id, Dictionary<int, Robot> robots)
        {
            if (!robots.ContainsKey(id))
                robots[id] = new Robot();
        }

        [Action]
        public void Search(int id, Dictionary<int, Robot> robots)
        {
            if (CanSearch(id, robots))
            {
                robots[id].Power -= 30;
                robots[id].Reward += 2;
            }
        }

        [Action]
        public void Wait(int id, Dictionary<int, Robot> robots)
        {
            if (CanWait(id, robots))
            {
                robots[id].Reward += 1;
            }
        }

        [Action]
        public void Recharge(int id, Dictionary<int, Robot> robots)
        {
            if (CanRecharge(id, robots))
            {
                robots[id].Power = 100;
            }
        }

        public bool CanSearch(int id, Dictionary<int, Robot> robots)
        {
            return robots.ContainsKey(id) && robots[id].Power > 30;
        }

        public bool CanWait(int id, Dictionary<int, Robot> robots)
        {
            return robots.ContainsKey(id) && robots[id].Power <= 50;
        }

        public bool CanRecharge(int id, Dictionary<int, Robot> robots)
        {
            return robots.ContainsKey(id) && robots[id].Power < 100;
        }
    }
}
