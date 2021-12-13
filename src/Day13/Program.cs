// See https://aka.ms/new-console-template for more information

using Day13;

var folder = new Folder();
folder.FoldAlongNextLine();

Console.WriteLine($"There are {folder.CountDots()} dots visible after the first fold");
folder.FinishFold();