// See https://aka.ms/new-console-template for more information

using Day3;

var positioner = new Positioner(
    "/Users/juleswijt/Development/AdventOfCode/2021/advent-of-code-2021/inputs/day3.txt");

Console.WriteLine($"Part 1: The total power consumption is {positioner.CalculatePartOne()}");
Console.WriteLine($"Part 1: The total life support is {positioner.CalculatePartTwo()}");