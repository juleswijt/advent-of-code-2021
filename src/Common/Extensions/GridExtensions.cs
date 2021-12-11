namespace Common.Extensions;

public static class GridExtensions
{
    public static bool isInMap(this int[,] map, int x, int y)
        => x >= 0
           && x < map.GetLength(0)
           && y >= 0
           && y < map.GetLength(1);
}