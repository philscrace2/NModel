using NModel;
using NModel.Conformance;
using NModel.Execution;
using NModel.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NModelRLTester.TesterCore
{
    public class RLStrategy : IStrategy
    {
        private readonly IModelStepper model;
        private readonly TestTraceGraph traceGraph;
        private readonly Strategy strategy;
        private readonly Random random = new Random();

        public RLStrategy(IModelStepper model, Strategy strategy = Strategy.RandomizedLeastCost)
        {
            this.model = model;
            this.traceGraph = new TestTraceGraph(model);
            this.strategy = strategy;
        }

        public Set<Symbol> ActionSymbols => throw new NotImplementedException();

        public Set<Symbol> ObservableActionSymbols { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IState CurrentState => throw new NotImplementedException();

        public bool IsInAcceptingState => throw new NotImplementedException();

        public CompoundTerm ChooseAction(IEnumerable<CompoundTerm> enabledActions)
        {
            var actions = enabledActions.ToList();
            if (actions.Count == 0) throw new InvalidOperationException("No enabled actions to choose from.");

            switch (strategy)
            {
                case Strategy.LeastCost:
                    int minCost = actions.Min(a => traceGraph.GetWeight(a));
                    var minActions = actions.Where(a => traceGraph.GetWeight(a) == minCost).ToList();
                    return minActions[random.Next(minActions.Count)];

                case Strategy.RandomizedLeastCost:
                    var weights = actions.Select(a => new
                    {
                        Action = a,
                        Score = 1.0 / traceGraph.GetWeight(a)
                    }).ToList();

                    double total = weights.Sum(w => w.Score);
                    double pick = random.NextDouble() * total;

                    double running = 0;
                    foreach (var item in weights)
                    {
                        running += item.Score;
                        if (running >= pick)
                            return item.Action;
                    }
                    return weights.Last().Action; // fallback

                default:
                    return actions[random.Next(actions.Count)];
            }
        }

        public void DoAction(CompoundTerm action)
        {
            throw new NotImplementedException();
        }

        public bool IsActionEnabled(CompoundTerm action, out string failureReason)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public CompoundTerm SelectAction(Set<Symbol> actionSymbols)
        {
            throw new NotImplementedException();
        }

        public void Update(CompoundTerm executedAction)
        {
            traceGraph.Update(executedAction);
        }
    }
}
