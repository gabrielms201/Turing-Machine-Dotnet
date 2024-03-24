using System.Runtime.ConstrainedExecution;
using static TuringMachine.Constants;

namespace TuringMachine
{
    class Program
    {
        public static void Main(string[] args)
        {
            Transitions transitions = new();
            // Q0
            transitions.AddTransition(0, "0", "0", Direction.Right, 1);
            // Q1
            transitions.AddTransition(1, "1", "1", Direction.Right, 2);
            // Q2
            transitions.AddTransition(2, Empty, Empty, Direction.Right, 3);
            transitions.AddTransition(2, "1", "1", Direction.Stand, 0);
            transitions.AddTransition(2, "0", "0", Direction.Stand, 0);
            // Q3
            // Q3 is final
            TuringMachine tm = new(
                    states: new() { 0, 1, 2, 3 },
                    inputAlphabet: new() { "0", "1" },
                    transitions: transitions,
                    startState: 0,
                    acceptStates: new() { 3 }
                );

            // Act
            var input = "01010101010101";
            var decides = tm.Decides(input);
            Console.WriteLine("Configuration:\n" + tm);
            Console.WriteLine($"{nameof(tm)}");
            if (decides)
            {
                Console.WriteLine($"Decises {nameof(input)}");
                return;
            }
            Console.WriteLine($"Do Not Decises {nameof(input)}");
        }
    }
}