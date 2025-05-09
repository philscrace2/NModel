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
        private Dictionary<int, Robot> robots;
        private readonly Interpreter interpreter;

        public IEnumerable<CompoundTerm> _enabledControllables;
        public IEnumerable<CompoundTerm> _enabledObservables;

        public RobotStepper()
        {
            model = LibraryModelProgram.Create(typeof(RobotModel));
            robot = new RobotModel();
            interpreter = new Interpreter(model);
            Reset();
        }

        public void Reset()
        {
            currentState = model.InitialState;
            robots = new Dictionary<int, Robot>();
        }

        public void DoStep(CompoundTerm action)
        {
            string actionName = action.Name;
            var args = action.Arguments.Select(arg => ((Literal)arg).Value).ToArray();

            switch (actionName)
            {
                case "Start":
                    RobotModel.Start((int)args[0]);
                    //model.Start((int)args[0], robots);
                    break;
                case "Search":
                    RobotModel.Search((int)args[0]);
                    break;
                case "Wait":
                    RobotModel.Wait((int)args[0]);
                    break;
                case "Recharge":
                    RobotModel.Recharge((int)args[0]);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown action: {actionName}");
            }
        }


        public IEnumerable<CompoundTerm> GetEnabledActions() => interpreter.GetEnabledActions();

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
            // You'll need to extract robot state via model state here, or fake it
            RobotModel.Start(0);
            return (-1, -1); // Stub
        }

        public IComparable GetAbstractAction(CompoundTerm action) => action.Name;

        private bool IsObservable(CompoundTerm action) =>
            action.Name == "Recharge";

        public CompoundTerm DoAction(CompoundTerm action)
        {
            return null;
        }

        bool IStepper.Reset()
        {
            throw new NotImplementedException();
        }
    }
}
