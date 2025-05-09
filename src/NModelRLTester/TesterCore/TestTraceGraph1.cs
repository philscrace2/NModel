using NModel.Terms;
using System;
using System.Collections.Generic;

namespace NModelRLTester.TesterCore
{
    //public enum StrategyKind
    //{
    //    Random,
    //    LeastCost,
    //    RandomizedLeastCost
    //}

    public class TestTraceGraph
    {
        private readonly Dictionary<(IComparable, IComparable), int> freq = new Dictionary<(IComparable, IComparable), int>();
        private readonly IModelStepper model;

        public TestTraceGraph(IModelStepper model)
        {
            this.model = model;
        }

        public int GetWeight(CompoundTerm action)
        {
            var state = model.GetAbstractState(action);
            var act = model.GetAbstractAction(action);
            return freq.TryGetValue((state, act), out var count) ? count : 1;
        }

        public void Update(CompoundTerm action)
        {
            Console.WriteLine($"Updating trace graph with: {action}");
            var state = model.GetAbstractState(action);
            var act = model.GetAbstractAction(action);
            var key = (state, act);

            if (freq.ContainsKey(key))
                freq[key]++;
            else
                freq[key] = 2; // start from 2 to reflect update after first seen
        }
    }
}
