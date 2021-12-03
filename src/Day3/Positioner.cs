namespace Day3;

public class Positioner
{
    private readonly string[] _diagnostics;

    private readonly string[] _examples = new[]
    {
        "00100",
        "11110",
        "10110",
        "10111",
        "10101",
        "01111",
        "00111",
        "11100",
        "10000",
        "11001",
        "00010",
        "01010"
    };

    public Positioner(string filePath)
    {
        _diagnostics = File.ReadAllLines(filePath);
    }

    public int CalculatePartOne()
    {
        var gammaRate = string.Empty;
        var epsilonRate = string.Empty;
        foreach (var (most, least, _) in ToEntries(_diagnostics))
        {
            gammaRate += most;
            epsilonRate += least;
        }

        Console.WriteLine($"GammaRate is {gammaRate} and EpsilonRate is {epsilonRate}");

        return Convert.ToInt32(epsilonRate, 2) * Convert.ToInt32(gammaRate, 2);
    }

    public int CalculatePartTwo()
    {
        var oxygen = Loop(_diagnostics, 0, "oxygen");
        var carbon = Loop(_diagnostics, 0, "carbon");
        Console.WriteLine($"Oxygen is {oxygen} and Carbon is {carbon}");

        return oxygen * carbon;
    }

    private int Loop(IEnumerable<string> loopOver, int index, string type)
    {
        while (true)
        {
            var entries = ToEntries(loopOver);
            var indexes = new List<string>();
            foreach (var t in loopOver)
            {
                if (entries[index].AreEqual)
                {
                    if (type == "oxygen" && t[index] == '1')
                    {
                        indexes.Add(t);
                    }

                    if (type == "carbon" && t[index] == '0')
                    {
                        indexes.Add(t);
                    }

                    continue;
                }

                if (type == "oxygen" && entries[index].Most == t[index])
                {
                    indexes.Add(t);
                }

                if (type == "carbon" && entries[index].Least == t[index])
                {
                    indexes.Add(t);
                }
            }

            if (indexes.Count == 1) return Convert.ToInt32(indexes.First(), 2);
            loopOver = indexes;
            index++;
        }
    }

    private DiagnosticEntry[] ToEntries(IEnumerable<string> rawDiagnostic)
    {
        var bits = new string[rawDiagnostic.First().Length];
        foreach (var t in rawDiagnostic)
        {
            for (var j = 0; j < t.Length; j++)
            {
                bits[j] += t[j];
            }
        }

        return bits.Select(DiagnosticEntry.Parse).ToArray();
    }
}

public record DiagnosticEntry(char Most, char Least, bool AreEqual)
{
    public static DiagnosticEntry Parse(string input)
    {
        var zeros = input.Count(x => x == '0');
        var ones = input.Count(x => x == '1');
        var most = zeros >= ones ? '0' : '1';
        var least = zeros < ones ? '0' : '1';
        return new DiagnosticEntry(most, least, zeros == ones);
    }
}