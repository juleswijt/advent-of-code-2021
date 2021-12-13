namespace Day5;

public class Overlapper
{
    private readonly Instruction[] _instructions;
    private readonly int _matrixWidth;
    private readonly int _matrixHeight;

    public Overlapper(string filePath)
    {
        _instructions = File.ReadAllLines(filePath)
            .Select(Instruction.Parse)
            .ToArray();

        var coordinates = _instructions
            .Select(x => x.Start)
            .Concat(_instructions.Select(x => x.End));
        _matrixWidth = coordinates.Max(Coordinate => Coordinate.x) + 1;
        _matrixHeight = coordinates.Max(Coordinate => Coordinate.y) + 1;
    }

    public int CountOverlapped(bool withDiagonal = false)
    {
        var matrix = new int[_matrixWidth, _matrixHeight];
        foreach (var (start, end) in _instructions)
        {
            var xDifference = end.x - start.x;
            var yDifference = end.y - start.y;
            if (!withDiagonal && xDifference != 0 && yDifference != 0) continue;

            var currentX = start.x;
            var currentY = start.y;
            var xSteps = 0;
            var ySteps = 0;
            while (xSteps <= Math.Abs(xDifference) && ySteps <= Math.Abs(yDifference))
            {
                matrix[currentX, currentY]++;

                var stepX = xDifference == 0 ? 0 : xDifference / Math.Abs(xDifference);
                var stepY = yDifference == 0 ? 0 : yDifference / Math.Abs(yDifference);

                currentX += stepX;
                currentY += stepY;
                xSteps += Math.Abs(stepX);
                ySteps += Math.Abs(stepY);
            }
        }

        var overlaps = 0;
        for (var row = 0; row < matrix.GetLength(0); row++)
        {
            for (var column = 0; column < matrix.GetLength(1); column++)
            {
                if (matrix[row, column] > 1) overlaps++;
            }
        }

        return overlaps;
    }
}

public record Instruction(Coordinate Start, Coordinate End)
{
    public static Instruction Parse(string raw)
    {
        var coordinates = raw.Split(" -> ").Select(Coordinate.Parse).ToArray();
        return new Instruction(coordinates[0], coordinates[1]);
    }
}

public record Coordinate(int x, int y)
{
    public static Coordinate Parse(string raw)
    {
        var split = raw.Split(",");
        return new Coordinate(int.Parse(split[0]), int.Parse(split[1]));
    }
}