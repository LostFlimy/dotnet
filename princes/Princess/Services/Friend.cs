using Nsu.Princess.Exceptions;

namespace Nsu.Princess.Services;

public class Friend : IFriend
{
    private IHall _hall;

    public Friend(IHall hall)
    {
        _hall = hall;
    }

    public string AskBetter(string first, string second)
    {
        if (_hall.IsGoToPrincess(first) && _hall.IsGoToPrincess(second))
        {
            return ((_hall.GetContenderLevelByName(first) > _hall.GetContenderLevelByName(second)) ? first : second);
        }

        if (_hall.Exists(first) && _hall.Exists(second))
        {
            throw new ContenderNotVisitPrincessException(
                first + " or " + second + " have not yet visited the princess."
            );
        }

        throw new ContenderNotExistsException(first + " or " + second + " not exists.");
    }
}