using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using Common.Models;
using NUnit.Framework;

namespace AdventOfCode2022;

public class Day12
{
    [Example(31)]
    [Puzzle(534)]
    public long PartOne(string input)
    {
        var map = HeightMap.Init(input);
        return Traverse(map.Layout, map.Start, map.End);
    }

    [Example(29)]
    [Puzzle(525)]
    public long PartTwo(string input)
    {
        var map = HeightMap.Init(input);

        return map.Potential
            .Select(coord => Traverse(map.Layout, coord, map.End))
            .Min();
    }

    public int Traverse(char[,] layout, Coordinate start, Coordinate end)
    {
        var visited = new Dictionary<Coordinate, Coordinate?>()
        {
            { start, null }
        };
        var queue = new Queue<Coordinate>();
        queue.Enqueue(start);

        while (queue.Any())
        {
            var coordinate = queue.Dequeue();
            if (coordinate == end) break;
            var currentHeight = layout.CoordinateValue(coordinate) == 'S'
                ? 'a'
                : layout.CoordinateValue(coordinate);
            foreach (var neighbour in coordinate.DirectNeighbours())
            {
                if (!layout.IsIn(neighbour)) continue;
                var neighbourHeight = layout.CoordinateValue(neighbour) == 'E'
                    ? 'z'
                    : layout.CoordinateValue(neighbour);

                if (visited.ContainsKey(neighbour) || neighbourHeight - currentHeight >= 2)
                    continue;
                visited[neighbour] = coordinate;
                queue.Enqueue(neighbour);
            }
        }

        // when unable to visit end return high number
        if (!visited.ContainsKey(end)) return 100000;

        var path = new List<Coordinate>();
        var current = end;
        while (current != start)
        {
            if (current is null) throw new NullReferenceException();
            path.Add(current);
            current = visited[current];
        }

        return path.Count;
    }


    private record HeightMap(
        char[,] Layout,
        Coordinate Start,
        Coordinate End,
        IEnumerable<Coordinate> Potential)
    {
        public static HeightMap Init(string input)
        {
            Coordinate? start = null, end = null;
            var potential = new List<Coordinate>();
            var split = input.Split("\n");
            var map = new char[split.Length, split.First().Length];
            foreach (var item in split.Select((value, index) => new { Index = index, Value = value }))
            foreach (var character in item.Value.Select((value, index) => new { Index = index, Value = value }))
            {
                map[item.Index, character.Index] = character.Value;
                switch (character.Value)
                {
                    case 'S':
                        start = new Coordinate(item.Index, character.Index);
                        break;
                    case 'E':
                        end = new Coordinate(item.Index, character.Index);
                        break;
                    case 'a':
                        potential.Add(new Coordinate(item.Index, character.Index));
                        break;
                }
            }

            if (start is null || end is null) throw new NullReferenceException();

            return new HeightMap(map, start, end, potential);
        }
    }
}