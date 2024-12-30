using AdventOfCode2024.Utils;
using AdventOfCode2024.Utils.Grid;
using System.Diagnostics;

namespace AdventOfCode2024.Days.Day8;

public static class DayEightPuzzels
{

	public static void HandlePuzzels()
	{
		var rawGrid = ReadInputFile.GetGridChar(ReadInputFile.GetPathToInput(8));

		var attenaGrid = new Grid<char>(rawGrid);

		var stopwatch = new Stopwatch();
		stopwatch.Start();
		var partOne = PartOne(attenaGrid);
		stopwatch.Stop();

		Console.WriteLine($"Day 8 part one {partOne}, {stopwatch.ElapsedMilliseconds} ms");

		stopwatch.Restart();
		var partTwo = PartTwo(attenaGrid);
		stopwatch.Stop();

		Console.WriteLine($"Day 8 part two {partTwo}, {stopwatch.ElapsedMilliseconds} ms");
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

	public static int PartTwo(Grid<char> grid)
	{

		var antinodes = grid.Cells
			.Where(_ => _.Value != '.')
			.GroupBy(_ => _.Value)
			.SelectMany(antennaGroup => antennaGroup
				.SelectMany(antenna => antennaGroup
					.Where(_ => _ != antenna)
					.SelectMany(_ =>
					{
						var antinodeDistance = _.Coordinate - antenna.Coordinate;
						var antinodePosition = _.Coordinate + antinodeDistance;

						var ResonantFrequencyNodes = GetResonantFrequencies(antinodePosition, antinodeDistance, grid, [antinodePosition, _.Coordinate]);

						return ResonantFrequencyNodes;
					})

		))
		.Where(grid.IsInGrid)
		.Distinct()
		.Count();

		return antinodes;
	}

	public static IEnumerable<Coordinate> GetResonantFrequencies(Coordinate startingPoint, Coordinate distance, Grid<char> grid, List<Coordinate> resultResonateFrequencies)
	{
		var newAntinode = startingPoint + distance;

		if (!grid.IsInGrid(newAntinode))
		{
			return resultResonateFrequencies;
		}
		resultResonateFrequencies.Add(newAntinode);

		var otherNodes = GetResonantFrequencies(newAntinode, distance, grid, resultResonateFrequencies);

		return otherNodes.Concat(resultResonateFrequencies);

	}
}
