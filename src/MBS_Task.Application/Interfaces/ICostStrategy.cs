using MBS_Task.Entities.Models;

namespace MBS_Task.Application.Interfaces;

public interface ICostStrategy
{
   public int GetCost(Cell cell);
}