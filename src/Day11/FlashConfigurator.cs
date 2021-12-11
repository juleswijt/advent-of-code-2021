using Common;
using Common.Extensions;

namespace Day11;

public class FlashConfigurator
{
    private readonly int[,] _energyMap;

    private readonly List<(int, int)> Flashed = new();

    public readonly List<(int, int)> Directions = new()
    {
        (0, -1),
        (0, 1),
        (1, -1),
        (1, 1),
        (1, 0),
        (-1, 1),
        (-1, -1),
        (-1, 0)
    };


    public FlashConfigurator(bool example)
    {
        var rawMap = InputFile.Read(11, example)
            .Select(x => x.ToCharArray())
            .ToArray();

        _energyMap = new int[rawMap.Length, rawMap.First().Length];
        for (var x = 0; x < rawMap.Length; x++)
        for (var y = 0; y < rawMap[x].Length; y++)
            _energyMap[x, y] = int.Parse(rawMap[x][y].ToString());
    }

    public int CalculateFlashes()
    {
        PrintMap();

        var flashes = 0;
        for (var steps = 0; steps < 100; steps++)
        {
            for (var x = 0; x < _energyMap.GetLength(0); x++)
            for (var y = 0; y < _energyMap.GetLength(1); y++)
                _energyMap[x, y]++;

            for (var x = 0; x < _energyMap.GetLength(0); x++)
            for (var y = 0; y < _energyMap.GetLength(1); y++)
                Flash(x, y);

            for (var x = 0; x < _energyMap.GetLength(0); x++)
            for (var y = 0; y < _energyMap.GetLength(1); y++)
                if (_energyMap[x, y] > 9)
                {
                    flashes++;
                    _energyMap[x, y] = 0;
                }

            Flashed.Clear();
            PrintMap();
        }

        return flashes;
    }

    public int CalculateSimultaneousFlash()
    {
        int flashes;
        var step = 0;
        do
        {
            for (var x = 0; x < _energyMap.GetLength(0); x++)
            for (var y = 0; y < _energyMap.GetLength(1); y++)
                _energyMap[x, y]++;

            for (var x = 0; x < _energyMap.GetLength(0); x++)
            for (var y = 0; y < _energyMap.GetLength(1); y++)
                Flash(x, y);

            var currentFlashes = 0;
            for (var x = 0; x < _energyMap.GetLength(0); x++)
            for (var y = 0; y < _energyMap.GetLength(1); y++)
                if (_energyMap[x, y] > 9)
                {
                    currentFlashes++;
                    _energyMap[x, y] = 0;
                }

            Flashed.Clear();
            flashes = currentFlashes;
            step++;
        } while (flashes != _energyMap.GetLength(0) * _energyMap.GetLength(0));

        return step;
    }

    private void Flash(int x, int y)
    {
        if (_energyMap[x, y] > 9 && !Flashed.Contains((x, y)))
        {
            _energyMap[x, y]++;
            Flashed.Add((x, y));
            foreach (var (xDirection, yDirection) in Directions)
            {
                var newX = x + xDirection;
                var newY = y + yDirection;
                if (_energyMap.isInMap(newX, newY))
                {
                    _energyMap[newX, newY]++;
                    Flash(newX, newY);
                }
            }
        }
    }

    private void PrintMap()
    {
        for (var x = 0; x < _energyMap.GetLength(0); x++)
        {
            for (var y = 0; y < _energyMap.GetLength(1); y++)
            {
                var write = _energyMap[x, y] == 0 ? $"\x1b[31m{_energyMap[x, y]}\x1b[0m" : _energyMap[x, y].ToString();
                Console.Write(write);
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
}