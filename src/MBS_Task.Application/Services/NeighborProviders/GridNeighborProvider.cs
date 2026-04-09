using MBS_Task.Application.Interfaces;
using MBS_Task.Domain.Enums;
using MBS_Task.Entities.Models;

namespace MBS_Task.Application.Services.NeighborProviders;

public class GridNeighborProvider : INeighborProvider
{
    private static readonly int[] dx = { 1, -1, 0, 0 };
    private static readonly int[] dy = { 0, 0, 1, -1 };

    public IEnumerable<(Position pos, bool isTeleport)> GetNeighborsWithTeleportFlag(Position current, Grid grid, HashSet<char> usedPortals)
    {
        var currentCell = grid.Cells[current.X, current.Y];

        for (int d = 0; d < 4; d++)
        {
            int nx = current.X + dx[d];
            int ny = current.Y + dy[d];

            if (nx < 0 || ny < 0 || nx >= grid.Rows || ny >= grid.Cols)
                continue;

            var neighbor = grid.Cells[nx, ny];
            if (neighbor.Type != CellType.Wall)
                yield return (neighbor.Position, false); // обычный шаг
        }

        if (currentCell.Type == CellType.Portal && !usedPortals.Contains(currentCell.Raw))
        {
            usedPortals.Add(currentCell.Raw);

            foreach (var portalPos in grid.Portals[currentCell.Raw])
            {
                if (!portalPos.Equals(current))
                    yield return (portalPos, true); // teleport = true
            }
        }
    }

    public IEnumerable<Position> GetNeighbors(Position current, Grid grid, HashSet<char> usedPortals)
    {
        foreach (var (pos, _) in GetNeighborsWithTeleportFlag(current, grid, usedPortals))
            yield return pos;
    }
}