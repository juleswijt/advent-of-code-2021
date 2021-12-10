namespace Common;

public static class InputFile
{
    private const string Dir = "/Users/juleswijt/Development/AdventOfCode/2021/advent-of-code-2021/inputs/{0}.txt";

    public static string[] Read(int day, bool example)
        => File.ReadAllLines(string.Format(Dir, example ? $"day{day}_example" : $"day{day}"));
}