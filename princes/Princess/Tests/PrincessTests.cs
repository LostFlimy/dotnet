using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Hosting.Internal;

using Moq;
using Nsu.Princess.Configuration;
using Nsu.Princess.Exceptions;
using Nsu.Princess.Model;
using Nsu.Princess.Services;
using NUnit.Framework;

namespace Nsu.Princess.Tests;

[TestFixture]
public class PrincessTests
{
    private IFriend _friend;
    private IHall _hall;
    private IContenderGenerator _generator;
    private Services.Princess _princess;

    [SetUp]
    public void SetUp()
    {
        _generator = new ContenderGenerator();
        _hall = new Hall(_generator);
        _friend = new Friend(_hall);
        ILogger<ApplicationLifetime> logger = NullLogger<ApplicationLifetime>.Instance;
        ApplicationLifetime lifetime = new(logger);
        _princess = new Services.Princess(_hall, _friend, lifetime);
    }

    [Test]
    public void CheckCorrectWorking()
    {
        PrincessChoice? choice = null;
        List<string> alreadyGoes = new List<string>();
        for (int i = 0; i < 100; ++i)
        {
            choice = _princess.FirstStrategy();
            alreadyGoes.Add(choice.Name);
            Assert.NotNull(choice);
            
            if (choice.Answer.Equals(ApplicationConfig.POSITIVE_ANSWER))
            {
                Assert.That(i >= 100 / 2); //Первых 50 пропускаем
                Assert.That(choice.Rank.Equals(_hall.GetContenderLevelByName(choice.Name)));
            }
            
        }
    }

    [Test]
    public void CheckExceptionWorking()
    {
        //Пропускаем вручную первых 100 притендентов
        for (int i = 0; i < 100; ++i)
        {
            _hall.GetNewContenderName();
        }

        Assert.Throws<NoMoreContendersException>(delegate { _princess.FirstStrategy();});
    }
    
}