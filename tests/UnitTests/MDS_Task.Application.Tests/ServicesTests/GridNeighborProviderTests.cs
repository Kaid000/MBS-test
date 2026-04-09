using MBS_Task.Application.Services.NeighborProviders;
using MBS_Task.Entities.Models;
using Shouldly;

namespace MBS_Task.Application.Tests.ServicesTests
{
    [TestFixture]
    public class GridNeighborProviderTests
    {
        private GridNeighborProvider _provider;

        [SetUp]
        public void SetUp() => _provider = new GridNeighborProvider();

        [Test]
        public void GetNeighbors_Should_Return_Adjacent_NonWall_Cells()
        {
            var cells = new Cell[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    cells[i, j] = new Cell(new Position(i, j), '.');

            cells[1, 1] = new Cell(new Position(1, 1), '#');

            var grid = new Grid(cells, new Position(0, 0), new Position(2, 2), new Dictionary<char, List<Position>>());
            var usedPortals = new HashSet<char>();

            var neighbors = _provider.GetNeighbors(new Position(1, 0), grid, usedPortals);
            neighbors.ShouldBe(new List<Position> { new Position(0, 0), new Position(2, 0) }, ignoreOrder: true);
        }
    }
}