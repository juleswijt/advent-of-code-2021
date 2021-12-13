using Common;

namespace Day13;

public class Folder
{
    private readonly List<Instruction> _instructions = new();
    private int[,] _paper;

    public Folder()
    {
        var dots = new List<Coordinate>();
        foreach (var input in InputFile.Read(13, false))
        {
            if (string.IsNullOrEmpty(input)) continue;
            if (input.StartsWith("fold along")) _instructions.Add(Instruction.Parse(input));
            else dots.Add(Coordinate.Parse(input));
        }

        var highestX = dots.Max(x => x.X) + 1;
        var highestY = dots.Max(x => x.Y) + 1;

        _paper = new int[highestX, highestY];
        foreach (var (x, y) in dots) _paper[x, y]++;
    }

    public void FoldAlongNextLine()
    {
        var instruction = _instructions.First();
        _instructions.Remove(instruction);
        if (instruction.Direction == 'y') FoldHorizontal(instruction.Value);
        else FoldVertical(instruction.Value);
    }

    public void FinishFold()
    {
        foreach (var (direction, value) in _instructions)
        {
            if (direction == 'y') FoldHorizontal(value);
            else FoldVertical(value);
        }
    }

    public int CountDots()
    {
        var dots = 0;
        for (var x = 0; x < _paper.GetLength(0); x++)
        for (var y = 0; y < _paper.GetLength(1); y++)
            if (_paper[x, y] > 0)
                dots++;

        return dots;
    }

    private void FoldHorizontal(int divider)
    {
        var dots = new List<Coordinate>();
        if (_paper.GetLength(0) / 2 >= divider) // fold bottom to top 
        {
            for (var x = divider + 1; x < _paper.GetLength(0); x++)
            for (var y = 0; y < _paper.GetLength(1); y++)
                if (_paper[x, y] > 0)
                    dots.Add(new Coordinate(divider - x + divider, y));

            var newPaper = new int[divider, _paper.GetLength(1)];
            for (var x = 0; x < divider; x++)
            for (var y = 0; y < _paper.GetLength(1); y++)
                newPaper[x, y] = _paper[x, y];

            foreach (var (x, y) in dots) newPaper[x, y]++;
            _paper = newPaper;
        }

        PrintPaper();
    }

    private void FoldVertical(int divider)
    {
        var dots = new List<Coordinate>();
        if (_paper.GetLength(1) / 2 >= divider) // fold right to left
        {
            for (var x = 0; x < _paper.GetLength(0); x++)
            for (var y = divider + 1; y < _paper.GetLength(1); y++)
                if (_paper[x, y] > 0)
                    dots.Add(new Coordinate(x, divider - y + divider));

            var newPaper = new int[_paper.GetLength(0), divider];
            for (var x = 0; x < _paper.GetLength(0); x++)
            for (var y = 0; y < divider; y++)
                newPaper[x, y] = _paper[x, y];

            foreach (var (x, y) in dots) newPaper[x, y]++;
            _paper = newPaper;
        }

        PrintPaper();
    }

    private void PrintPaper()
    {
        for (var x = 0; x < _paper.GetLength(0); x++)
        {
            for (var y = 0; y < _paper.GetLength(1); y++)
            {
                var write = _paper[x, y] > 0 ? $"\x1b[31m{_paper[x, y]}\x1b[0m" : _paper[x, y].ToString();
                Console.Write(write);
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
}

public record Coordinate(int X, int Y)
{
    public static Coordinate Parse(string raw)
    {
        var split = raw.Split(",").Select(int.Parse).ToArray();
        return new Coordinate(split[1], split[0]);
    }
}

public record Instruction(char Direction, int Value)
{
    public static Instruction Parse(string raw)
    {
        var split = raw.Split("=").ToArray();
        return new Instruction(split[0].Last(), int.Parse(split[1]));
    }
}