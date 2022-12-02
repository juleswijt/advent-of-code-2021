using Common;
using NUnit.Framework;
using System;
using System.Linq;

namespace AdventOfCode2022;

[TestFixture]
public class Day02
{
    [Example(15)]
    [Puzzle(12586)]
    public long PartOne(string input)
    {
        return input.Split("\n")
            .Select(MatchUpPartOne.Parse)
            .Sum(x => x.Score());
    }

    [Example(12)]
    [Puzzle(13193)]
    public long PartTwo(string input)
        => input.Split("\n")
            .Select(MatchUpPartTwo.Parse)
            .Sum(x => x.Score());

    private record MatchUpPartOne(Action opponent, Action self)
    {
        public int Score()
        {
            var score = self.Score();
            if (self.Wins(opponent)) return score + 6;
            if (self.GetType() == opponent.GetType()) return score + 3;
            return score;
        }

        public static MatchUpPartOne Parse(string input)
        {
            var split = input.Split(" ").Select(Action.Parse);
            return new MatchUpPartOne(split.First(), split.Last());
        }
    }

    private record MatchUpPartTwo(Action opponent, ExpectedResult self)
    {
        public int Score()
            => self.Score() + self.DesiredAction(opponent).Score();

        public static MatchUpPartTwo Parse(string input)
        {
            var split = input.Split(" ");
            return new MatchUpPartTwo(Action.Parse(split[0]), ExpectedResult.Parse(split[1]));
        }
    }

    private abstract record ExpectedResult
    {
        public abstract int Score();
        public abstract Action DesiredAction(Action action);

        public static ExpectedResult Parse(string result)
            => result switch
            {
                "X" => new Lose(),
                "Y" => new Draw(),
                "Z" => new Win(),
                _ => throw new ArgumentOutOfRangeException(nameof(result), result, null)
            };
    }

    private record Lose : ExpectedResult
    {
        public override int Score() => 0;

        public override Action DesiredAction(Action opponent)
            => opponent switch
            {
                Rock => new Scissors(),
                Paper => new Rock(),
                Scissors => new Paper(),
                _ => throw new ArgumentOutOfRangeException(nameof(opponent), opponent, null)
            };
    }

    private record Draw : ExpectedResult
    {
        public override int Score() => 3;
        public override Action DesiredAction(Action opponent) => opponent;
    }

    private record Win : ExpectedResult
    {
        public override int Score() => 6;

        public override Action DesiredAction(Action opponent)
            => opponent switch
            {
                Rock => new Paper(),
                Paper => new Scissors(),
                Scissors => new Rock(),
                _ => throw new ArgumentOutOfRangeException(nameof(opponent), opponent, null)
            };
    }

    private abstract class Action
    {
        public abstract int Score();
        public abstract bool Wins(Action action);

        public static Action Parse(string input)
            => input switch
            {
                "A" or "X" => new Rock(),
                "B" or "Y" => new Paper(),
                "C" or "Z" => new Scissors(),
                _ => throw new ArgumentOutOfRangeException(nameof(input), input, null)
            };
    }

    private class Rock : Action
    {
        public override int Score() => 1;
        public override bool Wins(Action action) => action is Scissors;
    }

    private class Paper : Action
    {
        public override int Score() => 2;
        public override bool Wins(Action action) => action is Rock;
    }

    private class Scissors : Action
    {
        public override int Score() => 3;
        public override bool Wins(Action action) => action is Paper;
    }
}