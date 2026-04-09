using MBS_Task.Application.Interfaces;
using MBS_Task.Domain.Enums;
using MBS_Task.Entities.Models;

namespace MBS_Task.Application.Services.Strategies;

public class DefaultCostStrategy : ICostStrategy
{
    public int GetCost(Cell cell)
    {
        return cell.Type switch
        {
            CellType.Empty => 1,
            CellType.Start => 0,
            CellType.End => 1,
            CellType.Portal => 1,
            CellType.Weighted => cell.Raw - '0',
            CellType.Wall => int.MaxValue,
            _ => 1
        };
    }
}