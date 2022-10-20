namespace Nsu.Princess.Model;

public class Contender
{
    public string Name { get; }
    public int Rank { get; }

    public Contender(string name, int rank)
    {
        Name = name;
        Rank = rank;
    }

    public override string ToString()
    {
        return Name;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Contender contender)
        {
            return contender.Name == Name && contender.Rank == Rank;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Rank);
    }

    protected bool Equals(Contender other)
    {
        return Name == other.Name && Rank == other.Rank;
    }
}