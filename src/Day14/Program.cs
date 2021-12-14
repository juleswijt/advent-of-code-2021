// See https://aka.ms/new-console-template for more information

using System.Threading.Channels;
using Day14;

var templater = new Templater();

templater.ApplySteps(40);

Console.WriteLine($"Subtracting the least common from the most common results in {templater.Calculate()}");