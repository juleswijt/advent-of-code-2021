using Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022;

public class Day09
{
    [Example(88)]
    [Puzzle(6642)]
    public long PartOne(string input)
    {
        var instructions = input.Split("\n").Select(Instruction.Parse);
        return RopeHelper.CalculateSteps(instructions, 2);
    }

    [Example(36)]
    [Puzzle(2765)]
    public long PartTwo(string input)
    {
        var instructions = input.Split("\n").Select(Instruction.Parse);
        return RopeHelper.CalculateSteps(instructions, 10);
    }

    private static class RopeHelper
    {
        public static int CalculateSteps(IEnumerable<Instruction> instructions, int ropeLenght)
        {
            var rope = Create(ropeLenght);
            var traveled = new List<Coordinate> { rope.Last() };
            foreach (var instruction in instructions)
            {
                for (var steps = 0; steps < instruction.Steps; steps++)
                {
                    Coordinate? previous = null;
                    var newRope = new List<Coordinate>();
                    using var iterator = rope.GetEnumerator();
                    while (iterator.MoveNext())
                    {
                        var current = iterator.Current;
                        if (previous == null)
                        {
                            previous = instruction.Direction switch
                            {
                                "U" => current with { Y = current.Y + 1 },
                                "D" => current with { Y = current.Y - 1 },
                                "L" => current with { X = current.X - 1 },
                                "R" => current with { X = current.X + 1 },
                                _ => current
                            };
                        }
                        else if (!previous.Touches(current))
                        {
                            Coordinate coordinate;
                            if (current.X == previous.X)
                            {
                                coordinate = (current with { Y = current.Y + 1 }).Touches(previous)
                                    ? current with { Y = current.Y + 1 }
                                    : current with { Y = current.Y - 1 };
                            }
                            else if (current.Y == previous.Y)
                            {
                                coordinate = (current with { X = current.X + 1 }).Touches(previous)
                                    ? current with { X = current.X + 1 }
                                    : current with { X = current.X - 1 };
                            }
                            else
                            {
                                coordinate = current
                                    .Diagonals()
                                    .First(diagonal => diagonal.Touches(previous));
                            }


                            previous = coordinate;
                        }
                        else
                        {
                            previous = current;
                        }

                        newRope.Add(previous);
                    }

                    if (!rope.Last().Equals(newRope.Last())
                        && !traveled.Contains(newRope.Last()))
                    {
                        traveled.Add(newRope.Last());
                    }


                    rope = newRope;
                }
            }

            return traveled.Count;
        }

        private static IEnumerable<Coordinate> Create(int knots)
        {
            for (var knot = 0; knot < knots; knot++)
            {
                yield return new Coordinate(0, 0);
            }
        }
    }

    private record Instruction(string Direction, int Steps)
    {
        public static Instruction Parse(string input)
        {
            var split = input.Split(" ");
            return new Instruction(split[0], int.Parse(split[1]));
        }
    }

    private record Coordinate(int X, int Y)
    {
        public bool Touches(Coordinate coordinate)
            => Equals(coordinate) || Neighbours().Any(x => x.Equals(coordinate));


        public IEnumerable<Coordinate> Diagonals() => new[]
        {
            new Coordinate(X - 1, Y - 1),
            new Coordinate(X + 1, Y - 1),
            new Coordinate(X - 1, Y + 1),
            new Coordinate(X + 1, Y + 1),
        };
        
        public IEnumerable<Coordinate> ExactNeighbours() => new[]
        {
            new Coordinate(X, Y - 1),
            new Coordinate(X - 1, Y),
            new Coordinate(X + 1, Y),
            new Coordinate(X, Y + 1),
        };

        private IEnumerable<Coordinate> Neighbours() => Diagonals().Concat(ExactNeighbours());

        public virtual bool Equals(Coordinate? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode() => HashCode.Combine(X, Y);
    }
}