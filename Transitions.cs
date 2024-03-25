using System.ComponentModel;

namespace TuringMachine
{
    public enum Direction
    {
        Left, Right, Stand
    }
    public record Transition(string Read, string Write, Direction Direction, int NextState) : IEquatable<Transition>
    {
        public override string ToString() => $"[{Read};{Write},{Direction}]";

    }
    public class Transitions
    {
        public Dictionary<int, HashSet<Transition>> TransitionsData { get; } = new();

        public Transitions(Dictionary<int, HashSet<Transition>> transitionsData)
        {
            TransitionsData = transitionsData;
        }
        public Transitions()
        {
        }
        public HashSet<Transition> GetTransitionsForState(int state) => TransitionsData.GetValueOrDefault(state)
            ?? throw new Exception("State not defined for transition");
        public void AddTransition(int state,
                                  string read,
                                  string write,
                                  Direction direction,
                                  int nextState)
        {
            if (TransitionsData.TryGetValue(state, out var transitionForState))
            {
                transitionForState.Add(new(read, write, direction, nextState));
                return;
            }
            TransitionsData.Add(state, new HashSet<Transition>() { new Transition(read, write, direction, nextState) });
        }
        public void AddTransition(int state,
                                  string read,
                                  string write,
                                  string direction,
                                  int nextState)
        {
            var directionASEnum = GetDirectionAsEnum(direction);
            if (TransitionsData.TryGetValue(state, out var transitionForState))
            {
                transitionForState.Add(new(read, write, directionASEnum, nextState));
                return;
            }
            TransitionsData.Add(state, new HashSet<Transition>() { new Transition(read, write, directionASEnum, nextState) });
        }
        private Direction GetDirectionAsEnum(string direction) => direction switch
        {
            "R" => Direction.Right,
            "L" => Direction.Left,
            "S" => Direction.Stand,
            _ => throw new InvalidEnumArgumentException(nameof(direction))
        };
        public override string ToString()
        {
            var content = "\n\t";
            foreach (var item in TransitionsData)
            {
                content += $"Q{item.Key} = {string.Join(" | ", item.Value.Select(x => x.ToString()))}\n\t";
            }
            return content;
        }
    }
}
