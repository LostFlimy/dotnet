using Nsu.Princess.Configuration;

namespace Nsu.Princess.Services;

public class Princess : IHostedService
{
    private bool _running = true;
    private IHall _hall;
    private IFriend _friend;
    private IContenderGenerator _generator;

    private List<string> _alreadyGoes;
    private IHostApplicationLifetime _appLifetime;
    public Princess(
        IHall hall,
        IFriend friend,
        IContenderGenerator contenderGenerator,
        IHostApplicationLifetime appLifetime
    )
    {
        _hall = hall;
        _friend = friend;
        _generator = contenderGenerator;
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
            string? newContender = FirstStrategy();
            if (newContender is null && _alreadyGoes.Count == ApplicationConfig.CONTENDERS_NUMBER)
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
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private string? FirstStrategy()
    {
        string? newContender = _hall.GetNewContenderName();
        if (newContender is null)
        {
            return newContender;
        }
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