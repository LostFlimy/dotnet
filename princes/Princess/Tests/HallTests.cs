using Moq;
using Nsu.Princess.Exceptions;
using Nsu.Princess.Model;
using Nsu.Princess.Services;
using NUnit.Framework;

namespace Nsu.Princess.Tests;

public class HallTests
{
    private IHall _hall;

    [SetUp]
    public void SetUp()
    {
        var contender1 = new Contender("Alex", 15);
        var contenders = new List<Contender>();
        contenders.Add(contender1);
        var generator = Mock.Of<IContenderGenerator>(
            fg => fg.GenerateContenders() == contenders);
        _hall = new Hall(generator);
    }

    [Test]
    public void getContenderNameIfHave()
    {
        Assert.Equals(_hall.GetNewContenderName(), "Alex");
        Assert.Equals(_hall.GetContenderLevelByName("Alex"), 15);
    }

    [Test]
    public void getContenderNameIfHaveNot()
    {
        _hall.GetNewContenderName();
        Assert.Throws<NoMoreContendersException>(delegate {_hall.GetNewContenderName();});
    }

}