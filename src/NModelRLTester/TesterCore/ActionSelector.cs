using NModel.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NModelRLTester.TesterCore
{

    public static class ActionSelector
    {
        public static CompoundTerm Choose(IModelStepper model, IEnumerable<CompoundTerm> actions, Strategy strategy, TestTraceGraph ttg)
        {
            var list = actions.ToList();
            Console.WriteLine($"\n[ActionSelector] Choosing from {list.Count} actions using {strategy}");

            Random random = new Random();

            switch (strategy)
            {
                case Strategy.LeastCost:
                    var min = actions.Min(a => ttg.GetWeight(a));
                    var minActions = list.Where(a =>
                        ttg.GetWeight(a) == min).ToList();
                    Console.WriteLine($" → LeastCost = {min}");
                    foreach (var a in minActions)
                        Console.WriteLine($"    - {a}");

                    return minActions[random.Next(minActions.Count)];

                case Strategy.RandomizedLeastCost:
                    var weights = actions.Select(a =>
                    {
                        var w = ttg.GetWeight(a);
                        Console.WriteLine($" - {a} : weight={w}, score={1.0 / w}");
                        return (a, score: 1.0 / w);
                    }).ToList();

                    if (random.NextDouble() < 0.2)
                    {
                        var randomPick = list[random.Next(list.Count)];
                        Console.WriteLine($" → Exploring randomly: {randomPick}");
                        return randomPick;
                    }

                    var selected = WeightedRandom(weights);
                    Console.WriteLine($" → Selected: {selected}");
                    return selected;

                default:
                    return actions.Random();
            }
        }

        private static CompoundTerm WeightedRandom(List<(CompoundTerm action, double score)> items)
        {
            var total = items.Sum(i => i.score);
            var r = new Random().NextDouble() * total;
            double sum = 0;

            foreach (var item in items)
            {
                sum += item.score;
                if (sum >= r)
                    return item.action;
            }

            return items.Last().action;
        }
    }
}
