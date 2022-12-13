using Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.Json;

namespace AdventOfCode2022;

public class Day13
{
    [Example(13)]
    [Puzzle(4894)]
    public long PartOne(string input)
    {
        var pairs = input.Split("\n\n").Select(Pair.Parse).ToImmutableArray();

        var rightIndices = new List<int>();
        for (var pairIndex = 0; pairIndex < pairs.Length; pairIndex++)
        {
            var pair = pairs[pairIndex];

            var left = pair.Left.Document.RootElement;
            var right = pair.Right.Document.RootElement;

            if (Validate(left, right) == PairResult.Right) rightIndices.Add(pairIndex + 1);
        }

        return rightIndices.Sum();
    }

    [Example(140)]
    [Puzzle(24180)]
    public long PartTwo(string input)
    {
        var additionalPackets = new[]
        {
            Packet.Parse("[[2]]"),
            Packet.Parse("[[6]]")
        };

        var packets = input.Split("\n\n")
            .SelectMany(x => x.Split("\n"))
            .Select(Packet.Parse)
            .Concat(additionalPackets);

        var order = packets
            .ToDictionary(
                packet => packet,
                packet => packets
                    .Where(op => Validate(op.Document.RootElement, packet.Document.RootElement) == PairResult.Right))
            .OrderBy(x => x.Value.Count())
            .ToList();
        
        return additionalPackets
            .Select(packet =>
                order
                    .Select((kv, Index) => new { kv.Key, Index = Index })
                    .First(x => x.Key == packet)
                    .Index + 1)
            .Aggregate(1, (current, index) => current * index);
    }

    private static PairResult Validate(JsonElement left, JsonElement right)
    {
        if (left.ValueKind == JsonValueKind.Array || right.ValueKind == JsonValueKind.Array)
        {
            if (left.ValueKind != JsonValueKind.Array)
            {
                left = JsonDocument.Parse($"[{left.GetInt32()}]").RootElement;
            }

            if (right.ValueKind != JsonValueKind.Array)
            {
                right = JsonDocument.Parse($"[{right.GetInt32()}]").RootElement;
            }

            for (var i = 0; i < left.GetArrayLength(); i++)
            {
                if (i >= right.GetArrayLength()) return PairResult.Wrong;

                var result = Validate(left[i], right[i]);
                if (result != PairResult.Abstained) return result;
            }

            if (right.GetArrayLength() > left.GetArrayLength()) return PairResult.Right;
        }
        else
        {
            if (left.GetInt32() < right.GetInt32()) return PairResult.Right;
            if (left.GetInt32() > right.GetInt32()) return PairResult.Wrong;
        }

        return PairResult.Abstained;
    }

    public enum PairResult
    {
        Right,
        Wrong,
        Abstained
    }

    private class OrderItem
    {
        public Packet? Next { get; set; }
        public Packet? Previous { get; set; }
    }

    private record Pair(Packet Left, Packet Right)
    {
        public static Pair Parse(string input)
        {
            var split = input.Split("\n");
            return new Pair(Packet.Parse(split[0]), Packet.Parse(split[1]));
        }
    }

    private record Packet(JsonDocument Document)
    {
        public static Packet Parse(string input) => new(JsonDocument.Parse(input));
    }
}