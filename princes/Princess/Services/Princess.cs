using Nsu.Princess.Configuration;
using Nsu.Princess.Exceptions;
using Nsu.Princess.Model;

namespace Nsu.Princess.Services;

public class Princess : IHostedService
{
    private bool _running = true;
    private IHall _hall;
    private IFriend _friend;

    private List<string> _alreadyGoes;
    private IHostApplicationLifetime _appLifetime;
    public Princess(
        IHall hall,
        IFriend friend,
        IHostApplicationLifetime appLifetime
    )
    {
        _hall = hall;
        _friend = friend;
        _alreadyGoes = new List<string>();
        _appLifetime = appLifetime;

    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(RunAsync);
        return Task.CompletedTask;
    }

    private void RunAsync()
    {
        while (_running)
        {
            try
            {
                PrincessChoice choice = FirstStrategy();
                if (_alreadyGoes.Count == ApplicationConfig.CONTENDERS_NUMBER)
                {
                    Console.WriteLine(10);
                }

                if (choice is null || choice.Name is null)
                {
                    throw new Exception("Something wrong");
                }

                Console.WriteLine(choice.Name);
                if (_running == false)
                {
                    Console.WriteLine(choice.Rank);
                    _appLifetime.StopApplication();
                }
            }
            catch (NoMoreContendersException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(10);
                throw;
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public PrincessChoice FirstStrategy()
    {
        string newContender = _hall.GetNewContenderName();
        
        if (_alreadyGoes.Count < ApplicationConfig.CONTENDERS_NUMBER / 2)
        {
            _alreadyGoes.Add(newContender);
            return new PrincessChoice(newContender, 0, ApplicationConfig.NEGATIVE_ANSWER);
        }

        int betterThen = 0;
        _alreadyGoes.Add(newContender);

        foreach (string prevName in _alreadyGoes)
        {
            if (_friend.AskBetter(prevName, newContender) == newContender)
            {
                betterThen++;
            }
        }

        if (betterThen > ApplicationConfig.CONTENDERS_NUMBER / 2)
        {
            _running = false;
            return new PrincessChoice(
                newContender, 
                _hall.GetContenderLevelByName(newContender), 
                ApplicationConfig.POSITIVE_ANSWER
                );
        }

        return new PrincessChoice(
            newContender, 
            _hall.GetContenderLevelByName(newContender), 
            ApplicationConfig.NEGATIVE_ANSWER
        );
    }
}