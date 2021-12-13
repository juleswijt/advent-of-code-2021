namespace Day9;

public class Reader
{
    private int[,] _heightmap;

    public readonly List<(int, int)> Directions = new()
    {
        (0, -1),
        (0, 1),
        (1, 0),
        (-1, 0)
    };

    public Reader(string filePath)
    {
        var rawMap = File.ReadAllLines(filePath)
            .Select(x => x.ToCharArray()).ToArray();

        _heightmap = new int[rawMap.Length, rawMap.First().Length];
        for (var x = 0; x < rawMap.Length; x++)
        for (var y = 0; y < rawMap[x].Length; y++)
            _heightmap[x, y] = int.Parse(rawMap[x][y].ToString());
    }

    public (int, int) AssesRiskLevel()
    {
        var sumOfRiskLevels = 0;
        var basins = new List<int>();
        for (var x = 0; x < _heightmap.GetLength(0); x++)
        {
            for (var y = 0; y < _heightmap.GetLength(1); y++)
            {
                if (IsLowPoint(x, y))
                {
                    sumOfRiskLevels += _heightmap[x, y] + 1;
                    basins.Add(CalculateBasinSize(x, y));
                    _heightmap = _heightmap.Reset();
                }
            }
        }

        var sumOfBasins = basins
            .OrderByDescending(x => x)
            .Take(3)
            .Aggregate(1, (current, basinSize) => current * basinSize);

        return (sumOfRiskLevels, sumOfBasins);
    }

    private int CalculateBasinSize(int x, int y)
    {
        var size = 1;
        _heightmap[x, y] = _heightmap[x, y] == 0 ? -10 : _heightmap[x, y] * -1;
        foreach (var (xDirection, yDirection) in Directions)
        {
            var newX = x + xDirection;
            var newY = y + yDirection;
            if (IsInMap(newX, newY) && _heightmap[newX, newY] != 9 && _heightmap[newX, newY] >= 0)
            {
                size += CalculateBasinSize(newX, newY);
            }
        }

        return size;
    }

    private bool IsLowPoint(int x, int y)
    {
        var currentHeight = _heightmap[x, y];
        foreach (var (xDirection, yDirection) in Directions)
        {
            var newX = x + xDirection;
            var newY = y + yDirection;
            if (IsInMap(newX, newY) && _heightmap[newX, newY] <= currentHeight)
            {
                return false;
            }
        }

        return true;
    }

    private bool IsInMap(int x, int y)
        => x >= 0
           && x < _heightmap.GetLength(0)
           && y >= 0
           && y < _heightmap.GetLength(1);
}

public static class Extensions
{
    public static int[,] Reset(this int[,] heightMap)
    {
        for (var x = 0; x < heightMap.GetLength(0); x++)
        for (var y = 0; y < heightMap.GetLength(1); y++)
            if (heightMap[x, y] == -10) heightMap[x, y] = 0;
            else if (heightMap[x, y] < 0) heightMap[x, y] *= -1;
        return heightMap;
    }
}