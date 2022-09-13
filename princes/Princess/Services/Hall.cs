using Nsu.Princess.Model;

namespace Nsu.Princess.Services;

public class Hall : IHall
{
    private List<Contender> _contenders;
    private List<Contender> _alreadyGoes;
    
    public Hall(IContenderGenerator generator)
    {
        _contenders = generator.GenerateContenders();
        _alreadyGoes = new List<Contender>();
    }
    
    public string? GetNewContenderName()
    {
        if (_contenders.Count > 0)
        {
            Contender newContender = _contenders[0];
            _alreadyGoes.Add(newContender);
            _contenders.RemoveAt(0);
            return newContender.Name;
        }

        return null;
    }

    public int GetContenderLevelByName(string name)
    {
        foreach (Contender contender in _contenders)
        {
            if (contender.Name.Equals(name))
            {
                return contender.Rank;
            }
        }

        foreach (Contender contender in _alreadyGoes)
        {
            if (contender.Name.Equals(name))
            {
                return contender.Rank;
            }
        }

        throw new ArgumentException("Have not contender with this name!");
    }

    public bool IsGoToPrincess(string name)
    {
        foreach (Contender contender in _alreadyGoes)
        {
            if (contender.Name.Equals(name))
            {
                return true;
            }
        }
        
        return false;
    }

    public bool Exists(string name)
    {
        foreach (Contender contender in _contenders)
        {
            if (contender.Name.Equals(name))
            {
                return true;
            }
        }

        foreach (Contender contender in _alreadyGoes)
        {
            if (contender.Name.Equals(name))
            {
                return true;
            }
        }

        return false;
    }
}