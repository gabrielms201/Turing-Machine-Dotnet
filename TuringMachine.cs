namespace TuringMachine
{
    public class TuringMachine
    {
        // 7 Tuple
        public HashSet<int> States { get; }         // Q
        public List<string> InputAlphabet { get; }  // Sigma
        public List<string> TapeAlphabet { get; } // ??
        public Transitions Transitions { get; }
        public int StartState { get; }              // Q0
        public HashSet<int> AcceptStates { get; }   // QF
        public Tape Tape { get; private set; }
        private int CurrentState;

        public TuringMachine(HashSet<int> states,
                             List<string> inputAlphabet,
                             Transitions transitions,
                             int startState,
                             HashSet<int> acceptStates)
        {
            States = states;
            InputAlphabet = inputAlphabet;
            TapeAlphabet = inputAlphabet.Append(Constants.Empty).ToList();
            Transitions = transitions;
            StartState = startState;
            AcceptStates = acceptStates;
            Tape = Tape.Empty;
            CurrentState = StartState;
        }
        public bool Decides(string input)
        {
            var (_, accept) = Execute(input);
            return accept;
        }
        private (string output, bool accepts) Execute(string input)
        {
            Tape = new(input: input);

            while (!AcceptStates.Contains(CurrentState))
            {
                var transitions = Transitions.GetTransitionsForState(CurrentState);
                var currentSymbol = Tape.GetCurrentSymbol();

                var transition =
                    transitions.SingleOrDefault(x => x.Write.Equals(currentSymbol));

                if (transition is null)
                    return Reject();
                Tape.Write(transition.Write);
                Tape.Move(transition.Direction);
                CurrentState = transition.NextState;
            }

            return Accept();
        }
        private (string Output, bool) Reject() => (Tape.Output, false);
        private (string Output, bool) Accept() => (Tape.Output, true);
        public override string ToString()
            =>
                $"States : {string.Join(",", States)}" + Environment.NewLine +
                $"InputAlphabet : {string.Join(",", InputAlphabet)}" + Environment.NewLine +
                $"TapeAlphabet :  {string.Join(",", TapeAlphabet)}" + Environment.NewLine +
                $"Transitions : {Transitions}" + Environment.NewLine +
                $"StartState : {StartState}" + Environment.NewLine +
                $"AcceptStates : {string.Join(",", AcceptStates)}" + Environment.NewLine +
                $"Tape : {Tape}" + Environment.NewLine;
    }
}
