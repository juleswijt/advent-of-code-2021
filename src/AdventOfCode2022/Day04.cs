using Common;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022;

public class Day04
{
    [Example(2)]
    [Puzzle(538)]
    public long PartOne(string input)
    {
        var pairs = input.Split("\n").Select(CleaningPair.Parse);
        return pairs.Count(x => x.OverlapsCompletely());
    }

    [Example(4)]
    [Puzzle(792)]
    public long PartTwo(string input)
    {
        var pairs = input.Split("\n").Select(CleaningPair.Parse);
        return pairs.Count(x => x.OverlapsPartially());
    }

    private record CleaningPair(IEnumerable<int> Elf1, IEnumerable<int> Elf2)
    {
        public bool OverlapsCompletely() => !Elf1.Except(Elf2).Any() || !Elf2.Except(Elf1).Any();

        public bool OverlapsPartially() => Elf1.Intersect(Elf2).Any() || Elf2.Intersect(Elf1).Any();

        public static CleaningPair Parse(string input)
        {
            var split = input.Split(",").Select(ParseRange);
            return new CleaningPair(split.First(), split.Last());
        }

        private static IEnumerable<int> ParseRange(string input)
        {
            var split = input.Split("-");
            return Enumerable.Range(int.Parse(split[0]), int.Parse(split[1]) - int.Parse(split[0]) + 1);
        }
    }
}