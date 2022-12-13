using Common;
using System.Linq;

namespace AdventOfCode2022;

public class Day06
{
    [Example(11)]
    [Puzzle(1566)]
    public int PartOne(string input)
    {
        for (var index = 4; index < input.Length; index++)
        {
            var subString = input[(index-4)..index];
            if (subString.All(@char => subString.Count(x => @char == x) < 2))
            {
                return index;
            }
        }

        return 0;
    }
    
    [Example(26)]
    [Puzzle(2265)]
    public int PartTwo(string input)
    {
        for (var index = 14; index < input.Length; index++)
        {
            var subString = input[(index-14)..index];
            if (subString.All(@char => subString.Count(x => @char == x) < 2))
            {
                return index;
            }
        }

        return 0;
    }
}