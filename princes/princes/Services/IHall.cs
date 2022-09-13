using princes.Model;

namespace princes.Services;

public interface IHall
{
    string? GetNewContenderName();

    int GetContenderLevelByName(string name);

    bool IsGoToPrincess(string name);
}