using Nsu.Princess.Model;
using Nsu.Princess.Services;
using NUnit.Framework;

namespace Nsu.Princess.Tests;

[TestFixture]
public class GeneratorTests
{
    private readonly IContenderGenerator _contenderGenerator;

    public GeneratorTests()
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
                    Assert.That(!generatedContenders[i].Equals(generatedContenders[j]));
                }
            }
        }
    }
}