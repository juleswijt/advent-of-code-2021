// See https://aka.ms/new-console-template for more information

using Day8;

var reader = new DisplayReader(
    "/Users/juleswijt/Development/AdventOfCode/2021/advent-of-code-2021/inputs/day8.txt");

Console.WriteLine($"Counted {reader.CountEasyDigits()} easy digits");
Console.WriteLine($"Counted {reader.CountOutput()} output");
