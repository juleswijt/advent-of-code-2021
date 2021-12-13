// See https://aka.ms/new-console-template for more information

using Day12;

var navigator = new Navigator();

Console.WriteLine($"There are {navigator.CalculateSimpleRoutes()} valid simple routes");
Console.WriteLine($"There are {navigator.CalculateComplexRoutes()} valid complex routes");