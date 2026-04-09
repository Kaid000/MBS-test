using Shouldly;


namespace MBS_Task.Service.Tests.InputTests
{
	[TestFixture]
	public class ProgramTests
	{
		[Test]
		public void Should_Read_From_Stdin_And_Return_Result()
		{
			// Arrange
			var input = string.Join(Environment.NewLine, new[]
			{
				"5 7",
				"S..2..A",
				".#.#...",
				"..#..#.",
				"A..3..E",
				"......."
			});

			var reader = new StringReader(input);
			var writer = new StringWriter();

			Console.SetIn(reader);
			Console.SetOut(writer);

			// Act
			Program.Main(Array.Empty<string>());

			// Assert
			var output = writer.ToString().Trim();

			output.ShouldContain("6");
		}

		[Test]
		public void Should_Read_From_File_And_Return_Result()
		{
			// Arrange
			var inputFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "input.txt");

			if (!File.Exists(inputFilePath))
			{
				Assert.Fail($"No file: {inputFilePath}");
			}

			using var writer = new StringWriter();
			Console.SetOut(writer);

			// Act
			Program.Main(new[] { "--input", inputFilePath });

			// Assert
			var output = writer.ToString().Trim();
			output.ShouldBe("6");
		}

	}
}