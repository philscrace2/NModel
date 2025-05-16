using NModel.Conformance;
using NModel.Execution;
using NModel.Terms;
using NModelRLTester.ModelLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NModelRLTester.TesterCore
{
    public class RobotStepper : IModelStepper
    {
        private ModelProgram model;
        private IState currentState;
        RobotModel robot;
        int maxRobotId = 2;
        private Dictionary<int, Robot> robots;

        public IEnumerable<CompoundTerm> _enabledControllables;
        public IEnumerable<CompoundTerm> _enabledObservables;

        public RobotStepper()
        {
            //model = LibraryModelProgram.Create(typeof(RobotModel));
            robot = new RobotModel();
            Reset();
        }

        public void Reset()
        {
            //currentState = model.InitialState;
            robots = new Dictionary<int, Robot>();
        }

        //public void DoStep(CompoundTerm action)
        //{
        //    Console.WriteLine($"Model executing: {action}");
        //    string actionName = action.Name;
        //    var args = action.Arguments.Select(arg => ((Literal)arg).Value).ToArray();

        //    switch (actionName)
        //    {
        //        case "Start":
        //            robot.Start((int)args[0], robots);
        //            //model.Start((int)args[0], robots);
        //            break;
        //        case "Search":
        //            robot.Search((int)args[0], robots);
        //            break;
        //        case "Wait":
        //            robot.Wait((int)args[0], robots);
        //            break;
        //        case "Recharge":
        //            robot.Recharge((int)args[0], robots);
        //            break;
        //        default:
        //            throw new InvalidOperationException($"Unknown action: {actionName}");
        //    }
        //}


        public IEnumerable<CompoundTerm> GetEnabledActions()
        {
            foreach (var robot in robots.Values)
            {
                int id = robot.Id;
                Console.WriteLine("GetEnabledActions for robot " + robot.Id + " and robot power " + robot.Power);
                // Manually evaluate guards (just like you would in a model program)
                if (robot.Power > 30) // Search guard
                    yield return new CompoundTerm(new Symbol("Search"), new Literal(id));

                if (robot.Power <= 50) // Wait guard
                    yield return new CompoundTerm(new Symbol("Wait"), new Literal(id));

                if (robot.Power < 100) // Recharge guard
                {
                    Console.WriteLine($" → Recharge({id}) enabled");
                    yield return new CompoundTerm(new Symbol("Recharge"), new Literal(id));
                }

            }

            // Optional: Allow Start for uninitialized robots
            for (int id = 0; id < maxRobotId; id++)
            {
                if (!robots.ContainsKey(id))
                    yield return new CompoundTerm(new Symbol("Start"), new Literal(id));
            }
        }

        public IEnumerable<CompoundTerm> EnabledControllables
        {
            get { return GetEnabledActions().Where(a => !IsObservable(a)); }
            set { _enabledControllables = value; } // Implementing the setter
        }

        public IEnumerable<CompoundTerm> EnabledObservables
        {
            get { return GetEnabledActions().Where(a => IsObservable(a)); }
            set { _enabledObservables = value; } // Implementing the setter
        }

        public bool ResetEnabled => true;

        public IComparable GetAbstractState(CompoundTerm action)
        {
            int id = (int)((Literal)action.Arguments[0]).Value;
            if (robots.TryGetValue(id, out var r))
            {
                Console.WriteLine($"State for robot {id}: Power={r.Power}, Reward={r.Reward}");
                return (r.Power, r.Reward);
            }

            return (-1, -1); // Unknown robot, fallback
        }

        public IComparable GetAbstractAction(CompoundTerm action) => action.Name;

        private bool IsObservable(CompoundTerm action) =>
            action.Name == "Recharge";

        public CompoundTerm DoAction(CompoundTerm action)
        {
            Console.WriteLine($"Model executing: {action}");
            string actionName = action.Name;
            var args = action.Arguments.Select(arg => ((Literal)arg).Value).ToArray();

            switch (actionName)
            {
                case "Start":
                    robot.Start((int)args[0], robots);
                    //model.Start((int)args[0], robots);
                    break;
                case "Search":
                    robot.Search((int)args[0], robots);
                    break;
                case "Wait":
                    robot.Wait((int)args[0], robots);
                    break;
                case "Recharge":
                    robot.Recharge((int)args[0], robots);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown action: {actionName}");
            }

            return null;
        }

        bool IStepper.Reset()
        {
            //currentState = model.InitialState;
            robots = new Dictionary<int, Robot>();

            return true;
        }


    }
}
