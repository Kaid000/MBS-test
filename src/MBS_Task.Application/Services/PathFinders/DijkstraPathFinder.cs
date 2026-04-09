using MBS_Task.Application.Helpers.DistanceMap;
using MBS_Task.Application.Interfaces;
using MBS_Task.Application.Services.NeighborProviders;
using MBS_Task.Entities.Models;

namespace MBS_Task.Application.Services.PathFinders;

public class DijkstraPathFinder : IPathFinder
{
    private readonly ICostStrategy _costStrategy;
    private readonly INeighborProvider _neighborProvider;

    public DijkstraPathFinder(ICostStrategy costStrategy, INeighborProvider neighborProvider)
    {
        _costStrategy = costStrategy;
        _neighborProvider = neighborProvider;
    }

    public int FindShortestPath(Grid grid)
    {
        var dist = new DistanceMap(grid.Rows, grid.Cols);
        var pq = new PriorityQueue<Position, int>();
        var usedPortals = new HashSet<char>();

        dist.Set(grid.Start, 0);
        pq.Enqueue(grid.Start, 0);

        while (pq.Count > 0)
        {
            var current = pq.Dequeue();
            int currentDist = dist.Get(current);
            var currentCell = grid.Cells[current.X, current.Y];

            if (current.Equals(grid.End))
                return currentDist;

            foreach (var (neighborPos, isTeleport) in
                ((GridNeighborProvider)_neighborProvider).GetNeighborsWithTeleportFlag(current, grid, usedPortals))
            {
                var neighborCell = grid.Cells[neighborPos.X, neighborPos.Y];

                int stepCost = isTeleport ? 0 : _costStrategy.GetCost(neighborCell);
                int newDist = currentDist + stepCost;

                if (newDist < dist.Get(neighborPos))
                {
                    dist.Set(neighborPos, newDist);
                    pq.Enqueue(neighborPos, newDist);
                }
            }
        }

        return -1;
    }
}
