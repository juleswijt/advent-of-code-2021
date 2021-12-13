// See https://aka.ms/new-console-template for more information

using Day4;

var caller = new Caller(
    "/Users/juleswijt/Development/AdventOfCode/2021/advent-of-code-2021/inputs/day4.txt");

Console.WriteLine(caller.CalculateWinningScore());
Console.WriteLine(caller.CalculateLosingScore());