using princes.Model;

namespace princes.Services;

public interface IContenderGenerator
{
    List<Contender> GenerateContenders(int count);
}