// See https://aka.ms/new-console-template for more information

using Day1;

var calculator = new DepthCalculator(
    "/Users/juleswijt/Development/AdventOfCode/2021/advent-of-code-2021/inputs/day1.txt");

Console.WriteLine($"Part 1: There are {calculator.MeasurePartOne()} measurements larger");
Console.WriteLine($"Part 2: There are {calculator.MeasurePartTwo()} measurements larger");