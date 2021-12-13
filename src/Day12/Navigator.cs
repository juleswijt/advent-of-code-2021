using Common;

namespace Day12;

public class Navigator
{
    private readonly Dictionary<string, List<string>> _paths;
    private readonly List<string[]> _simpleRoutes = new();
    private readonly List<string[]> _complexRoutes = new();

    public Navigator()
    {
        _paths = new Dictionary<string, List<string>>();
        foreach (var path in InputFile.Read(12, false))
        {
            var split = path.Split("-");
            AddPath(split[0], split[1]);
            AddPath(split[1], split[0]);
        }
    }

    public int CalculateSimpleRoutes()
    {
        NavigateSimple("start", new List<string>());
        return _simpleRoutes.Count;
    }

    public int CalculateComplexRoutes()
    {
        NavigateComplex("start", new List<string>());
        return _complexRoutes.Count;
    }

    private void NavigateSimple(string current, ICollection<string> route)
    {
        route = new List<string>(route);
        route.Add(current);
        foreach (var path in _paths[current])
        {
            switch (path)
            {
                case "end":
                    route.Add("end");
                    _simpleRoutes.Add(route.ToArray());
                    // PrintRoute(route);
                    break;
                case "start":
                    continue;
                default:
                {
                    if (path == path.ToUpper()) NavigateSimple(path, route);
                    else if (!route.Contains(path)) NavigateSimple(path, route);
                    else continue;
                    break;
                }
            }
        }
    }

    private void NavigateComplex(string current, ICollection<string> route)
    {
        route = new List<string>(route);
        route.Add(current);
        foreach (var path in _paths[current])
        {
            switch (path)
            {
                case "end":
                    route.Add("end");
                    _complexRoutes.Add(route.ToArray());
                    // PrintRoute(route);
                    break;
                case "start":
                    continue;
                default:
                {
                    if (CanNavigate(path, route)) NavigateComplex(path, route);
                    break;
                }
            }
        }
    }

    private void AddPath(string node, string connect)
    {
        if (_paths.ContainsKey(node)) _paths[node].Add(connect);
        else _paths.Add(node, new List<string> { connect });
    }

    private static bool CanNavigate(string path, IEnumerable<string> route)
    {
        if (path == path.ToUpper()) return true;
        if (!route.Contains(path)) return true;

        return route
            .Where(x => x.ToUpper() != x)
            .GroupBy(x => x)
            .All(group => group.Count() != 2);
    }

    private static void PrintRoute(IEnumerable<string> route)
    {
        foreach (var path in route) Console.Write($"{path},");
        Console.WriteLine();
    }
}