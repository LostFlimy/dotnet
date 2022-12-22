namespace Nsu.Princess.Model;

public class PrincessChoice
{
    public string Name { get; }

    public int Rank { get; }
    
    public string Answer { get; }

    public PrincessChoice(string name, int rank, string answer)
    {
        Name = name;
        Rank = rank;
        Answer = answer;
    }
}