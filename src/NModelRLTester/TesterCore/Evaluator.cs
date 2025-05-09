//using NModel.Execution;
//using NModel.Terms;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace NModelRLTester.TesterCore
//{
//    public static class Evaluator
//    {
//        public static IEnumerable<CompoundTerm> GetEnabledActions(IState state, ActionTable actionTable)
//        {
//            foreach (var entry in actionTable)
//            {
//                string actionName = entry.Key;
//                foreach (var rule in entry.Value)
//                {
//                    var argumentCombinations = rule.GetAllArgumentCombinations(state);
//                    foreach (var args in argumentCombinations)
//                    {
//                        CompoundTerm action = new CompoundTerm(actionName, args);
//                        if (rule.Guard(state, args))
//                        {
//                            yield return action;
//                        }
//                    }
//                }
//            }
//        }

//        public static IState Apply(IState state, CompoundTerm action, ActionTable actionTable)
//        {
//            if (!actionTable.TryGetValue(action.Name, out var rules))
//                throw new InvalidOperationException($"Action '{action.Name}' not found in action table.");

//            foreach (var rule in rules)
//            {
//                if (rule.Matches(action) && rule.Guard(state, action.Arguments))
//                {
//                    return rule.Update(state, action.Arguments);
//                }
//            }

//            throw new InvalidOperationException($"No matching rule found for action '{action}'.");
//        }
//    }
//}
