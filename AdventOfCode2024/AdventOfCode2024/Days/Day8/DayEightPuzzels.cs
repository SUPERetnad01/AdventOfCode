using AdventOfCode2024.Utils;
using AdventOfCode2024.Utils.Grid;

namespace AdventOfCode2024.Days.Day8;

public static class DayEightPuzzels
{

	public static void HandlePuzzels()
	{
		var rawGrid = ReadInputFile.GetGridChar(ReadInputFile.GetPathToInput(8));
		var attenaGrid = new Grid<char>(rawGrid);

		var partOne = PartOne(attenaGrid);
		Console.WriteLine($"Day 8 part one {partOne}");
	}

	public static int PartOne(Grid<char> grid)
	{

		var differentAntennas = grid.Cells
			.Where(_ => _.Value != '.')
			.GroupBy(_ => _.Value);

		var totalAntinodes = new List<IEnumerable<Coordinate>>();

		foreach (var antennaGroup in differentAntennas)
		{
			foreach (var antenna in antennaGroup)
			{
				var antinodes = antennaGroup.Where(_ => _ != antenna)
				.Select(_ =>
				{
					var antinodeDistance = _.Coordinate - antenna.Coordinate;

					var antinodePosition = _.Coordinate + antinodeDistance;
					return antinodePosition;
				})
				.Where(antinode => grid.Cells.Any(_ => _.Coordinate == antinode));


				totalAntinodes.Add(antinodes);
			}

		}
		var totalA = totalAntinodes.SelectMany(_ => _).Distinct();

		return totalA.Count();
	}

	public List<Coordinate> GetRessonantFrequencies(Coordinate startingPoint, Coordinate distance, Grid<char> grid)
	{
		if (grid.Cells.Any(_ => _.Coordinate != startingPoint))
		{
			return [];
		}

		var antinodeDistance = startingPoint - distance;

	}
}
