using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;

namespace Common;

[AttributeUsage(AttributeTargets.Method)]
public class PuzzleAttribute : Attribute, ITestBuilder, IImplyFixture
{
    public PuzzleAttribute(object answer, string? file = null) => (Answer, FilePath) = (answer, file);

    public object Answer { get; }

    public string? FilePath { get; }

    public IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test? suite)
    {
        var parameters = new TestCaseParameters(new object?[]
        {
            File.ReadAllText(FileName(method))
        })
        {
            ExpectedResult = Answer
        };

        yield return new NUnitTestCaseBuilder().BuildTestMethod(method, suite, parameters);
    }

    protected virtual string FileName(IMethodInfo method) => FilePath ?? $"{method.TypeInfo.Name}.txt";
}