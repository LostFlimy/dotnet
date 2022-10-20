namespace Nsu.Princess.Services;

public interface IHall
{
    string GetNewContenderName();

    int GetContenderLevelByName(string name);

    bool IsGoToPrincess(string name);

    bool Exists(string name);
}