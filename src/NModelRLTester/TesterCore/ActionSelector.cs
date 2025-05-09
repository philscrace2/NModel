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
            switch (strategy)
            {
                case Strategy.LeastCost:
                    var min = actions.Min(a => ttg.GetWeight(a));
                    return actions.Where(a =>
                        ttg.GetWeight(a) == min).Random();

                case Strategy.RandomizedLeastCost:
                    var weights = actions.Select(a =>
                    {
                        var w = ttg.GetWeight(a);
                        return (a, score: 1.0 / w);
                    }).ToList();

                    return WeightedRandom(weights);

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
