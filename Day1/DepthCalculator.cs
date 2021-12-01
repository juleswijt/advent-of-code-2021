namespace Day1;

public class DepthCalculator
{
    private readonly int[] _values;

    public DepthCalculator(string filePath)
    {
        _values = File.ReadAllLines(filePath)
            .Select(int.Parse)
            .ToArray();
    }

    public int MeasurePartOne()
    {
        var increased = 0;
        var previous = _values[0];
        for (var i = 1; i < _values.Length; i++)
        {
            if (_values[i] > previous) increased++;
            previous = _values[i];
        }

        return increased;
    }

    public int MeasurePartTwo()
    {
        var increased = 0;
        var previous2 = _values[0] + _values[1] + _values[2];
        for (var i = 3; i < _values.Length; i++)
        {
            var value = _values[i] + _values[i - 1] + _values[i - 2];
            if (value > previous2) increased++;
            previous2 = value;
        }

        return increased;
    }
}