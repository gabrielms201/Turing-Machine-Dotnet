using System.Text;

namespace TuringMachine
{
    public class Tape
    {
        public static Tape Empty => new(Constants.Empty);
        public string Input { get; init; }
        public string Output => OutputBuilder.ToString();
        private readonly StringBuilder OutputBuilder;
        public int Position { get; private set; } = 0;

        public Tape(string input)
        {
            Input = input;
            OutputBuilder = new(Input);
        }

        public bool CurrentPositionIsEmpty()
            => Position == -1 || Position == OutputBuilder.Length;
        public string GetCurrentSymbol() => CurrentPositionIsEmpty() ? Constants.Empty : OutputBuilder[Position].ToString();
        public void Write(string content)
        {
            if (CurrentPositionIsEmpty())
            {
                if (content.Equals(Constants.Empty))
                    return;
                Console.WriteLine("Trying to write on empty position. For know this simulator can't reproduce that");
                return;
            }
            OutputBuilder.Remove(Position, 1);
            OutputBuilder.Insert(Position, content);
        }
        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    Position--;
                    break;
                case Direction.Right:
                    Position++;
                    break;
                case Direction.Stand:
                    break;
            }
        }
        public override string ToString() =>
            $"\n\tTape Input: {Input}" + Environment.NewLine +
            $"\tTape Output: {Output}" + Environment.NewLine;
    }
}
