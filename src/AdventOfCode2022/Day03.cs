using Common;
using System.Linq;

namespace AdventOfCode2022;

public class Day03
{
    [Example(157)]
    [Puzzle(7785)]
    public long PartOne(string input)
    {
        var sacks = input.Split("\n");

        var total = 0;
        foreach (var sack in sacks)
        {
            var comp1 = sack.Take(sack.Length / 2).ToArray();
            var comp2 = sack.Skip(sack.Length / 2).ToArray();

            total += ToPriority(comp1
                .Intersect(comp2)
                .First());
        }

        return total;
    }

    [Example(70)]
    [Puzzle(2633)]
    public long PartTwo(string input)
    {
        var sacks = input.Split("\n");

        var total = 0;
        for (var i = 0; i < sacks.Length; i += 3)
        {
            var supplies = sacks.Skip(i).Take(3).ToArray();

            total += ToPriority(supplies[0]
                .Intersect(supplies[1])
                .Intersect(supplies[2])
                .First());
        }

        return total;
    }

    private int ToPriority(int supply) => supply > 96 ? supply - 96 : supply - 38;
}