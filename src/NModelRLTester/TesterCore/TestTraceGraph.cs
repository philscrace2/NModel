using NModel.Terms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

        public void PrintSummary()
        {
            Console.WriteLine($"Total unique (state, action) pairs: {freq.Count}");

            var grouped = freq
                .GroupBy(kvp => kvp.Key.Item1)
                .Select(g => (Action: g.Key, Count: g.Count()))
                .OrderByDescending(x => x.Count);

            foreach (var entry in grouped)
                Console.WriteLine($" - {entry.Action} visited {entry.Count} times");
        }

        public void PrintSummary1()
        {
            Console.WriteLine($"Total unique (state, action) pairs: {freq.Count}");

            foreach (var entry in freq.OrderByDescending(kvp => kvp.Value))
            {
                string state = FormatState(entry.Key.Item1);
                string action = FormatAction(entry.Key.Item2);
                int count = entry.Value;

                Console.WriteLine($" - STATE: {state} | ACTION: {action} | VISITS: {count}");
            }

        }

        private string FormatState(IComparable state)
        {
            if (state is ValueTuple<int, int> s)
                return $"Power={s.Item1}, Reward={s.Item2}";

            return state.ToString();
        }

        private string FormatAction(IComparable action)
        {
            if (action is CompoundTerm term)
            {
                var args = string.Join(", ", term.Arguments.Select(a => a.ToString()));
                return $"{term.Name}({args})";
            }

            return action.ToString();
        }

        public void ExportToCsv(string filePath)
        {
            var writer = new StreamWriter(filePath, false, Encoding.UTF8);
            writer.WriteLine("State,Action,VisitCount");

            foreach (var entry in freq)
            {
                var state = entry.Key.Item1.ToString().Replace(",", ";");
                var action = entry.Key.Item2.ToString().Replace(",", ";");
                writer.WriteLine($"{state},{action},{entry.Value}");
            }

            Console.WriteLine($"📄 Trace graph exported to: {filePath}");
        }
    }
}
