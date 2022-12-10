using Common;
using Common.Extensions;
using System.Linq;

namespace AdventOfCode2022;

public class Day10
{
    [Example(13140)]
    [Puzzle(11820)]
    public long PartOne(string input)
    {
        var programLines = input.Split("\n").Select(ProgramLine.Parse);

        
        
        var x = 1;
        var cycle = 0;
        var signal = 0;
        foreach (var programLine in programLines)
        {
            if (programLine.Instruction != "addx")
            {
                cycle++;
                if (cycle == 20 || (cycle - 20) % 40 == 0)
                {
                    signal += cycle * x;
                }
                continue;
            }

            for (var i = 0; i < 2; i++)
            {
                cycle++;
                if (cycle == 20 || (cycle - 20) % 40 == 0)
                {
                    signal += cycle * x;
                }
            }

            if (programLine.Steps != null) x += programLine.Steps.Value;
        }

        return signal;
    }

    [Example(0)]
    [Puzzle(0)]
    public long PartTwo(string input)
    {
        var programLines = input.Split("\n").Select(ProgramLine.Parse);

        var crt = new int[40, 6];

        var x = 1;
        var cycle = 0;
        var currentY = 0;
        foreach (var programLine in programLines)
        {
            
            if (programLine.Instruction != "addx")
            {
                if (x == cycle || x - 1 == cycle || x + 1 == cycle) crt[cycle, currentY]++;
                cycle++;
                if (cycle % 40 == 0)
                {
                    currentY++;
                    cycle -= 40;
                }
            }
            else
            {
                for (var i = 0; i < 2; i++)
                {
                    if (x == cycle || x - 1 == cycle || x + 1 == cycle) crt[cycle, currentY]++;
                    cycle++;
                    if (cycle % 40 == 0)
                    {
                        currentY++;
                        cycle -= 40;
                    }
                }
                
                if (programLine.Steps != null) x += programLine.Steps.Value;
            }
        }
        
        crt.Print();

        return 0;
    }

    private record ProgramLine(string Instruction, int? Steps = null)
    {
        public static ProgramLine Parse(string input)
        {
            var split = input.Split(" ");
            int? steps = split.Length > 1 ? int.Parse(split[1]) : null;
            return new ProgramLine(split[0], steps);
        }
    }
}