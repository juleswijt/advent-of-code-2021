using System.Text;

namespace Common.Extensions;

public static class GridExtensions
{
    public static bool isInMap(this int[,] map, int x, int y)
        => x >= 0
           && x < map.GetLength(0)
           && y >= 0
           && y < map.GetLength(1);

    public static void Print(this int[,] map)
    {
        for (var y = 0; y < map.GetLength(1); y++)
        {
            var builder = new StringBuilder("");
            for (var x = 0; x < map.GetLength(0); x++)
            {
                builder.Append(map[x, y] >= 1 ? '#' : '.');
            }

            Console.WriteLine(builder.ToString());
        }
    }
}