// See https://aka.ms/new-console-template for more information

using Day10;

var parser = new Parser(false);

Console.WriteLine($"The total syntax error score is: {parser.TotalErrorScore()}");
Console.WriteLine($"The middle score is: {parser.MiddleScore()}");