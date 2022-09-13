using princes.Model;

namespace princes.Services;

public class Hall : IHall
{
    private List<Contender> contenders;
    private List<Contender> alreadyGoes;
    
    public Hall(List<Contender> contenders)
    {
        this.contenders = contenders;
        alreadyGoes = new List<Contender>();
    }
    
    public string? GetNewContenderName()
    {
        if (contenders.Count > 0)
        {
            Contender newContender = contenders[0];
            alreadyGoes.Add(newContender);
            contenders.RemoveAt(0);
            return newContender.Name;
        }

        return null;
    }

    public int GetContenderLevelByName(string name)
    {
        foreach (Contender contender in contenders)
        {
            if (contender.Name.Equals(name))
            {
                return contender.Rank;
            }
        }

        foreach (Contender contender in alreadyGoes)
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
        foreach (Contender contender in alreadyGoes)
        {
            if (contender.Name.Equals(name))
            {
                return true;
            }
        }
        
        return false;
    }
}