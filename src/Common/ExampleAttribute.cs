using NUnit.Framework.Interfaces;

namespace Common;

[AttributeUsage(AttributeTargets.Method)]
public sealed class ExampleAttribute : PuzzleAttribute
{
    public ExampleAttribute(object answer, string? file = null) : base(answer, file)
    {
    }

    protected override string FileName(IMethodInfo method) => FilePath ?? $"{method.TypeInfo.Name}_example.txt";
}