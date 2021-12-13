namespace Day8;

public class DisplayReader
{
    private readonly Display[] _displays;

    public static readonly Dictionary<int, int> EasyDigits = new()
    {
        { 1, 2 },
        { 4, 4 },
        { 7, 3 },
        { 8, 7 }
    };

    private readonly Dictionary<int, int> _mapping = new()
    {
        { 0, 19 },
        { 1, 9 },
        { 2, 20 },
        { 3, 21 },
        { 4, 15 },
        { 5, 24 },
        { 6, 25 },
        { 7, 10 },
        { 8, 28 },
        { 9, 23 }
    };

    public DisplayReader(string filePath)
    {
        _displays = File.ReadAllLines(filePath)
            .Select(Display.Parse)
            .ToArray();
    }

    public int CountEasyDigits()
    {
        var total = 0;
        foreach (var display in _displays)
        {
            total += display.OutputValues.Count(x => EasyDigits.ContainsValue(x.Length));
        }

        return total;
    }

    public long CountOutput()
    {
        var total = 0L;
        foreach (var display in _displays)
        {
            var config = FindConfiguration(display);
            var output = "";
            foreach (var value in display.OutputValues)
            {
                if (value.Length == 2) output += '1';
                else if (value.Length == 3) output += '7';
                else if (value.Length == 4) output += '4';
                else if (value.Length == 7) output += '8';
                else if (value.Length == 5)
                {
                    if (value.Contains(config[3][0]) && value.Contains(config[3][1]))
                    {
                        output += '3';
                    }
                    else if (value.Contains(config[2][0]) && value.Contains(config[2][1]))
                    {
                        output += '5';
                    }
                    else output += '2';
                }
                else
                {
                    if (value.Contains(config[3][0]) &&
                        value.Contains(config[3][1]) &&
                        value.Contains(config[4][0]) &&
                        value.Contains(config[4][1]))
                    {
                        output += '9';
                    }
                    else if (value.Contains(config[2][0]) &&
                             value.Contains(config[2][1]) &&
                             value.Contains(config[5][0]) &&
                             value.Contains(config[5][1]))
                    {
                        output += '6';
                    }
                    else output += '0';
                }
            }

            total += long.Parse(output);
        }

        return total;
    }

    public Dictionary<int, string> FindConfiguration(Display display)
    {
        var configuration = new Dictionary<int, string>
        {
            { 1, "" },
            { 2, "" },
            { 3, "" },
            { 4, "" },
            { 5, "" },
            { 6, "" },
            { 7, "" },
        };

        foreach (var easyDigit in display.EasyDigits().OrderBy(x => x.Length))
        {
            switch (easyDigit.Length)
            {
                case 2:
                    configuration[3] += easyDigit;
                    configuration[6] += easyDigit;
                    break;
                case 3:
                    configuration[1] += easyDigit.NotIn(configuration[3], configuration[6]);
                    if (configuration[1].Length > 1)
                    {
                        configuration[3] += configuration[1];
                        configuration[6] += configuration[1];
                    }

                    break;
                case 4:
                    configuration[2] += easyDigit.NotIn(configuration[3], configuration[6]);
                    configuration[4] += easyDigit.NotIn(configuration[3], configuration[6]);
                    if (configuration[2].Length > 2)
                    {
                        configuration[3] += easyDigit;
                        configuration[6] += easyDigit;
                    }

                    break;
                case 7:
                    var result = easyDigit.NotIn(
                        configuration[1],
                        configuration[2],
                        configuration[3],
                        configuration[4],
                        configuration[6]);
                    foreach (var (key, value) in configuration)
                    {
                        if (string.IsNullOrEmpty(value))
                        {
                            configuration[key] += result;
                        }
                    }

                    break;
            }
        }

        return configuration;
    }
}

public record Display(string[] SignalPatterns, string[] OutputValues)
{
    public static Display Parse(string raw)
    {
        var split = raw.Split("|");
        return new Display(split[0].Split(" "), split[1].Split(" "));
    }

    public string[] Combined()
    {
        var combined = new string[SignalPatterns.Length + OutputValues.Length];
        SignalPatterns.CopyTo(combined, 0);
        OutputValues.CopyTo(combined, SignalPatterns.Length);
        return combined;
    }

    public string[] EasyDigits()
    {
        return Combined()
            .Select(x => string.Concat(x.OrderBy(c => c)))
            .Distinct()
            .Where(x => DisplayReader.EasyDigits.ContainsValue(x.Length))
            .ToArray();
    }
}

public static class StringExtension
{
    public static string NotIn(this string str, params string[] otherStr)
        => new(str
            .Where(x =>
                !otherStr.SelectMany(y => y)
                    .Distinct()
                    .Contains(x))
            .ToArray());
}