using Moq;
using Nsu.Princess.Exceptions;
using Nsu.Princess.Model;
using Nsu.Princess.Services;
using NUnit.Framework;

namespace Nsu.Princess.Tests;

public class FriendTests
{
    private IFriend _friend;
    private IHall _hall;
    
    [SetUp]
    public void SetUp()
    {
        _hall = new Hall(GenerateMockGenerator());
        _friend = new Friend(_hall);
    }

    [Test]
    public void CorrectCompareContenders()
    {
        _hall.GetNewContenderName();
        _hall.GetNewContenderName();
        Assert.Equals(_friend.AskBetter("Alex", "Bill"), "Bill");
    }

    [Test]
    public void IncorrectCompareContenders()
    {
        Assert.Throws<ContenderNotExistsException>(delegate { _friend.AskBetter("Anonym", "Alex"); });
        Assert.Throws<ContenderNotVisitPrincessException>(delegate { _friend.AskBetter("Alex", "Bill"); });
        _hall.GetNewContenderName();
        Assert.Throws<ContenderNotVisitPrincessException>(delegate { _friend.AskBetter("Alex", "Bill"); });
    }

    private IContenderGenerator GenerateMockGenerator()
    {
        List<Contender> contenders = new List<Contender>();
        contenders.Add(new Contender("Alex", 15));
        contenders.Add(new Contender("Bill", 16));
        var generator = Mock.Of<IContenderGenerator>(
            fg => fg.GenerateContenders() == contenders
        );
        return generator;
    }
}