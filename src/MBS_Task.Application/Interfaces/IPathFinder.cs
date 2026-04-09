using MBS_Task.Entities.Models;

namespace MBS_Task.Application.Interfaces;

public interface IPathFinder
{
    public int FindShortestPath(Grid grid);
}