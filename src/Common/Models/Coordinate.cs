namespace Common.Models;

public record Coordinate(int X, int Y)
{
    public Coordinate[] DirectNeighbours()
        => new[]
        {
            this with { X = X + 1 },
            this with { X = X - 1 },
            this with { Y = Y + 1 },
            this with { Y = Y - 1 },
        };
}