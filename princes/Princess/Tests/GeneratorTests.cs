using Nsu.Princess.Model;
using Nsu.Princess.Services;
using NUnit.Framework;

namespace Nsu.Princess.Tests;

[TestFixture]
public class GeneratorTests
{
    private IContenderGenerator _contenderGenerator;

    [SetUp]
    public void SetUp()
    {
        _contenderGenerator = new ContenderGenerator();
    }

    [Test]
    public void UniqueContendersTest()
    {
        List<Contender> generatedContenders = _contenderGenerator.GenerateContenders();

        for (int i = 0; i < generatedContenders.Count; ++i)
        {
            for (int j = 0; j < generatedContenders.Count; ++j)
            {
                if (j != i)
                {
                    Assert.AreNotEqual(generatedContenders[i],generatedContenders[j]);
                }
            }
        }
    }
}