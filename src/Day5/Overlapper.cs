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

        var coordinates = _instructions.Select(x => x.Start)
            .Concat(_instructions.Select(x => x.End));
        _matrixWidth = coordinates.Max(Coordinate => Coordinate.x) + 1;
        _matrixHeight = coordinates.Max(Coordinate => Coordinate.y) + 1;
    }

    public int CountOverlapped(bool withDiagonal = false)
    {
        var matrix = new int[_matrixWidth, _matrixHeight];
        var total = _instructions.Length;
        var current = 0;
        foreach (var (start, end) in _instructions)
        {
            Console.WriteLine($"Current instruction {current} of {total}");
            if (start.x == end.x)
            {
                var difference = Math.Abs(start.y - end.y);
                var startIndex = start.y < end.y ? start.y : end.y;
                for (var i = startIndex; i <= startIndex + difference; i++)
                {
                    matrix[start.x, i]++;
                }

                current++;
                continue;
            }

            if (start.y == end.y)
            {
                var difference = Math.Abs(start.x - end.x);
                var startIndex = start.x < end.x ? start.x : end.x;
                for (var i = startIndex; i <= startIndex + difference; i++)
                {
                    matrix[i, start.y]++;
                }

                current++;
                continue;
            }

            if (withDiagonal)
            {
                var xDifference = Math.Abs(start.x - end.x);
                var yDifference = Math.Abs(start.y - end.y);
                // Means diagonally increasing
                if (start.x > end.x && start.y > end.y || end.x > start.x && end.y > start.y)
                {
                    var startCoordinate = start.x < end.x ? start : end;
                    var traversed = new List<Coordinate>();
                    for (var i = startCoordinate.x; i <= startCoordinate.x + xDifference; i++)
                    {
                        for (var j = startCoordinate.y; j <= startCoordinate.y + yDifference; j++)
                        {
                            if (NotTraversedRowOrColumn(traversed, i, j))
                            {
                                matrix[i, j]++;
                                traversed.Add(new Coordinate(i, j));
                            }
                        }
                    }
                }
                else
                {
                    var startCoordinate = start.x > end.x ? start : end;
                    var traversed = new List<Coordinate>();
                    for (var i = startCoordinate.x; i >= 0; i--)
                    {
                        for (var j = startCoordinate.y; j <= startCoordinate.y + yDifference; j++)
                        {
                            if (NotTraversedRowOrColumn(traversed, i, j))
                            {
                                matrix[i, j]++;
                                traversed.Add(new Coordinate(i, j));
                            }
                        }
                    }
                }

                current++;
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

    private static bool NotTraversedRowOrColumn(List<Coordinate> coordinates, int x, int y)
    {
        return !coordinates.Any(coordinate => coordinate.x == x || coordinate.y == y);
    }

    private static void PrintMatrix(int[,] matrix)
    {
        // int rowLength = ;
        // int colLength = matrix.GetLength(1);
        //
        // for (int i = 0; i < rowLength; i++)
        // {
        //     for (int j = 0; j < colLength; j++)
        //     {
        //         Console.Write(string.Format("{0} ", matrix[j, i]));
        //     }
        //     Console.Write(Environment.NewLine);
        // }
        // Console.ReadLine();
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