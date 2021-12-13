// See https://aka.ms/new-console-template for more information

using Day2;

var calculator = new Navigator(
    "/Users/juleswijt/Development/AdventOfCode/2021/advent-of-code-2021/inputs/day2.txt");

Console.WriteLine($"Part 1: The total position is {calculator.NavigateParteOne()}");
Console.WriteLine($"Part 2: The total position is {calculator.NavigatePartTwo()}");

