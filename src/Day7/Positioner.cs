namespace Day7;

public class Positioner
{
    private readonly int[] _crabs;

    public Positioner(string filePath)
    {
        _crabs = File.ReadAllLines(filePath)[0]
            .Split(",")
            .Select(int.Parse)
            .ToArray();
    }

    public int CalculateMinimumFuelUsage()
    {
        var distinctCrabs = _crabs.Distinct().ToArray();
        var fuelUsage = new int[distinctCrabs.Length];
        for (var index = 0; index < distinctCrabs.Length; index++)
        {
            fuelUsage[index] = CalculateFuelUsage(distinctCrabs[index]);
        }

        return fuelUsage.Min();
    }

    public int CalculateMinimumEngineeredFuelUsage()
    {
        var distinctCrabs = Enumerable.Range(0, _crabs.Max() + 1).ToArray();
        var fuelUsage = new int[distinctCrabs.Length];
        for (var index = 0; index < distinctCrabs.Length; index++)
        {
            fuelUsage[index] = CalculateEngineeredFuelUsage(distinctCrabs[index]);
        }

        return fuelUsage.Min();
    }

    private int CalculateFuelUsage(int position) => _crabs.Sum(crab => Math.Abs(position - crab));

    private int CalculateEngineeredFuelUsage(int position)
    {
        var fuelUsage = 0;
        foreach (var crab in _crabs)
        {
            var steps = Math.Abs(position - crab);
            fuelUsage += steps * (steps + 1) / 2;
        }

        return fuelUsage;
    }
}