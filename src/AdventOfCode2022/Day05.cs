using Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2022;

public class Day05
{
    [Example("CMZ")]
    [Puzzle("WSFTMRHPP")]
    public string PartOne(string input)
    {
        var split = input.Split("\n\n");

        var config = CrateConfig.Parse(split[0]);
        var instructions = split[1].Split("\n").Select(Instruction.Parse);

        foreach (var instruction in instructions)
        {
            for (var i = 0; i < instruction.Number; i++)
            {
                var crate = config.Crates[instruction.Origin].Pop();
                config.Crates[instruction.Location].Push(crate);
            }
        }

        var message = new StringBuilder("");
        foreach (var crateStack in config.Crates)
        {
            message.Append(crateStack.Pop());
        }

        return message.ToString();
    }
    
    [Example("MCD")]
    [Puzzle("GSLCMFBRP")]
    public string PartTwo(string input)
    {
        var split = input.Split("\n\n");

        var config = CrateConfig.Parse(split[0]);
        var instructions = split[1].Split("\n").Select(Instruction.Parse);

        foreach (var instruction in instructions)
        {
            var crates = new List<char>();
            for (var i = 0; i < instruction.Number; i++)
            {
                crates.Add(config.Crates[instruction.Origin].Pop());
            }

            crates.Reverse();
            foreach(var crate in crates)
            {
                config.Crates[instruction.Location].Push(crate);
            }
        }

        var message = new StringBuilder("");
        foreach (var crateStack in config.Crates)
        {
            message.Append(crateStack.Pop());
        }

        return message.ToString();
    }

    private record Instruction(int Number, int Origin, int Location)
    {
        public static Instruction Parse(string input)
        {
            var split = input.Split("move")[1].Split("from");
            var location = split[1].Split("to");
            return new Instruction(
                int.Parse(split[0]),
                int.Parse(location[0]) - 1,
                int.Parse(location[1]) - 1);
        }
    }

    private record CrateConfig(Stack<char>[] Crates)
    {
        public static CrateConfig Parse(string input)
        {
            var rows = input.Split("\n").SkipLast(1).Reverse();

            Stack<char>?[]? crates = null;
            foreach (var row in rows)
            {
                crates ??= new Stack<char>?[row.Length / 4 + 1];
                for (var i = 0; i < row.Length; i += 4)
                {
                    var crate = row[i + 1];
                    if (crate != ' ')
                    {
                        crates[i / 4] ??= new Stack<char>();
                        crates[i / 4]?.Push(crate);
                    }
                }
            }

            return new CrateConfig(crates!);
        }
    }
}