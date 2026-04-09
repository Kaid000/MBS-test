using MBS_Task.Application.Services.Strategies;
using MBS_Task.Domain.Enums;
using MBS_Task.Entities.Models;
using Shouldly;

namespace MBS_Task.Application.Tests.ServicesTests
{
    [TestFixture]
    public class DefaultCostStrategyTests
    {
        private DefaultCostStrategy _strategy;

        [SetUp]
        public void SetUp() => _strategy = new DefaultCostStrategy();

        [Test]
        [TestCase(CellType.Empty, ' ', 1)]
        [TestCase(CellType.Start, 'S', 0)]
        [TestCase(CellType.End, 'E', 1)]
        [TestCase(CellType.Portal, 'A', 1)]
        [TestCase(CellType.Weighted, '3', 3)]
        [TestCase(CellType.Wall, '#', int.MaxValue)]
        public void GetCost_Should_Return_Correct_Value(CellType type, char raw, int expected)
        {
            var cell = new Cell(new Position(0, 0), raw);
            cell.Type.ShouldBe(type);
            int cost = _strategy.GetCost(cell);
            cost.ShouldBe(expected);
        }
    }
}
