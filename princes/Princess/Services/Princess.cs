using Nsu.Princess.Configuration;
using Nsu.Princess.Exceptions;

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
        return Task.Run(RunAsync);
    }

    private void RunAsync()
    {
        while (_running)
        {
            try
            {
                string? newContender = FirstStrategy();
                if (_alreadyGoes.Count == ApplicationConfig.CONTENDERS_NUMBER)
                {
                    Console.WriteLine(10);
                }

                if (newContender is null)
                {
                    throw new Exception("Something wrong");
                }

                Console.WriteLine(newContender);
                if (_running == false)
                {
                    Console.WriteLine(_hall.GetContenderLevelByName(newContender));
                    _appLifetime.StopApplication();
                }
            }
            catch (NoMoreContendersException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(10);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private string FirstStrategy()
    {
        string newContender = _hall.GetNewContenderName();
        
        if (_alreadyGoes.Count < ApplicationConfig.CONTENDERS_NUMBER / 2)
        {
            _alreadyGoes.Add(newContender);
            return newContender;
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
        }

        return newContender;
    }
}