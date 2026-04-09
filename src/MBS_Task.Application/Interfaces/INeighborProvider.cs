using MBS_Task.Entities.Models;

namespace MBS_Task.Application.Interfaces;

public interface INeighborProvider
{
    public IEnumerable<Position> GetNeighbors(Position current, Grid grid, HashSet<char> usedPortals);
}