using AdventOfCode2024.Utils;
using AdventOfCode2024.Utils.Grid;
using System.Diagnostics;
using System.IO;

namespace AdventOfCode2024.Days.Day10;

public class DayTenPuzzels
{
	public void HandlePuzzles()
	{

		var input = ReadInputFile.GetGridWithOutSplit(ReadInputFile.GetPathToInput(10));
		var grid = new Grid<int>(input);

		var stopwatch = new Stopwatch();
		stopwatch.Start();
		var result = PartOne(grid);
		stopwatch.Stop();
		Console.WriteLine($"Day 10 part one: {result} time ms: {stopwatch.ElapsedMilliseconds}");

		stopwatch.Restart();
		var partTwo = PartTwo(grid);
		stopwatch.Stop();
		Console.WriteLine($"Day 10 part two: {partTwo} time ms: {stopwatch.ElapsedMilliseconds}");
	}

	public int PartOne(Grid<int> grid)
	{
		var allStartingPrositions = grid.Cells.Where(_ => _.Value == 0).ToList();

		var paths = 0;

		foreach (var startingPoint in allStartingPrositions)
		{
			paths += ValidPaths(grid, startingPoint, null);
			HitTops = [];
		}

		return paths;
	}

	public List<Coordinate> HitTops { get; set; } = [];

	public int ValidPaths(Grid<int> grid, Cell<int> position, Cell<int>? previousCell)
	{

		var validPaths = 0;

		foreach (var direction in DirectionHelper.UpDownLeftRight)
		{
			var cellInDirection = grid.GetCellBasedOnDirection(position, direction);

			if (
				cellInDirection == null ||
				cellInDirection == previousCell ||
				cellInDirection.Value != position.Value + 1 ||
				HitTops.Contains(cellInDirection.Coordinate)
			)
			{
				continue;
			}

			if (cellInDirection.Value == 9)
			{
				validPaths++;
				HitTops.Add(cellInDirection.Coordinate);
				continue;
			}

			validPaths += ValidPaths(grid, cellInDirection, position);

		}

		return validPaths;
	}


	public int PartTwo(Grid<int> grid)
	{
		var allStartingPrositions = grid.Cells.Where(_ => _.Value == 0).ToList();

		var paths = 0;

		foreach (var startingPoint in allStartingPrositions)
		{
			paths += TrailRating(grid, startingPoint, null);
			HitTops = [];
		}

		return paths;
	}

	public int TrailRating(Grid<int> grid, Cell<int> position, Cell<int>? previousCell)
	{
		var validPaths = 0;

		foreach (var direction in DirectionHelper.UpDownLeftRight)
		{
			var cellInDirection = grid.GetCellBasedOnDirection(position, direction);

			if (
				cellInDirection == null ||
				cellInDirection == previousCell ||
				cellInDirection.Value != position.Value + 1
			)
			{
				continue;
			}

			if (cellInDirection.Value == 9)
			{
				validPaths++;
				continue;
			}

			validPaths += TrailRating(grid, cellInDirection, position);
		}

		return validPaths;
	}
}
