using ICFPC2015.GameLogic.Logic;

namespace ICFPC2015.Player.Implementation
{
    public struct State
    {
        public State(GameUnit unit, Command lastCommand, bool isLocked, bool canGoHorisontal, bool? isLastCommandWord) : this()
        {
            IsLocked = isLocked;
            Unit = unit;
            LastCommand = lastCommand;
            CanGoHorisontal = canGoHorisontal;
            IsLastCommandWord = isLastCommandWord;
        }

        public GameUnit Unit { get; set; }
        public Command LastCommand { get; set; }
        public bool? IsLastCommandWord { get; set; }
        public bool IsLocked { get; set; }
        public bool CanGoHorisontal { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Unit != null ? Unit.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) LastCommand;
                hashCode = (hashCode * 397) ^ IsLocked.GetHashCode();
                hashCode = (hashCode * 397) ^ IsLastCommandWord.GetHashCode();
                hashCode = (hashCode * 397) ^ CanGoHorisontal.GetHashCode();
                return hashCode;
            }
        }

        public bool Equals(State other)
        {
            return Unit.Equals(other.Unit) && LastCommand == other.LastCommand && IsLocked == other.IsLocked && IsLastCommandWord == other.IsLastCommandWord && CanGoHorisontal == other.CanGoHorisontal;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is State && Equals((State) obj);
        }
    }
}