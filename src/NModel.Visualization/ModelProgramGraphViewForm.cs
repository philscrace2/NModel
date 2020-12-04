//-------------------------------------
// Copyright (c) Microsoft Corporation
//-------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NModel;
using NModel.Utilities;
using System.Reflection;
using NModel.Execution;
using NModel.Terms;
using NModel.Algorithms;

namespace NModel.Visualization
{
    /// <summary>
    /// Used for creating a form that contains the ModelProgramGraphView component.
    /// </summary>
    public partial class ModelProgramGraphViewForm : Form
    {
        /// <summary>
        /// Creates a form containing the ModelProgramGraphView component.
        /// With an optional panel if testMode is true 
        /// that contains Yes/No buttons that close the form 
        /// and return either DialogResult.Yes or DialogResult.No
        /// </summary>
        /// <param name="title">used as the title of the form if title != null</param>
        /// <param name="testMode">if true shows a testpanel</param>
        public ModelProgramGraphViewForm(string title, bool testMode)
        {
            InitializeComponent();
            if (title != null)
                this.Text = title;
            if (testMode)
                this.panelForTestMode.Visible = true;  
        }

        /// <summary>
        /// Creates a form containing the ModelProgramGraphView component.
        /// </summary>
        /// <param name="title">used as the title of the form if title != null</param>
        public ModelProgramGraphViewForm(string title)
        {
            InitializeComponent();
            if (title != null)
                this.Text = title;
        }

        /// <summary>
        /// Gets the ModelProgramGraphView component.
        /// </summary>
        public ModelProgramGraphView View
        {
            get
            {
                return this.modelProgramGraphView1;
            }
        }

        //private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}

        /// <summary>
        /// Called when Save Settings menu item is selected
        /// </summary>
        public event EventHandler OnSaveSettings;

