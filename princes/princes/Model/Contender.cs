namespace princes.Model;

public class Contender
{
    public string Name { get; private set;}
    public int Rank { get; private set;}

    public Contender(string name, int rank)
    {
        Name = name;
        Rank = rank;
    }

    public override string ToString()
    {
        return Name;
    }
}