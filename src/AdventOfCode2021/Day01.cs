using Common;
using NUnit.Framework;
using System.Linq;

namespace AdventOfCode2021;

[TestFixture]
public class Day01
{
    [Example(7)]
    [Puzzle(1292)]
    public int PartOne(string input)
    {
        var values = input.Split("\n")
            .Select(int.Parse)
            .ToArray();
        
        var increased = 0;
        var previous = values[0];
        for (var i = 1; i < values.Length; i++)
        {
            if (values[i] > previous) increased++;
            previous = values[i];
        }

        return increased;
    }

    [Example(5)]
    [Puzzle(1262)]
    public int PartTwo(string input)
    {
        var values = input.Split("\n")
            .Select(int.Parse)
            .ToArray();
        
        var increased = 0;
        var previous2 = values[0] + values[1] + values[2];
        for (var i = 3; i < values.Length; i++)
        {
            var value = values[i] + values[i - 1] + values[i - 2];
            if (value > previous2) increased++;
            previous2 = value;
        }
    
        return increased;
    }
}