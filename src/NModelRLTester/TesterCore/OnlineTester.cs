using NModel.Conformance;
using NModel.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NModelRLTester.TesterCore
{
    public class OnlineTester
    {
        IModelStepper model;
        IStepper impl;
        Strategy strategy;
        TestTraceGraph ttg;
        int maxSteps;
        int maxRuns;

        public OnlineTester(IModelStepper model, IStepper implementation, Strategy strategyKind, int maxRuns = 100, int maxSteps = 1000)
        {
            this.model = model;
            this.impl = implementation;
            this.strategy = strategyKind; //new RLStrategy(model, strategyKind);
            this.maxSteps = maxSteps;
            this.maxRuns = maxRuns;
            Ttg = new TestTraceGraph(model);
        }

        public TestTraceGraph Ttg { get => ttg; set => ttg = value; }

        public void RunTests()
        {
            for (int run = 0; run < maxRuns; run++)
            {
                if (!model.ResetEnabled || !impl.Reset()) break;
                model.Reset();
                impl.Reset();

                for (int step = 0; step < maxSteps; step++)
                {
                    var mCtrls = model.EnabledControllables;
                    var iObs = impl.EnabledObservables;

                    if (!mCtrls.Any() && !iObs.Any())
                        break;

                    if (ShouldObserve(mCtrls, iObs))
                    {
                        var obs = iObs.Random(); // stub: actual impl triggers this
                        impl.DoAction(obs);
                        if (model.EnabledObservables.Contains(obs))
                        {
                            Ttg.Update(obs);
                            model.DoAction(obs);
                        }
                        else
                            throw new Exception("Conformance failure (unexpected observable).");
                    }
                    else
                    {
                        var chosen = ActionSelector.Choose(model, mCtrls, strategy, Ttg);
                        Ttg.Update(chosen);
                        model.DoAction(chosen);

                        if (impl.EnabledControllables.Contains(chosen))
                            impl.DoAction(chosen);
                        else
                            throw new Exception("Conformance failure (rejected controllable).");
                    }
                }
            }
        }

        private bool ShouldObserve(IEnumerable<CompoundTerm> mCtrls, IEnumerable<CompoundTerm> iObs)
        {
            // heuristic or random
            return !mCtrls.Any() || (iObs.Any() && new Random().Next(2) == 0);
        }
    }


    public static class EnumerableExtensions
    {
        private static readonly Random _random = new Random();

        public static T Random<T>(this IEnumerable<T> source)
        {
            var list = source.ToList();
            if (list.Count == 0)
                throw new InvalidOperationException("Cannot select a random element from an empty collection.");

            return list[_random.Next(list.Count)];
        }
    }
}
