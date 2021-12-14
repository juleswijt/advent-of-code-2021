using Common;

namespace Day14;

public class Templater
{
    private readonly IEnumerable<Rule> _rawRules;
    private readonly Dictionary<string, char> _rules;
    private readonly char _lastChar;
    private Dictionary<string, long> _template;

    public Templater()
    {
        var lines = InputFile.Read(14, false);

        _rawRules = lines.Skip(2).Select(Rule.Parse);
        _rules = _rawRules.ToDictionary(x => x.Pair, x => x.Insertion);
        _template = _rawRules.ToDictionary(x => x.Pair, x => 0L);
        _lastChar = lines[0].Last();
        for (var index = 0; index < lines[0].Length - 1; index++)
        {
            _template[new string(new[] { lines[0][index], lines[0][index + 1] })]++;
        }
    }

    public long Calculate()
    {
        var characters = _rules.Keys.SelectMany(x => x.ToCharArray())
            .Distinct()
            .ToDictionary(x => x, x => 0L);
        foreach (var (pair, amount) in _template)
        {
            characters[pair[0]] += amount;
        }

        characters[_lastChar]++;

        return characters.Max(x=> x.Value) - characters.Min(x=> x.Value);
    }

    public void ApplySteps(int steps)
    {
        for (var step = 0; step < steps; step++)
        {
            var newDict = _rawRules.ToDictionary(x => x.Pair, x => 0L);
            foreach (var (pair, amount) in _template)
            {
                var character = _rules[pair];
                newDict[new string(new[] { pair[0], character })] += amount;
                newDict[new string(new[] { character, pair[1] })] += amount;
            }

            Console.WriteLine(newDict.Sum(x => x.Value));
            _template = newDict;
        }
    }
}

public record Rule(string Pair, char Insertion)
{
    public static Rule Parse(string raw)
    {
        var split = raw.Split(" -> ");
        return new Rule(split[0], split[1].ToCharArray().First());
    }
}