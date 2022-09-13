using Nsu.Princess.Model;

namespace Nsu.Princess.Services;

public class ContenderGenerator : IContenderGenerator
{
    private List<string> names;
    private int generatedCount;

    public ContenderGenerator()
    {
        names = new List<string>();
        generatedCount = 0;
    }

    public List<Contender> GenerateContenders(int count)
    {
        GenerateNames();
        List<Contender> resultList = new List<Contender>();

        for (int i = 0; i < count; ++i)
        {
            Contender newContender = GenerateUnique();
            while (resultList.Contains(newContender))
            {
                newContender = GenerateUnique();
            }
            resultList.Add(newContender);
        }

        return ShufleContenders(resultList);
    }

    private void GenerateNames()
    {
        StreamReader reader = new StreamReader("Properties/contendersNames.txt");
        string? name;
        while ((name = reader.ReadLine()) != null)
        {
            names.Add(name);
        }
        reader.Close();
    }

    private List<Contender> ShufleContenders(List<Contender> contenders)
    {
        List<Contender> result = new List<Contender>();
        int initialCount = contenders.Count;
        
        for (int i = 0; i < initialCount; ++i)
        {
            int pickedContenderIndex = Random.Shared.Next(0, contenders.Count);
            result.Add(contenders[pickedContenderIndex]);
            contenders.RemoveAt(pickedContenderIndex);
        }

        return result;
    }
    
    private Contender GenerateUnique()
    {
        int pickedNameIndex = Random.Shared.Next(0, names.Count);
        generatedCount++;
        Contender generatedContender = new Contender(names[pickedNameIndex], generatedCount);
        names.RemoveAt(pickedNameIndex);
        return generatedContender;
    }
}