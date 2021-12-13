namespace Day4;

public class Caller
{
    private readonly int[] _callingOrder;
    private readonly List<BingoCart> _carts;

    public Caller(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        _callingOrder = lines[0].Split(",").Select(int.Parse).ToArray();
        _carts = string
            .Join("\n", lines.Skip(2))
            .Split("\n\n")
            .Select(BingoCart.Parse)
            .ToList();
    }

    public int CalculateWinningScore()
    {
        foreach (var current in _callingOrder)
        {
            foreach (var cart in _carts)
            {
                cart.Mark(current);
                if (cart.HasWon())
                    return cart.Remaining * current;
            }

            Console.WriteLine("-------------------------");
        }

        return 0;
    }

    public int CalculateLosingScore()
    {
        foreach (var current in _callingOrder)
        {
            foreach (var cart in _carts.Where(cart => !cart.Won))
            {
                cart.Mark(current);
                if (cart.HasWon())
                {
                    if (_carts.Count(x => !x.Won) == 1) return cart.Remaining * current;
                    cart.Won = true;
                }
            }

            Console.WriteLine("-------------------------");
        }

        return 0;
    }
}

public record BingoCart(List<Bingo[]> Rows)
{
    public bool Won { get; set; }

    public void Mark(int number)
    {
        foreach (var bingo in Rows.SelectMany(x => x))
        {
            if (bingo.Number == number) bingo.Called = true;
        }

        foreach (var bingo in Rows)
        {
            Console.WriteLine(string.Join(";", bingo.Select(x => x.Called ? $"\x1b[31m{x.Number}\x1b[0m" : x.Number.ToString())));
        }

        Console.WriteLine("");
    }

    public bool HasWon()
    {
        if (Rows.Any(x => x.All(bingo => bingo.Called))) return true;
        for (var rowIndex = 0; rowIndex < 5; rowIndex++)
        {
            var columnCalls = 0;
            for (var columnIndex = 0; columnIndex < 5; columnIndex++)
            {
                if (Rows[columnIndex][rowIndex].Called)
                {
                    columnCalls++;
                    if (columnCalls == 5) return true;
                }
            }
        }

        return false;
    }

    public int Remaining => Rows
        .SelectMany(x => x.Where(bingo => !bingo.Called))
        .Distinct()
        .Sum(x => x.Number);

    public static BingoCart Parse(string raw)
    {
        var rows = raw.Split("\n")
            .Select(t => t.Split(" ")
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(Bingo.Parse)
                .ToArray())
            .ToList();

        return new BingoCart(rows);
    }
}

public record Bingo(int Number)
{
    public bool Called { get; set; }

    public static Bingo Parse(string raw) => new(int.Parse(raw));
}