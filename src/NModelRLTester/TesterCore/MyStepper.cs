using NModel.Conformance;
using NModel.Terms;
using System;
using System.Collections.Generic;

namespace NModelRLTester.TesterCore
{
    public class MyStepper : IStepper
    {
        public CompoundTerm DoAction(CompoundTerm action)
        {
            throw new NotImplementedException();
        }

        public bool Reset()
        {
            throw new NotImplementedException();
        }

        public bool ResetMe()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CompoundTerm> EnabledObservables
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IEnumerable<CompoundTerm> EnabledControllables
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
