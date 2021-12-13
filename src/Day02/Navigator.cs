namespace Day2;

public class Navigator
{
    private readonly Instruction[] _instructions;

    private static Instruction[] _examples = new[]
    {
        "forward 5",
        "down 5",
        "forward 8",
        "up 3",
        "down 8",
        "forward 2",
    }.Select(Instruction.Parse).ToArray();

    public Navigator(string filePath)
    {
        _instructions = File.ReadAllLines(filePath)
            .Select(Instruction.Parse)
            .ToArray();
    }

    public int NavigateParteOne()
    {
        var depth = 0;
        var horizontal = 0;
        foreach (var (direction, steps) in _instructions)
        {
            if (direction == "forward") horizontal += steps;
            else if (direction == "down") depth += steps;
            else if (direction == "up") depth -= steps;
        }

        Console.WriteLine($"The depth is {depth} and horizontal is {horizontal}");

        return depth * horizontal;
    }

    public int NavigatePartTwo()
    {
        var depth = 0;
        var horizontal = 0;
        var aim = 0;
        foreach (var (direction, steps) in _instructions)
        {
            if (direction == "forward")
            {
                depth += aim * steps;
                horizontal += steps;
            }
            else if (direction == "down") aim += steps;
            else if (direction == "up") aim -= steps;
        }
        
        Console.WriteLine($"The depth is {depth} and horizontal is {horizontal}");

        return depth * horizontal;
    }
}

public record Instruction(string Direction, int Steps)
{
    public static Instruction Parse(string input)
    {
        var exploded = input.Split(" ");
        return new Instruction(exploded[0], int.Parse(exploded[1]));
    }
}