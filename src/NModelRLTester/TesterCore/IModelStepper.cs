using NModel.Conformance;
using NModel.Internals;
using NModel.Terms;
using NModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NModelRLTester.TesterCore
{
    public interface IModelStepper : IStepper
    {
        IEnumerable<CompoundTerm> EnabledControllables { get; }
        IEnumerable<CompoundTerm> EnabledObservables { get; }

        bool ResetEnabled { get; }

        IComparable GetAbstractState(CompoundTerm action);
        IComparable GetAbstractAction(CompoundTerm action);
    }
}
