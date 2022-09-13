using Nsu.Princess.Model;

namespace Nsu.Princess.Services;

public interface IContenderGenerator
{
    List<Contender> GenerateContenders();
}