// See https://aka.ms/new-console-template for more information

using Day5;

var overlapper = new Overlapper(
    "/Users/juleswijt/Development/AdventOfCode/2021/advent-of-code-2021/inputs/day5.txt");

Console.WriteLine(overlapper.CountOverlapped());
Console.WriteLine(overlapper.CountOverlapped(true));