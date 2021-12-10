using Common;

namespace Day10;

public class Parser
{
    private readonly List<long> _contestEntries = new();

    private readonly Dictionary<char, int> _wrong = new()
    {
        { ')', 0 },
        { '>', 0 },
        { ']', 0 },
        { '}', 0 }
    };

    private readonly Dictionary<char, char> _mapping = new()
    {
        { '(', ')' },
        { '<', '>' },
        { '[', ']' },
        { '{', '}' }
    };

    private readonly Dictionary<char, int> _scoring = new()
    {
        { ')', 1 },
        { ']', 2 },
        { '}', 3 },
        { '>', 4 }
    };


    public Parser(bool example)
    {
        var corrupted = InputFile.Read(10, example)
            .Select(x => x.ToCharArray());

        ProcessCorrupted(corrupted);
    }

    public int TotalErrorScore() => _wrong[')'] * 3 + _wrong[']'] * 57 + _wrong['}'] * 1197 + _wrong['>'] * 25137;

    public long MiddleScore() => _contestEntries.OrderByDescending(x => x).ToArray()[_contestEntries.Count / 2];

    private void ProcessCorrupted(IEnumerable<char[]> corruptedLines)
    {
        foreach (var corruptedLine in corruptedLines)
        {
            var stack = new Stack<char>();
            var incorrect = false;
            foreach (var character in corruptedLine)
            {
                if (_mapping.ContainsKey(character)) stack.Push(character);
                else if (_mapping[stack.Peek()] == character) stack.Pop();
                else
                {
                    incorrect = true;
                    _wrong[character]++;
                    break;
                }
            }

            if (!incorrect)
            {
                var completed = "";
                foreach (var character in stack)
                {
                    completed += _mapping[character];
                }

                _contestEntries.Add(CalculateScore(completed));
            }
        }
    }

    private long CalculateScore(string completion)
    {
        long score = 0;
        foreach (var character in completion.ToCharArray())
        {
            score = (score * 5) + _scoring[character];
        }

        return score;
    }
}