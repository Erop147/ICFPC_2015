using System;
using System.Linq;

namespace ICFPC2015.GameLogic.Logic
{
    public struct Game
    {
        public int ProblemId { get; set; }
        public long Seed { get; set; }

        public readonly Unit[] UnitsSequence;
        public int CurrentUnitNumber { get; set; }
        public Board Board { get; private set; }
        public GameUnit Current { get; private set; }
        public GameState State { get; private set; }

        public string LastSymbols { get; set; }
        public int LastUnitLinesCleared { get; private set; }
        public int Score { get; private set; }
        public int WordsMask { get; set; }

        public Game(Board board, GameUnit current, Unit[] unitsSequence, int currentUnitNumber, int lastUnitLinesCleared, int score, int problemId, long seed, string lastSymbols, int wordsMask)
            : this()
        {
            Board = board;
            Current = current;
            UnitsSequence = unitsSequence;
            CurrentUnitNumber = currentUnitNumber;
            LastUnitLinesCleared = lastUnitLinesCleared;
            Score = score;
            ProblemId = problemId;
            Seed = seed;
            LastSymbols = lastSymbols;
            WordsMask = wordsMask;
        }

        public Game TrySpawnNew()
        {
            if (Current != null)
            {
                throw new Exception("Current unit is not null");
            }
            if (CurrentUnitNumber == UnitsSequence.Length)
            {
                return GameOver();
            }
            var unit = UnitsSequence[CurrentUnitNumber];
            var pivot = GetPivotLocation(unit);
            var gameUnit = new GameUnit(unit, new UnitPosition(pivot, 0));

            if (!IsValid(gameUnit))
            {
                return GameOver();
            }

            return SpawnedNew(gameUnit);
        }

        public Point GetPivotLocation(Unit unit)
        {
            var row = - unit.Points.Min(x => x.Row);
            var minCol = unit.Points.Min(x => x.Col);
            var maxCol = unit.Points.Max(x => x.Col);
            var colShift = (Board.Width - (maxCol - minCol + 1)) / 2;
            var col = colShift - minCol;

            return new Point(col, row) - unit.PivotPoint;
        }

        public bool IsValid(GameUnit gameUnit)
        {
            return Board.IsValid(gameUnit);
        }

        public Game TryMakeStep(char ch)
        {
            var command = CommandConverter.Convert(ch);
            if (Current == null)
            {
                throw new Exception("Current game unit is null");
            }

            if (!IsValid(Current))
            {
                throw new Exception("Current game unit is not valid, new step is impossible");
            }

            var newGameUnit = Current.MakeStep(command);
            var lastSymbols = LastSymbols.Insert(LastSymbols.Length, ch.ToString());
            int wordsMask;
            var wordsScore = CalculateWordsScore(lastSymbols, out wordsMask);

            if (!IsValid(newGameUnit))
            {
                var lockedCells = Current.GetAbsolutePoints();
                var updateResult = Board.Place(lockedCells).Update();
                var additionalScore = CalculateScore(LastUnitLinesCleared, updateResult.RowsCleaned, Current.Unit.Points.Length);
                
                if (lastSymbols.Length == 51)
                {
                    lastSymbols = lastSymbols.Substring(1);
                }

                var gameWithNewUnit = new Game(updateResult.NewBoard, null, UnitsSequence, CurrentUnitNumber + 1, updateResult.RowsCleaned, Score + additionalScore + wordsScore, ProblemId, Seed, lastSymbols, wordsScore);
                return gameWithNewUnit.TrySpawnNew();
            }

            return MoveCurrent(newGameUnit, lastSymbols, Score + wordsScore, wordsMask);
        }
        
        private static int CalculateScore(int lastUnitRowsCleaned, int rowsCleaned, int size)
        {
            var points = size + 100 * (1 + rowsCleaned) * rowsCleaned / 2;
            var lineBonus = lastUnitRowsCleaned > 1 ? (lastUnitRowsCleaned - 1) * points / 10 : 0;
            return points + lineBonus;
        }

        private int CalculateWordsScore(string lastSymbols, out int wordsMask)
        {
            wordsMask = WordsMask;
            var ret = 0;
            for (int i = 0; i < MagicWordsStore.Words.Length; i ++)
            {
                var word = MagicWordsStore.Words[i];
                if (lastSymbols.Length >= word.Length && lastSymbols.Substring(lastSymbols.Length - word.Length) == word)
                {
                    if ((wordsMask & (1 << i)) == 0)
                    {
                        ret += 300;
                        wordsMask |= (1 << i);
                    }
                    ret += 2 * word.Length;
                }
            }
            return ret;
        }

        private Game SpawnedNew(GameUnit gameUnit)
        {
            var clone = this;
            clone.Current = gameUnit;
            clone.State = GameState.NewIsSpawned;
            return clone;
        }

        private Game MoveCurrent(GameUnit gameUnit, string lastSymbols, int newScore, int wordsMask)
        {
            var clone = this;
            clone.Current = gameUnit;
            clone.State = GameState.Ok;
            clone.LastSymbols = lastSymbols;
            clone.Score = newScore;
            clone.WordsMask = wordsMask;
            return clone;
        }

        private Game GameOver()
        {
            var clone = this;
            clone.State = GameState.GameOver;
            return clone;
        }
    }
}