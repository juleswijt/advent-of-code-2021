// See https://aka.ms/new-console-template for more information

using Day7;

var simulator = new Positioner(
    "/Users/juleswijt/Development/AdventOfCode/2021/advent-of-code-2021/inputs/day7.txt");

Console.WriteLine(simulator.CalculateMinimumFuelUsage());
Console.WriteLine(simulator.CalculateMinimumEngineeredFuelUsage());