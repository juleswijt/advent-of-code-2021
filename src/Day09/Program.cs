// See https://aka.ms/new-console-template for more information

using Day9;

var reader = new Reader(
    "/Users/juleswijt/Development/AdventOfCode/2021/advent-of-code-2021/inputs/day9.txt");

Console.WriteLine($"Sum of the risk levels is {reader.AssesRiskLevel().Item1}");
Console.WriteLine($"Sum of the basins is {reader.AssesRiskLevel().Item2}");