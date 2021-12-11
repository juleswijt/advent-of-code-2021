// See https://aka.ms/new-console-template for more information

using Day11;

var configurator = new FlashConfigurator(false);

Console.WriteLine($"There are a total of {configurator.CalculateFlashes()} after 100 steps");
Console.WriteLine($"The first step at which all octopuses flash is {configurator.CalculateSimultaneousFlash()}");