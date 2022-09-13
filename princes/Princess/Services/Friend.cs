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

        throw new ArgumentException("This contender is never been at princess or not exist");
    }
}