        private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnSaveSettings != null)
                OnSaveSettings(this, e);
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            List<string> argsList = new List<string>();

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "NModel dll files (*.dll)|*.dll|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }
            argsList.Add("/r:" + filePath);
            argsList.Add("NewsReader.Factory.CreateNewsReader");
            doSomethingWithTheArgs(argsList.ToArray());
        }

        private void doSomethingWithTheArgs(string[] args)
        {
            ProgramSettings settings = new ProgramSettings();
            if (!Parser.ParseArgumentsWithUsage(args, settings))
            {
                return;
            }

            #region load the libraries
            List<Assembly> libs = new List<Assembly>();
            try
            {

                if (settings.reference != null)
                {
                    foreach (string l in settings.reference)
                    {
                        libs.Add(System.Reflection.Assembly.LoadFrom(l));
                    }
                }
            }
            catch (Exception e)
            {
                throw new ModelProgramUserException(e.Message);
            }
            #endregion

            #region create a model program for each model using the factory method and compose into product
            string mpMethodName;
            string mpClassName;
            ModelProgram mp = null;
            if (settings.model != null && settings.model.Length > 0)
            {
                if (libs.Count == 0)
                {
                    throw new ModelProgramUserException("No reference was provided to load models from.");
                }
                ReflectionHelper.SplitFullMethodName(settings.model[0], out mpClassName, out mpMethodName);
                Type mpType = ReflectionHelper.FindType(libs, mpClassName);
                MethodInfo mpMethod = ReflectionHelper.FindMethod(mpType, mpMethodName, Type.EmptyTypes, typeof(ModelProgram));
                try
                {
                    mp = (ModelProgram)mpMethod.Invoke(null, null);
                }
                catch (Exception e)
                {
                    throw new ModelProgramUserException("Invocation of '" + settings.model[0] + "' failed: " + e.ToString());
                }
                for (int i = 1; i < settings.model.Length; i++)
                {
                    ReflectionHelper.SplitFullMethodName(settings.model[i], out mpClassName, out mpMethodName);
                    mpType = ReflectionHelper.FindType(libs, mpClassName);
                    mpMethod = ReflectionHelper.FindMethod(mpType, mpMethodName, Type.EmptyTypes, typeof(ModelProgram));
                    ModelProgram mp2 = null;
                    try
                    {
                        mp2 = (ModelProgram)mpMethod.Invoke(null, null);
                    }
                    catch (Exception e)
                    {
                        throw new ModelProgramUserException("Invocation of '" + settings.model[i] + "' failed: " + e.ToString());
                    }
                    mp = new ProductModelProgram(mp, mp2);
                }
            }
            #endregion

            #region create a model program from given namespace and feature names
            if (settings.mp != null && settings.mp.Length > 0)
            {
                if (libs.Count == 0)
                {
                    throw new ModelProgramUserException("No reference was provided to load models from.");
                }
                //parse the model program name and the feature names for each entry
                foreach (string mps in settings.mp)
                {
                    //the first element is the model program, the remaining ones are 
                    //feature names
                    string[] mpsSplit = mps.Split(new string[] { "[", "]", "," },
                        StringSplitOptions.RemoveEmptyEntries);
                    if (mpsSplit.Length == 0)
                    {
                        throw new ModelProgramUserException("Invalid model program specifier '" + mps + "'.");
                    }
                    string mpName = mpsSplit[0];
                    Assembly mpAssembly = ReflectionHelper.FindAssembly(libs, mpName);
                    Set<string> mpFeatures = new Set<string>(mpsSplit).Remove(mpName);
                    ModelProgram mp1 = new LibraryModelProgram(mpAssembly, mpName, mpFeatures);
                    mp = (mp == null ? mp1 : new ProductModelProgram(mp, mp1));
                }
            }

            #endregion

            #region load the test cases if any
            Sequence<Sequence<CompoundTerm>> testcases = Sequence<Sequence<CompoundTerm>>.EmptySequence;
            if (!String.IsNullOrEmpty(settings.testSuite))
            {
                try
                {
                    System.IO.StreamReader testSuiteReader =
                        new System.IO.StreamReader(settings.testSuite);
                    string testSuiteAsString = testSuiteReader.ReadToEnd();
                    testSuiteReader.Close();
                    CompoundTerm testSuite = CompoundTerm.Parse(testSuiteAsString);
                    foreach (CompoundTerm testCaseTerm in testSuite.Arguments)
                    {
                        Sequence<CompoundTerm> testCase =
                            testCaseTerm.Arguments.Convert<CompoundTerm>(delegate (Term t) { return (CompoundTerm)t; });
                        testcases = testcases.AddLast(testCase);
                    }
                }
                catch (Exception e)
                {
                    throw new ModelProgramUserException("Cannot create test suite: " + e.Message);
                }
            }
            #endregion

            #region load the fsms if any
            Dictionary<string, FSM> fsms = new Dictionary<string, FSM>();
            if (settings.fsm != null && settings.fsm.Length > 0)
            {
                try
                {
                    foreach (string fsmFile in settings.fsm)
                    {
                        System.IO.StreamReader fsmReader = new System.IO.StreamReader(fsmFile);
                        string fsmAsString = fsmReader.ReadToEnd();
                        fsmReader.Close();
                        fsms[fsmFile] = FSM.FromTerm(CompoundTerm.Parse(fsmAsString));
                    }
                }
                catch (Exception e)
                {
                    throw new ModelProgramUserException("Cannot create fsm: " + e.Message);
                }
            }
            #endregion

            if (mp == null && testcases.IsEmpty && fsms.Count == 0)
            {
                throw new ModelProgramUserException("No model, fsm, or test suite was given.");
            }

            if (!testcases.IsEmpty)
            {
                FSM fa = FsmTraversals.GenerateTestSequenceAutomaton(
                    settings.startTestAction, testcases, GetActionSymbols(testcases));
                ModelProgram famp = new FsmModelProgram(fa, settings.testSuite);
                if (mp == null)
                    mp = famp;
                else
                    mp = new ProductModelProgram(mp, famp);
            }

            if (fsms.Count > 0)
            {
                foreach (string fsmName in fsms.Keys)
                {
                    ModelProgram fsmmp = new FsmModelProgram(fsms[fsmName], fsmName);
                    if (mp == null)
                        mp = fsmmp;
                    else
                        mp = new ProductModelProgram(mp, fsmmp);
                }
            }

            //ModelProgramGraphViewForm form = new ModelProgramGraphViewForm("Model Program Viewer");
            //configure the settings of the viewer
            this.View.AcceptingStatesMarked = settings.acceptingStatesMarked;
            this.View.TransitionLabels = settings.transitionLabels;
            this.View.CombineActions = settings.combineActions;
            this.View.Direction = settings.direction;
            this.View.UnsafeStateColor = Color.FromName(settings.unsafeStateColor);
            this.View.HoverColor = Color.FromName(settings.hoverColor);
            this.View.InitialStateColor = Color.FromName(settings.initialStateColor);
            this.View.LoopsVisible = settings.loopsVisible;
            this.View.MaxTransitions = settings.maxTransitions;
            this.View.NodeLabelsVisible = settings.nodeLabelsVisible;
            this.View.SelectionColor = Color.FromName(settings.selectionColor);
            this.View.MergeLabels = settings.mergeLabels;
            this.View.StateShape = settings.stateShape;
            this.View.DeadStateColor = Color.FromName(settings.deadStateColor);
            this.View.InitialTransitions = settings.initialTransitions;
            this.View.LivenessCheckIsOn = settings.livenessCheckIsOn;
            this.View.ExcludeIsomorphicStates = settings.excludeIsomorphicStates;
            this.View.SafetyCheckIsOn = settings.safetyCheckIsOn;
            this.View.DeadstatesVisible = settings.deadStatesVisible;
            this.View.StateViewVisible = settings.stateViewVisible;

            //show the view of the product of all the model programs
            this.View.SetModelProgram(mp);

        }

        private static Set<Symbol> GetActionSymbols(Sequence<Sequence<CompoundTerm>> testcases)
        {
            Set<Symbol> symbs = Set<Symbol>.EmptySet;
            foreach (Sequence<CompoundTerm> testcase in testcases)
                foreach (CompoundTerm action in testcase)
                    symbs = symbs.Add(action.Symbol);
            return symbs;
        }
    }
}