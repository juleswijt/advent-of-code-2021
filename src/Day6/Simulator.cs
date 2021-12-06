namespace Day6;

public class Simulator
{
    private readonly int _numberOfDays;
    private readonly long[] _fishes;
    private readonly Dictionary<long, long> _options;

    public Simulator(string filePath, int numberOfDays = 80)
    {
        _numberOfDays = numberOfDays;
        _fishes = File.ReadAllLines(filePath)[0]
            .Split(",")
            .Select(long.Parse)
            .ToArray();
        _options = _fishes.Distinct().ToDictionary(x => x);
        foreach (var (key, _) in _options) _options[key] = CalculateFish(key);
    }

    public long SimulatePartOne()
    {
        return _fishes.Sum(fish => _options[fish]);
    }

    private long CalculateFish(long start)
    {
        var fishes = new long[9];
        var updated = new long[9];
        fishes[start] += 1;
        for (var day = 0; day < _numberOfDays; day++)
        {
            for (var index = 0; index < fishes.Length; index++)
            {
                if (index == 0)
                {
                    updated[6] += fishes[index];
                    updated[8] += fishes[index];
                }
                else
                {
                    updated[index - 1] += fishes[index];
                }
            }

            fishes = updated;
            updated = new long[9];
        }

        return fishes.Sum();
    }
}