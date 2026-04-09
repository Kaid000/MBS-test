using MBS_Task.Application.DependencyInjection;
using MBS_Task.Application.Services.NeighborProviders;
using MBS_Task.Application.Services.PathFinders;
using MBS_Task.Application.Services.Strategies;
using MBS_Task.Infrastructure.DataLoaders;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace MDS_Task.Application.Tests
{
    [TestFixture]
    public class DijkstraPathFinderTests
    {
        private DijkstraPathFinder _pathFinder;
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddApplicationServices();
            _pathFinder = new DijkstraPathFinder(new DefaultCostStrategy(), new GridNeighborProvider());
        }

        public static IEnumerable<TestCaseData> PathFindingCases()
        {
            yield return new TestCaseData(new[]
            {
                "5 7",
                "S..2..A",
                ".#.#...",
                "..#..#.",
                "A..3..E",
                "......."
            }, 6).SetName("Example1");

            yield return new TestCaseData(new[]
            {
                "3 5",
                "SAB..",
                "#####",
                "..BAE"
            }, 2).SetName("PortalsShortcut");

            yield return new TestCaseData(new[]
            {
                "5 5",
                "S.A..",
                "###.#",
                "A#.#E",
                "#...#",
                "#####"
            }, -1).SetName("BlockedPath");

            yield return new TestCaseData(new[]
            {
                "3 3",
                "S#E",
                "###",
                "..."
            }, -1).SetName("WallBlockingEnd");

            yield return new TestCaseData(new[]
            {
                "3 3",
                "S1E",
                ".#.",
                "..."
            }, 2).SetName("WeightedCell");
        }

        [Test, TestCaseSource(nameof(PathFindingCases))]
        public void FindShortestPath_Should_Return_Correct_Result(string[] lines, int expected)
        {
            // Arrange
            var reader = new StringReader(string.Join("\n", lines));
            var loader = new TextGridLoader();
            var grid = loader.Load(reader);

            // Act
            int result = _pathFinder.FindShortestPath(grid);

            // Assert
            result.ShouldBe(expected);
        }
    }
}