using Common;
using NUnit.Framework;
using System.Linq;

namespace AdventOfCode2022;

[TestFixture]
public class Day01
{
    [Example(24000)]
    [Puzzle(70613)]
    public long PartOne(string input)
        => input.Split("\n\n")
            .Select(elf => elf.TotalCalories())
            .Max();

    [Example(45000)]
    [Puzzle(205805)]
    public long PartTwo(string input)
        => input.Split("\n\n")
            .Select(elf => elf.TotalCalories())
            .OrderByDescending(x => x)
            .Take(3)
            .Sum();
}

internal static class Extensions
{
    public static int TotalCalories(this string calories)
        => calories.Split("\n").Select(int.Parse).Sum();
}